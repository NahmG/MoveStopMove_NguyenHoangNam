using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIShopSkin : UICanvas
{
    [SerializeField]
    UIButton[] tabBtns = new UIButton[4];
    [SerializeField]
    UIShopTab[] tabs = new UIShopTab[4];

    [FoldoutGroup("Button")]
    [SerializeField]
    UIButton closeBtn, buyBtn, equipBtn;

    [FoldoutGroup("Text")]
    [SerializeField]
    TMP_Text cost, description;

    int currentTab = -1;
    public int CurrentTab
    {
        get => currentTab;
        set
        {
            if (currentTab == value) return;
            OpenTab(value);
        }
    }
    Skin currentItem;
    PlayerData playerData;
    PlayerEquipment playerEquip;

    void Awake()
    {
        playerData ??= DataManager.Ins.Get<PlayerData>();
        playerEquip ??= GameplayManager.Ins.Player.Core.DISPLAY.Equipment as PlayerEquipment;

        currentTab = -1;
        for (int i = 0; i < tabBtns.Length; i++)
        {
            tabBtns[i]._OnClick += OnTabBtnClick;
        }
        tabs.ForEach((x) =>
        {
            x._OnItemSelect += OnItemSelect;
            x.Initialize();
        });
        closeBtn._OnClick += OnCloseBtnClick;
        buyBtn._OnClick += OnBuyBtnClick;
        equipBtn._OnClick += OnEquipBtnClick;

        CloseAllTab();
    }

    void OnDestroy()
    {
        for (int i = 0; i < tabBtns.Length; i++)
        {
            tabBtns[i]._OnClick -= OnTabBtnClick;
        }
        closeBtn._OnClick -= OnCloseBtnClick;
        buyBtn._OnClick -= OnBuyBtnClick;
        equipBtn._OnClick -= OnEquipBtnClick;

        tabs.ForEach(x => x._OnItemSelect -= OnItemSelect);
    }

    public override void Open(object param = null)
    {
        base.Open(param);
        OpenTab(0);

        GameplayManager.Ins.mainCam.ChangeCamera(CAMERA_TYPE.SHOP);
        GameplayManager.Ins.Player.ChangeState(STATE.SHOP_SKIN);
    }

    public override void UpdateUI()
    {
        base.UpdateUI();
        tabs.ForEach(x => x.UpdateTab());
    }

    void OnTabBtnClick(int index)
    {
        playerEquip.Load();
        OpenTab(index);
    }

    void OnCloseBtnClick(int index)
    {
        Hide();

        playerEquip.Load();
        GameplayManager.Ins.Player.ChangeState(STATE.IDLE);
        UIManager.Ins.OpenUI<UIMainMenu>();
    }

    void OpenTab(int index)
    {
        if (currentTab >= 0)
        {
            tabs[currentTab].Hide();
            tabBtns[currentTab].SetState(UIButton.STATE.OPENING);
        }

        currentTab = index;
        tabs[index].Show();
        tabBtns[index].SetState(UIButton.STATE.SELECTING);
    }

    void CloseAllTab()
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            if (tabs[i] == null) continue;
            tabBtns[i].SetState(UIButton.STATE.OPENING);
            tabs[i].Hide();
        }
    }

    void OnItemSelect(Skin item)
    {
        currentItem = item;
        UpdateBtnState(item);

        if (item.isLock)
            cost.text = item.cost.ToString();

        description.text = item.description;
        playerEquip.Equip(item);
    }

    void OnBuyBtnClick(int index)
    {
        if (currentItem == null || !currentItem.isLock) return;

        if (playerData.gold.Value >= currentItem.cost)
        {
            playerData.gold.Plus(-currentItem.cost);
            currentItem.isLock = false;

            tabs[currentTab].UpdateTab();
            UpdateBtnState(currentItem);
        }
    }

    void OnEquipBtnClick(int index)
    {
        if (currentItem == null || currentItem.isLock) return;

        if (currentItem.isEquip)
        {
            playerEquip.UnEquip(currentItem);
            playerEquip.Save();
            currentItem.isEquip = false;
        }
        else
        {
            playerEquip.UnEquipOldItem(tabs[currentTab].type);
            currentItem.isEquip = true;
            playerEquip.Equip(currentItem);
            playerEquip.Save();
        }

        UpdateBtnState(currentItem);
    }

    void UpdateBtnState(Item item)
    {
        buyBtn.gameObject.SetActive(item.isLock);
        equipBtn.gameObject.SetActive(!item.isLock);

        if (item.isEquip)
            equipBtn.SetState(UIButton.STATE.SELECTING);
        else
            equipBtn.SetState(UIButton.STATE.OPENING);
    }
}

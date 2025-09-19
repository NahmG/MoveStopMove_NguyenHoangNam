using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
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

    void Awake()
    {
        currentTab = -1;
        for (int i = 0; i < tabBtns.Length; i++)
        {
            tabBtns[i]._OnClick += OnTabBtnClick;
        }
        closeBtn._OnClick += OnCloseBtnClick;

        tabs.ForEach((x) =>
        {
            x._OnItemSelect += OnItemSelect;
            x.Initialize();
        });

        CloseAllTab();
    }

    void OnDestroy()
    {
        for (int i = 0; i < tabBtns.Length; i++)
        {
            tabBtns[i]._OnClick -= OnTabBtnClick;
        }
        closeBtn._OnClick -= OnCloseBtnClick;
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
        OpenTab(index);
    }

    void OnCloseBtnClick(int index)
    {
        Hide();

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

    void OnItemSelect(ItemData data)
    {
        buyBtn.gameObject.SetActive(data.isLock);
        equipBtn.gameObject.SetActive(!data.isLock);

        if (data.isLock)
        {
            cost.text = data.cost.ToString();
        }
        description.text = data.description;
    }
}

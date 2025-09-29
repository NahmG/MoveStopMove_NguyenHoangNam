using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIShopTab : UICanvasComponent
{
    public Action<Skin> _OnItemSelect;
    [SerializeField] Transform content;
    [SerializeField] UIItemButton itemBtnPref;
    public SHOP type;

    List<Skin> itemDatas;
    List<UIItemButton> itemBtns = new();
    int currentBtn = -1;

    void OnDestroy()
    {
        if (itemBtns.Count > 0)
        {
            itemBtns.ForEach(x => x._OnClick -= OnItemBtnClick);
        }
    }

    public void Initialize()
    {
        itemDatas = DataManager.Ins.Get<ShopData>().GetItems<Skin>(type);
        for (int i = 0; i < itemDatas.Count; i++)
        {
            UIItemButton item = Instantiate(itemBtnPref, content);
            item.Init(i, itemDatas[i]);
            item._OnClick += OnItemBtnClick;

            itemBtns.Add(item);
        }
        DeSelectAllItem();
    }

    public override void Show()
    {
        base.Show();
        SelectItem(0);
    }

    public void UpdateTab()
    {
        itemBtns.ForEach(x => x.UpdateItem());
    }

    void OnItemBtnClick(int index)
    {
        SelectItem(index);
    }

    void SelectItem(int index)
    {
        if (currentBtn >= 0)
        {
            itemBtns[currentBtn].DeSelect();
        }
        currentBtn = index;
        itemBtns[currentBtn].OnSelect();

        _OnItemSelect.Invoke(itemDatas[currentBtn]);
    }

    void DeSelectAllItem()
    {
        itemBtns.ForEach(x => x.DeSelect());
    }
}

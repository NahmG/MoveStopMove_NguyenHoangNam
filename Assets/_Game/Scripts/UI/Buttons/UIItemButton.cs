using System;
using UnityEngine;
using UnityEngine.UI;

public class UIItemButton : UIButton
{
    [Header("Item")]
    [SerializeField] GameObject frameSelect;
    [SerializeField] GameObject itemLock;
    [SerializeField] Image itemIcon;
    Item data;

    public void Init(int index, Item data)
    {
        this.data = data;
        this.index = index;
    }

    public void UpdateItem()
    {
        itemIcon.sprite = data.icon;
        itemLock.SetActive(data.isLock);
    }

    public void OnSelect()
    {
        SetState(STATE.SELECTING);
        frameSelect.SetActive(true);
    }

    public void DeSelect()
    {
        SetState(STATE.OPENING);
        frameSelect.SetActive(false);
    }
}
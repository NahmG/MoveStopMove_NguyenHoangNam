using System;
using UnityEngine;
using UnityEngine.UI;

public class UIItemButton : UIButton
{

    [Header("Item")]
    [SerializeField] GameObject frameSelect;
    [SerializeField] GameObject itemLock;
    [SerializeField] Image itemIcon;
    ItemData data;

    public void Init(int index, ItemData data)
    {
        this.data = data;
        this.index = index;
    }

    public void UpdateItem()
    {
        itemIcon.sprite = data.image;
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

[Serializable]
public class ItemData
{
    public Sprite image;
    public bool isLock;
    public int cost;
    public string description;
}
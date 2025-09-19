using System;
using UnityEngine;

[Serializable]
public class EquipComponent
{
    public EQUIP_TYPE ID;

    [SerializeField]
    Transform parentTF;
    EquipmentModel modelPref;

    int currentIndex;
    public int CurrentIndex => currentIndex;
    EquipmentModel currentModel;

    public void Start()
    {
        currentIndex = -1;
    }

    public virtual void Equip(int index)
    {
        if (currentIndex == index) return;

        if (currentIndex >= 0)
        {
            UnEquip();
        }

        currentIndex = index;
        modelPref ??= DataManager.Ins.GetModel(ID);
        currentModel = GameObject.Instantiate(modelPref, parentTF);
    }

    public virtual void UnEquip()
    {
        if (currentModel != null)
            GameObject.Destroy(currentModel);

        currentModel = null;
    }

    public virtual void Save() { }
}
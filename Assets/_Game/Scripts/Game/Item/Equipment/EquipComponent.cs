using System;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

[Serializable]
public class EquipComponent
{
    public EQUIP ID;
    int currentIndex;
    public int CurrentIndex => currentIndex;
    protected EquipmentData data;

    public virtual void Initialize()
    {
        currentIndex = -1;
        data = DataManager.Ins.Get<EquipmentData>();
    }

    public virtual void Equip(int index)
    {
        if (currentIndex >= 0)
        {
            UnEquip();
        }
        currentIndex = index;
    }

    public virtual void UnEquip() { }

    public virtual void Save() { }
}

public class RenderComponent : EquipComponent
{
    [SerializeField]
    Renderer rend;
    [SerializeField]
    Material setMaterial;
    [SerializeField]
    int excludeId;
    [SerializeField]
    bool isUseDefault;
    Material defaultMat;

    public override void Initialize()
    {
        base.Initialize();
        if (isUseDefault)
            defaultMat = setMaterial;
        else
            defaultMat = data.GetRandomItemExclude(ID, excludeId) as Material;
    }

    public override void Equip(int index)
    {
        base.Equip(index);

        Material mat;

        if (index < 0)
            mat = defaultMat;
        else
            mat = data.GetItem(ID, index) as Material;
        rend.sharedMaterial = mat;
    }

    public override void UnEquip()
    {
        base.UnEquip();
        rend.sharedMaterial = defaultMat;
    }
}

public class ObjectComponent : EquipComponent
{
    [SerializeField]
    protected Transform parentTF;
    GameObject current;

    public override void Equip(int index)
    {
        base.Equip(index);

        if (index < 0) return;

        GameObject model = data.GetItem(ID, index) as GameObject;
        current = GameObject.Instantiate(model, parentTF);
    }

    public override void UnEquip()
    {
        base.UnEquip();
        if (current != null)
            GameObject.Destroy(current);
    }
}
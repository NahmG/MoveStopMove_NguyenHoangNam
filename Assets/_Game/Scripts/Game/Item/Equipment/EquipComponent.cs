using System;
using UnityEngine;

[Serializable]
public abstract class EquipComponent : MonoBehaviour
{
    public abstract EQUIP_TYPE ID { get; }

    [SerializeField]
    Transform parentTF;
    [SerializeField]
    GameObject model;

    GameObject currentModel;

    public virtual void Equip()
    {
        if (currentModel == null)
            Instantiate(model, parentTF);
    }

    public virtual void Remove()
    {
        if (currentModel != null)
            Destroy(currentModel);

        currentModel = null;
    }

    public virtual void Save() { }
}
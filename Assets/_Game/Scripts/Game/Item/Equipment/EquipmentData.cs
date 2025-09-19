using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentData : ScriptableObject
{
    public string equipName;
    public EquipmentModel model;
    public Material material;
    public int cost;
    public string description;
    public bool isLock;
    public bool isEquip;
}

public class EquipmentModel : MonoBehaviour
{
    [SerializeField]
    Renderer rend;

    public void SetSkin(Material material)
    {
        rend.sharedMaterial = material;
    }

    public void SetSkin(Texture texture)
    {
        rend.sharedMaterial.mainTexture = texture;
    }
}
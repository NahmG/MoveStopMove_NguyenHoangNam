using Sirenix.OdinInspector;
using UnityEngine;

public class Item : SerializedScriptableObject
{
    [Header("Info")]
    public string equipName;
    public string description;
    public Sprite icon;
    public int cost;

    [Header("Param")]
    public int Id;
    public SHOP shop;
    public bool isLock = true;
}

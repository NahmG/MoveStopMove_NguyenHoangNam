using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class Item : SerializedScriptableObject
{
    public string equipName;
    public string description;
    public Sprite icon;
    public int cost;
    public bool isLock = true;
    public bool isEquip = false;
}

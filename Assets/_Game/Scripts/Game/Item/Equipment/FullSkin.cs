using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Full skin", menuName = "ScriptableObject/Item/Full Skin")]
public class FullSkin : Item
{
    [ListDrawerSettings(ShowIndexLabels = true)]
    public int[] setId = new int[] { -1, -1, -1, -1, -1, -1 };
}
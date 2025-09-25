using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin Data", menuName = "ScriptableObject/Item/Skin")]
public class Skin : Item
{
    public bool isSet;
    [HideIf("isSet")] public EQUIP type = EQUIP.NONE;
    [HideIf("isSet")] public int modelId = -1;
    [ShowIf("isSet")]
    [ListDrawerSettings(ShowIndexLabels = true)]
    public int[] setId = new int[] { -1, -1, -1, -1, -1, -1 };
}
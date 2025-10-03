using Core.Display;
using UnityEngine;

public class EnemyDisplay : DisplayCore
{
    [SerializeField]
    GameObject indicator;

    public override void TurnIndicator(bool isOn)
    {
        base.TurnIndicator(isOn);
        indicator.SetActive(isOn);
    }
}
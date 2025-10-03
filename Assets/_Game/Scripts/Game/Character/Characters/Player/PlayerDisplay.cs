using Core.Display;
using UnityEngine;

public class PlayerDisplay : DisplayCore
{
    [SerializeField]
    Transform rangeIndicator;

    public override void TurnIndicator(bool isOn)
    {
        base.TurnIndicator(isOn);
        rangeIndicator.gameObject.SetActive(isOn);
    }

    public override void UpdateIndicator(float range)
    {
        base.UpdateIndicator(range);
        rangeIndicator.localScale = range * Vector3.one;
    }
}
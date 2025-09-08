
using System;
using UnityEngine;

[Serializable]
public class Stat
{
    public float BaseValue;
    public float _value;
    public float Value => _value;

    public Stat(float baseValue)
    {
        BaseValue = baseValue;
    }

    public void Reset()
    {
        _value = BaseValue;
    }

    public void Add(float _amount)
    {
        _value += _amount;
    }

}
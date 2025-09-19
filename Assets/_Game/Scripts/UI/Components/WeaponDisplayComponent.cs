using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDisplayComponent : MonoBehaviour
{
    public Action<WeaponData> _OnWeaponSelect;
    public List<WeaponData> datas;

    [SerializeField]
    Transform weaponPoint;

    int currentIndex = -1;
    public int CurrentIndex => currentIndex;
    WeaponModel currentModel;

    public void Init()
    {
        currentIndex = -1;
        currentModel = null;
    }

    public void ShowNextWeapon()
    {
        if (currentIndex >= datas.Count - 1) return;
        ShowWeapon(currentIndex + 1);
    }

    public void ShowPreviousWeapon()
    {
        if (currentIndex <= 0) return;
        ShowWeapon(currentIndex - 1);
    }

    public void ShowWeapon(int index)
    {
        if (currentIndex == index) return;

        if (currentIndex >= 0)
        {
            Destroy(currentModel.gameObject);
        }

        currentIndex = index;
        currentModel = Instantiate(datas[currentIndex].model, weaponPoint);
        _OnWeaponSelect?.Invoke(datas[currentIndex]);
    }

    public WeaponData GetWeaponData(int index)
    {
        return datas[index];
    }
}

[Serializable]
public class WeaponData
{
    public string name;
    public WeaponModel model;
    public bool isLock;
    public int cost;
    public string description;
}
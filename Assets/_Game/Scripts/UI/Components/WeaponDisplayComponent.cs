using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDisplayComponent : MonoBehaviour
{
    public Action<Weapon> _OnWeaponSelect;

    [SerializeField]
    Transform weaponPoint;

    List<Weapon> datas;
    List<GameObject> models;
    int currentIndex = -1;
    GameObject currentModel;

    public void Init()
    {
        currentIndex = -1;
        currentModel = null;

        datas = DataManager.Ins.Get<ShopData>().GetItems<Weapon>(SHOP.WEAPON);
        models = DataManager.Ins.Get<EquipmentData>().weapons;
    }

    public void ShowNextWeapon()
    {
        if (currentIndex >= models.Count - 1) return;
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
            Destroy(currentModel);
        }

        currentIndex = index;
        GameObject pref = models[currentIndex];
        currentModel = Instantiate(pref, weaponPoint);

        _OnWeaponSelect?.Invoke(datas[currentIndex]);
    }
}

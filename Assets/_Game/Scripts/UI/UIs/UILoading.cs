using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using URandom = UnityEngine.Random;

public class UILoading : UICanvas
{
    [SerializeField]
    float loadingTime;
    float timer;
    bool isOpen;

    [SerializeField]
    Image _char, weapon;
    [SerializeField]
    Sprite[] charImg, weaponImg;

    Action constructLvl;
    Action openMainMenu;

    public override void Open(object param = null)
    {
        base.Open(param);

        if (param is Action[] act)
        {
            constructLvl = act[0];
            openMainMenu = act[1];
        }

        timer = Time.time;
        isOpen = true;

        _char.sprite = charImg[URandom.Range(0, charImg.Length)];
        weapon.sprite = weaponImg[URandom.Range(0, weaponImg.Length)];

        constructLvl.Invoke();
    }

    public override void Hide()
    {
        base.Hide();
        isOpen = false;
    }

    void Update()
    {
        if (!isOpen) return;
        if (Time.time >= timer + loadingTime)
        {
            Hide();
            openMainMenu.Invoke();
        }

        weapon.transform.Rotate(Vector3.forward, -360 * Time.deltaTime);
    }
}

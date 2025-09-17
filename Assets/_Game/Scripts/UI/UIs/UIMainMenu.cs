using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    [SerializeField]
    UIButton playBtn, skinShop, weaponShop;

    [SerializeField]
    UIButton[] functionBtn;

    void Awake()
    {
        playBtn._OnClick += OnPlayButtonClick;
        skinShop._OnClick += OnSkinShopBtnClick;
        weaponShop._OnClick += OnWeaponShopBtnClick;

        functionBtn.ForEach(x => x._OnClick += OnFuntionBtnClick);
    }

    void OnDestroy()
    {
        playBtn._OnClick -= OnPlayButtonClick;
        skinShop._OnClick -= OnSkinShopBtnClick;
        weaponShop._OnClick -= OnWeaponShopBtnClick;

        functionBtn.ForEach(x => x._OnClick -= OnFuntionBtnClick);
    }

    public override void Open(object param = null)
    {
        base.Open(param);

        GameplayManager.Ins.Player.ChangeState(STATE.IDLE);
    }

    void OnPlayButtonClick(int index)
    {
        Hide();
        GameplayManager.Ins.StartLevel();
    }

    void OnSkinShopBtnClick(int index)
    {
        Hide();
        UIManager.Ins.OpenUI<UIShopSkin>();
    }

    void OnWeaponShopBtnClick(int index)
    {

    }

    void OnFuntionBtnClick(int index)
    {
        int currentState = (int)functionBtn[index].State;
        functionBtn[index].SetState(currentState == 0 ? 1 : 0);
    }
}

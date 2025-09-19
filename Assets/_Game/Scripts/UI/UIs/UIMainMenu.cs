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
        skinShop._OnClick += OnShopBtnClick;
        weaponShop._OnClick += OnShopBtnClick;

        functionBtn.ForEach(x => x._OnClick += OnFuntionBtnClick);
    }

    void OnDestroy()
    {
        playBtn._OnClick -= OnPlayButtonClick;
        skinShop._OnClick -= OnShopBtnClick;
        weaponShop._OnClick -= OnShopBtnClick;

        functionBtn.ForEach(x => x._OnClick -= OnFuntionBtnClick);
    }

    public override void Open(object param = null)
    {
        base.Open(param);

        GameplayManager.Ins.mainCam.ChangeCamera(CAMERA_TYPE.MENU);
    }

    void OnPlayButtonClick(int index)
    {
        Hide();
        UIManager.Ins.OpenUI<UIGameplay>();
        GameplayManager.Ins.StartLevel();
    }

    void OnShopBtnClick(int index)
    {
        Hide();
        switch (index)
        {
            case 0:
                UIManager.Ins.OpenUI<UIShopSkin>();
                break;
            case 1:
                UIManager.Ins.OpenUI<UIShopWeapon>();
                break;
        }
    }

    void OnFuntionBtnClick(int index)
    {
        int currentState = (int)functionBtn[index].State;
        functionBtn[index].SetState(currentState == 0 ? 1 : 0);
    }
}

using System;
using Sirenix.Utilities;
using UnityEngine;

public class UISetting : UICanvas
{
    [SerializeField]
    UIButton continueBtn, mainMenuBtn;
    [SerializeField]
    UIButton[] funcBtn;

    void Awake()
    {
        continueBtn._OnClick += OnContinueBtnClick;
        mainMenuBtn._OnClick += OnMainMenuBtnClick;
        funcBtn.ForEach(x => x._OnClick += OnFuncBtnClick);
    }

    void OnDestroy()
    {
        continueBtn._OnClick -= OnContinueBtnClick;
        mainMenuBtn._OnClick -= OnMainMenuBtnClick;
        funcBtn.ForEach(x => x._OnClick -= OnFuncBtnClick);
    }

    public override void UpdateUI()
    {
        base.UpdateUI();
        funcBtn.ForEach(x => x.SetState(1));
    }

    void OnFuncBtnClick(int index)
    {
        int currentState = (int)funcBtn[index].State;
        funcBtn[index].SetState(currentState == 0 ? 1 : 0);
    }

    void OnContinueBtnClick(int index)
    {
        Hide();
        UIManager.Ins.OpenUI<UIGameplay>();
    }

    void OnMainMenuBtnClick(int index)
    {
        Hide();
        Action[] actions = new Action[]{
            GameplayManager.Ins.ReconstructLevel,
            ()=>UIManager.Ins.OpenUI<UIMainMenu>()
        };
        UIManager.Ins.OpenUI<UILoading>(actions);
    }
}

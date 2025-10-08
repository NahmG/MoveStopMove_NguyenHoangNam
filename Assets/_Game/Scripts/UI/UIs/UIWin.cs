using System;
using TMPro;
using UnityEngine;

public class UIWin : UICanvas
{
    [SerializeField]
    UIButton rewardBtn, continueBtn;
    [SerializeField]
    TMP_Text gold;

    float timer;

    void Awake()
    {
        rewardBtn._OnClick += OnRewardBtnClick;
        continueBtn._OnClick += OnContinueBtnClick;
    }

    void OnDestroy()
    {
        rewardBtn._OnClick -= OnRewardBtnClick;
        continueBtn._OnClick -= OnContinueBtnClick;
    }

    public override void Open(object param = null)
    {
        base.Open(param);
        timer = 2f;
        continueBtn.gameObject.SetActive(false);

        gold.text = $"{GameplayManager.Ins.Player.Stats.Level.Value}";
    }

    void Update()
    {
        if (timer <= 0)
        {
            continueBtn.gameObject.SetActive(true);
            return;
        }
        timer -= Time.deltaTime;
    }

    void OnRewardBtnClick(int index)
    {

    }

    void OnContinueBtnClick(int index)
    {
        Hide();
        Action[] actions = new Action[]{
            GameplayManager.Ins.ReconstructLevel,
            ()=>UIManager.Ins.OpenUI<UIMainMenu>()
    };
        UIManager.Ins.OpenUI<UILoading>(actions);
    }
}

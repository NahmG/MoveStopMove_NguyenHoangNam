using TMPro;
using UnityEngine;

public class UIWin : UICanvas
{
    [Header("Ref")]
    [SerializeField]
    TMP_Text brickCount;
    [SerializeField]
    UIButton nextLvl;
    [SerializeField]
    UIButton replay;

    void Awake()
    {
        nextLvl._OnClick += OnNextLvlBtnClick;
        replay._OnClick += OnReplayBtnClick;
    }

    void OnDestroy()
    {
        nextLvl._OnClick -= OnNextLvlBtnClick;
        replay._OnClick -= OnReplayBtnClick;
    }

    public override void Open(object param = null)
    {
        base.Open(param);

    }

    void OnNextLvlBtnClick(int index)
    {
        Hide();
        UIManager.Ins.OpenUI<UILoading>();
    }

    void OnReplayBtnClick(int index)
    {
        Hide();
        UIManager.Ins.OpenUI<UILoading>();
    }
}

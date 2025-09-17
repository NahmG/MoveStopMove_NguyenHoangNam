using UnityEngine;

public class UILose : UICanvas
{
    [Header("Ref")]
    [SerializeField]
    UIButton replayBtn;

    void Awake()
    {
        replayBtn._OnClick += OnReplayBtnClick;
    }

    void OnDestroy()
    {
        replayBtn._OnClick -= OnReplayBtnClick;
    }

    void OnReplayBtnClick(int index)
    {
        Hide();
        UIManager.Ins.OpenUI<UILoading>();
    }
}

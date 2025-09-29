using System.Threading;
using TMPro;
using UnityEngine;

public class UIRevive : UICanvas
{
    [SerializeField]
    TMP_Text CDText;
    [SerializeField]
    RectTransform CDImg;
    [SerializeField]
    UIButton reviveBtn, closeBtn;

    float timer;

    void Awake()
    {
        reviveBtn._OnClick += OnReviveBtnClick;
        closeBtn._OnClick += OnCloseBtnClick;
    }

    void OnDestroy()
    {
        reviveBtn._OnClick -= OnReviveBtnClick;
        closeBtn._OnClick -= OnCloseBtnClick;
    }

    public override void Open(object param = null)
    {
        base.Open(param);
        timer = 5f;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Hide();
            UIManager.Ins.OpenUI<UILose>();
        }

        CDImg.Rotate(-360 * Time.deltaTime * Vector3.forward);
        CDText.text = $"{(int)timer + 1}";
    }

    void OnReviveBtnClick(int index)
    {
        Hide();
        GameplayManager.Ins.RevivePlayer();
    }

    void OnCloseBtnClick(int index)
    {
        Hide();
        UIManager.Ins.OpenUI<UILose>();
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    [SerializeField]
    UIButton playBtn;

    void Awake()
    {
        playBtn._OnClick += OnPlayButtonClick;
    }

    void OnDestroy()
    {
        playBtn._OnClick -= OnPlayButtonClick;
    }

    void OnPlayButtonClick()
    {
        Hide();
        UIManager.Ins.OpenUI<UILoading>();
    }
}

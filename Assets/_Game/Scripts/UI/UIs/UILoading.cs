using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILoading : UICanvas
{
    [SerializeField]
    TMP_Text countText;

    float count;
    bool isOpen;

    public override void Open(object param = null)
    {
        base.Open(param);

        count = 4;
        GameplayManager.Ins.ReconstructLevel();
        isOpen = true;
    }

    public override void Hide()
    {
        base.Hide();
        isOpen = false;

    }

    void Update()
    {
        if (!isOpen) return;

        count -= Time.deltaTime;
        if (count <= 0)
        {
            Hide();
            GameplayManager.Ins.StartLevel();
        }

        countText.text = $"{(int)count}";
    }
}

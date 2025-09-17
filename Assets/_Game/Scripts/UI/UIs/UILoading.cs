using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILoading : UICanvas
{
    [SerializeField]
    float loadingTime;
    float timer;
    bool isOpen;

    public override void Open(object param = null)
    {
        base.Open(param);

        timer = Time.time;
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
        if (Time.time >= timer + loadingTime)
        {
            Hide();
        }
    }
}

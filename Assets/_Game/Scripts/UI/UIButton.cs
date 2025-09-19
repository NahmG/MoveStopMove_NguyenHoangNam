using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public Action<int> _OnClick;

    public enum STATE
    {
        DISABLE = 0,
        OPENING = 1,
        SELECTING = 2,
        CLOSING = 3,
    }

    [SerializeField] Button button;
    [SerializeField] protected int index;
    [SerializeField] bool hasComponent;

    [ShowIfGroup("Extra", Condition = "hasComponent")]
    [ShowIfGroup("Extra")]
    [SerializeField] TMP_Text textButton;
    [ShowIfGroup("Extra")]
    [SerializeField] protected UIButtonComponent[] buttonComponents;

    STATE state;
    public STATE State => state;

    void Awake()
    {
        button.onClick.AddListener(OnBtnClick);
    }

    void OnDestroy()
    {
        button.onClick.RemoveListener(OnBtnClick);
    }

    public void SetData(string text)
    {
        if (textButton != null)
            textButton.text = text;
    }

    public void SetState(STATE state)
    {
        this.state = state;
        for (int i = 0; i < buttonComponents.Length; i++)
        {
            buttonComponents[i].SetState(state);
        }
    }

    public void SetState(int state)
    {
        this.state = (STATE)state;
        for (int i = 0; i < buttonComponents.Length; i++)
        {
            buttonComponents[i].SetState(this.state);
        }
    }

    public void SetInteractive(bool state)
    {
        button.interactable = state;
    }

    void OnBtnClick()
    {
        //play sound
        _OnClick?.Invoke(index);
    }
}


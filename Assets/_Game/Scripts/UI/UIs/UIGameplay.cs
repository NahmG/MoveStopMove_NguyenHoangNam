using TMPro;
using UnityEngine;

public class UIGameplay : UICanvas
{
    [SerializeField]
    TMP_Text enemyCountText;
    [SerializeField]
    UIButton settingBtn;

    void Awake()
    {
        settingBtn._OnClick += OnSettingBtnClick;
    }

    void OnDestroy()
    {
        settingBtn._OnClick -= OnSettingBtnClick;
    }

    void Update()
    {
        enemyCountText.text = $"{EnemySpawn.Ins.CurrentEnemyCount}";
    }

    void OnSettingBtnClick(int index)
    {

    }
}

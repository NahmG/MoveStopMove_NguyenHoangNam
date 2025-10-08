using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameplay : UICanvas
{
    [SerializeField]
    TMP_Text enemyCountText;
    [SerializeField]
    UIButton settingBtn;
    [SerializeField]
    Canvas canvas;

    CameraFollow mainCam;
    CameraFollow MainCam
    {
        get
        {
            mainCam ??= GameplayManager.Ins.mainCam;
            return mainCam;
        }
    }

    void Awake()
    {
        settingBtn._OnClick += OnSettingBtnClick;
        EnemySpawn.Ins._OnEnemySpawn += SpawnIndicator;
        EnemySpawn.Ins._OnEnemyDespawn += DespawnIndicator;
    }

    void OnDestroy()
    {
        settingBtn._OnClick -= OnSettingBtnClick;
        EnemySpawn.Ins._OnEnemySpawn -= SpawnIndicator;
        EnemySpawn.Ins._OnEnemyDespawn -= DespawnIndicator;
    }

    public override void Open(object param = null)
    {
        base.Open(param);
        MainCam.ChangeCamera(CAMERA_TYPE.GAME_PLAY);
    }

    void Update()
    {
        enemyCountText.text = $"{EnemySpawn.Ins.CurrentEnemyCount}";
    }

    void LateUpdate()
    {
        UpdateAllIndicator();
    }

    void OnSettingBtnClick(int index)
    {
        Hide();
        UIManager.Ins.OpenUI<UISetting>();
    }

    #region INDICATOR
    [Header("Indicator")]
    [SerializeField] TargetIndicator indicatorPref;
    List<TargetIndicator> indicators = new();

    public void SpawnIndicator(Character target)
    {
        TargetIndicator indicator = null;
        foreach (var ind in indicators)
        {
            if (ind.Target == target)
            {
                indicator = ind;
                break;
            }
        }

        if (indicator == null)
        {
            //spawn new
            indicator = HBPool.Spawn<TargetIndicator>(PoolType.INDICATOR, Vector3.zero, Quaternion.identity);
            indicator.transform.SetParent(transform);
            indicators.Add(indicator);
        }

        indicator.OnInit(target, MainCam.cam, canvas, target.Core.DISPLAY.Color);
    }

    public void DespawnIndicator(Character target)
    {
        foreach (var ind in indicators)
        {
            if (ind.Target == target)
            {
                //despawn
                HBPool.Despawn(ind);
                indicators.Remove(ind);
                break;
            }
        }
    }

    public void UpdateAllIndicator()
    {
        foreach (var ind in indicators)
        {
            ind.UpdateIndicator();
        }
    }

    public void DespawnAllIndicator()
    {
        foreach (var ind in indicators)
        {
            HBPool.Despawn(ind);
        }
        indicators.Clear();
    }
    #endregion
}

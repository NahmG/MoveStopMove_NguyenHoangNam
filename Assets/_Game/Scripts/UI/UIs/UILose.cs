using TMPro;
using UnityEngine;

public class UILose : UICanvas
{
    [SerializeField]
    UIButton rewardBtn, continueBtn;

    [SerializeField]
    TMP_Text rank, killer, gold;

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

        rank.text = $"#{EnemySpawn.Ins.CurrentEnemyCount}";
        gold.text = $"{GameplayManager.Ins.Player.Stats.Level.Value}";
        killer.text = $"";
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
        GameplayManager.Ins.LoadGame();
    }
}

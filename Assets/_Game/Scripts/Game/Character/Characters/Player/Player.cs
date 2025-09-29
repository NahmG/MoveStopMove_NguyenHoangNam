using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class Player : Character
{
    [SerializeField]
    Transform rangeImg;
    PlayerData playerData;
    public PlayerData PlayerData
    {
        get
        {
            if (playerData == null)
            {
                playerData = DataManager.Ins.Get<PlayerData>();
            }
            return playerData;
        }
    }

    public override int WeaponId
    {
        get => PlayerData.itemIds[(int)SHOP.WEAPON];
    }

    protected override void Awake()
    {
        base.Awake();
        rangeImg.gameObject.SetActive(false);
    }

    public override void OnInit(CharacterStats stats = null)
    {
        base.OnInit(stats);
        TF.position = Vector3.zero;
        Core.DISPLAY.SetSkinRotation(new Vector3(0, 180, 0), true);
    }

    public override void Run()
    {
        base.Run();
        rangeImg.gameObject.SetActive(true);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        rangeImg.gameObject.SetActive(false);
        GameplayManager.Ins.OnGameEnd(false);
    }

    public void OnRevive()
    {
        Core.Initialize(Stats);
        Stats.HP.Reset();
        Run();
    }

    public override void OnDeath()
    {
        base.OnDeath();

        StartNavigation(false);
        Core.MOVEMENT.StopMovement();
        Core.DISPLAY.SetSkinRotation(Quaternion.identity, true);
    }

    public override void OnLevelUp(float targetLevel)
    {
        base.OnLevelUp(targetLevel);

        float mult = LevelToMultiplier(Stats.Level.Value);
        //change camera position
        GameplayManager.Ins.mainCam.Scale(mult);
        Debug.Log("Scale cam: " + mult);
    }
}

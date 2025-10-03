using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class Player : Character
{
    PlayerData playerData;
    public PlayerData PlayerData
    {
        get
        {
            if (playerData == null)
                playerData = DataManager.Ins.Get<PlayerData>();
            return playerData;
        }
    }

    public override int WeaponId
    {
        get => PlayerData.itemId[(int)SHOP.WEAPON];
    }

    protected override void Awake()
    {
        base.Awake();
        Core.DISPLAY.TurnIndicator(false);
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
        Core.DISPLAY.TurnIndicator(true);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        StartNavigation(false);
        Core.isInit = false;
        Core.DISPLAY.TurnIndicator(false);
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

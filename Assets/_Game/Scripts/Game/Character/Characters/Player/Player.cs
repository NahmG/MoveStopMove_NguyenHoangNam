using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class Player : Character
{
    [SerializeField]
    Transform rangeImg;

    protected override void Awake()
    {
        base.Awake();
        rangeImg.gameObject.SetActive(false);
    }

    public override void OnInit(CharacterStats stats = null)
    {
        base.OnInit(stats);
    }

    public override void Run()
    {
        base.Run();
        rangeImg.gameObject.SetActive(true);
    }

    public override void OnDeath()
    {
        base.OnDeath();

        StartNavigation(false);
        Core.MOVEMENT.StopMovement();

        Core.DISPLAY.SetSkinRotation(Quaternion.identity, true);

        rangeImg.gameObject.SetActive(false);
    }

    public override void OnLevelUp(float targetLevel)
    {
        base.OnLevelUp(targetLevel);

        float mult = LevelToMultiplier(Stats.Level.Value);

        //change camera position
        GameplayManager.Ins.mainCam.Scale(mult);
    }
}

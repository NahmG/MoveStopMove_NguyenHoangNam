using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

using Core.Movement;
public class Player : Character
{
    bool _isBlock;
    public bool IsBlock
    {
        get => _isBlock;
        set
        {
            _isBlock = value;
            (Core.MOVEMENT as PlayerMovement).TriggerBlockMovement(value);
        }
    }


    public override void OnInit(CharacterStats stats = null)
    {
        base.OnInit(stats);
        StartNavigation(true);
    }

    public override void OnDeath()
    {
        base.OnDeath();

        StartNavigation(false);
        Core.MOVEMENT.StopMovement();

        Core.DISPLAY.SetSkinRotation(Quaternion.identity, true);
    }
}

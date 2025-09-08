using UnityEngine;

public class BrickOnBridge : Brick
{
    [SerializeField]
    Collider col;
    [HideInInspector]
    public bool isPlaced = false;

    protected override void Awake()
    {
        base.Awake();
        Disable();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            if (!character.Core.SENSOR.IsGoUpBridge) return;

            if (!isPlaced || character.Color != Color)
            {
                if (character.BrickCount > 0)
                {
                    character.RemoveBrick();
                    Enable();
                    ChangeColor(character.Color);
                }
                else
                {
                    //stop moving
                    if (character is Player player)
                        player.IsBlock = true;

                    if (character is Enemy enemy)
                    {
                        enemy.Core.MOVEMENT.StopMovement();
                        enemy.Core.stateMachine.ChangeState(STATE.FIND_BRICK);
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            if (!isPlaced || character.Color != Color)
            {
                if (character is Player player)
                    player.IsBlock = false;
            }
        }
    }

    void Enable()
    {
        isPlaced = true;
        SkinTF.gameObject.SetActive(true);
    }

    void Disable()
    {
        isPlaced = false;
        SkinTF.gameObject.SetActive(false);
    }
}
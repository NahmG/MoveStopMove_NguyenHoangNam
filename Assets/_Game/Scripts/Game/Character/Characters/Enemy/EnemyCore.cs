using UnityEngine;

namespace Core
{
    public class EnemyCore : CoreSystem
    {
        public override void Initialize(CharacterStats stats)
        {
            base.Initialize(stats);

            stateMachine = new StateMachine();
            // stateMachine.IsDebug = true;

            stateMachine.AddState(STATE.FIND_BRICK, new EnemyFindBrickState(this));
            stateMachine.AddState(STATE.BUILD_BRIDGE, new EnemyBuildBridgeState(this));
            stateMachine.AddState(STATE.FIND_DOOR, new EnemyFindDoorState(this));
            stateMachine.AddState(STATE.GO_TO_FINISH_LINE, new EnemyGoToFinishLineState(this));
            stateMachine.AddState(STATE.DEAD, new EnemyDeadState(this));

            stateMachine.Start(STATE.FIND_DOOR);
        }

        public override void UpdateData()
        {
            base.UpdateData();
            stateMachine.Update();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            stateMachine.FixedUpdate();
        }
    }
}
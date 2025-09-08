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
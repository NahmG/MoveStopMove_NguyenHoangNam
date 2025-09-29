using UnityEngine;

namespace Core.Movement
{
    public class PlayerMovement : MovementCore
    {
        [SerializeField]
        Transform TF;
        Vector3 velocity;

        public override void Initialize(CoreSystem core)
        {
            base.Initialize(core);
            velocity = Vector3.zero;
        }

        public override void UpdateData()
        {
            base.UpdateData();
            TF.Translate(velocity * Time.deltaTime);
        }

        public override void SetVelocity(Vector3 velocity)
        {
            this.velocity = velocity;
        }

        public override void StopMovement()
        {
            velocity = Vector3.zero;
        }
    }
}

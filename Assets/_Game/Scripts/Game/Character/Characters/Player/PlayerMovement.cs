using UnityEngine;

namespace Core.Movement
{
    public class PlayerMovement : MovementCore
    {
        [SerializeField]
        Rigidbody Rb;
        Vector3 velocity;

        public override void FixedUpdateData()
        {
            base.FixedUpdateData();

            Rb.linearVelocity = velocity;
        }

        public override void SetVelocity(Vector3 velocity)
        {
            this.velocity = velocity;
        }
    }
}

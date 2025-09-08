using UnityEngine;

namespace Core.Movement
{
    public class PlayerMovement : MovementCore
    {
        [SerializeField]
        CharacterController controller;
        [SerializeField]
        float gravityScale;

        Vector3 velocity;
        readonly float gravity = -9.81f;
        bool isBlock = false;

        public override void UpdateData()
        {
            base.UpdateData();

            if (isBlock && velocity.z > 0)
            {
                velocity.z = 0;
            }
            if (controller.enabled)
                controller.Move(velocity * Time.deltaTime);
        }

        public override void SetVelocity(Vector3 velocity)
        {
            this.velocity = velocity;
        }

        public override void ApplyGravity(float scale)
        {
            velocity += scale * gravity * Vector3.up;
        }

        public override void StopMovement()
        {
            ApplyGravity(0);
            SetVelocity(Vector3.zero);

        }

        public void TriggerBlockMovement(bool state)
        {
            isBlock = state;
        }
    }
}

using UnityEngine;

namespace Core.Movement
{
    public abstract class MovementCore : BaseCore
    {
        public virtual void SetVelocity(Vector3 velocity) { }
        public virtual void ApplyGravity(float scale) { }
        public virtual void SetDestination(Vector3 destination) { }
        public virtual void StopMovement() { }
    }
}

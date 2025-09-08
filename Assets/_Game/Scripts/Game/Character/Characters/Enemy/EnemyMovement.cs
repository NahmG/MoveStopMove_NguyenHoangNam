using UnityEngine;
using UnityEngine.AI;

namespace Core.Movement
{
    public class EnemyMovement : MovementCore
    {
        [SerializeField]
        NavMeshAgent agent;

        public override void UpdateData()
        {
            base.UpdateData();
        }

        public override void SetDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
        }

        public override void StopMovement()
        {
            base.StopMovement();

            agent.velocity = Vector3.zero;
            agent.ResetPath();
        }
    }
}
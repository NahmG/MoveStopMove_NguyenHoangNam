using UnityEngine;
using UnityEngine.AI;

namespace Core.Navigation
{
    public class EnemyNavigation : NavigationCore
    {
        [SerializeField]
        Enemy _enemy;
        [SerializeField]
        NavMeshAgent agent;

        bool isRunning = false;

        override public void UpdateData()
        {
            base.UpdateData();

            Vector3 dir = agent.velocity.normalized;
            dir.y = 0;

            MoveDirection = dir;
        }

        public override void StartNavigation()
        {
            isRunning = true;
            agent.enabled = true;
        }

        public override void StopNavigation()
        {
            isRunning = false;
            _enemy.Core.MOVEMENT.StopMovement();
            agent.enabled = false;
        }

        public void FindNearestBrick()
        {
            if (!isRunning) return;
            Destination = _enemy.FindNearestBrick();
        }

        public void FindNearestBridge()
        {
            if (!isRunning) return;
            Destination = _enemy.FindNearestBridge();
        }

        public void FindNearestDoor()
        {
            if (!isRunning) return;
            Destination = _enemy.FindNearestDoor();
        }

        public void FindFinishLine()
        {
            if (!isRunning) return;
            Destination = _enemy.FindFinishLine();
        }

        public bool ReachedDestination()
        {
            return agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending;
        }
    }

}
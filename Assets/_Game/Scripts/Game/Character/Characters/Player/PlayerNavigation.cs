using UnityEngine;

namespace Core.Navigation
{
    public class PlayerNavigation : NavigationCore
    {
        [SerializeField]
        Joystick stick;

        bool isRunning;

        public override void UpdateData()
        {
            if (!isRunning) return;

            float horizontal = stick.Horizontal;
            float vertical = stick.Vertical;

            Vector3 move = new(horizontal, 0, vertical);

            MoveDirection = move.normalized;
        }

        public override void StartNavigation()
        {
            isRunning = true;
        }

        public override void StopNavigation()
        {
            isRunning = false;
        }
    }
}

using UnityEngine;

namespace Core.Navigation
{
    public abstract class NavigationCore : BaseCore
    {
        public Vector3 MoveDirection { get; internal set; }
        public Vector3 Destination { get; internal set; }

        public virtual void StartNavigation() { }
        public virtual void StopNavigation() { }
    }
}

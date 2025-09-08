using System.Collections.Generic;
using UnityEngine;

namespace Core.Sensor
{
    using Navigation;
    public class SensorCore : BaseCore
    {
        [SerializeField]
        List<BaseSensor> sensors;

        // ----------- PARAM ---------------
        [field: SerializeField]
        public NavigationCore Navigation { get; protected set; }

        // ----------- DATA --------------
        public bool IsGrounded { get; internal set; }
        public bool IsGoUpBridge { get; internal set; }

        public void ReceiveInfo(NavigationCore Navigation)
        {
            this.Navigation = Navigation;
        }

        public override void Initialize(CoreSystem core)
        {
            base.Initialize(core);

            foreach (var sensor in sensors)
            {
                sensor.Initialize(this, Navigation);
            }
        }

        public override void UpdateData()
        {
            base.UpdateData();

            foreach (var sensor in sensors)
            {
                sensor.UpdateData();
            }
        }
    }
}

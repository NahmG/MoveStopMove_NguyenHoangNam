using UnityEngine;

namespace Core.Sensor
{
    public class DetectBridge : BaseSensor
    {
        [SerializeField]
        Transform bridgeCheck;
        [SerializeField]
        float checkDistance;

        public override void UpdateData()
        {
            base.UpdateData();

            Sensor.IsGoUpBridge = Physics.Raycast(bridgeCheck.position, Navigation.MoveDirection, checkDistance, layer);
        }
    }
}

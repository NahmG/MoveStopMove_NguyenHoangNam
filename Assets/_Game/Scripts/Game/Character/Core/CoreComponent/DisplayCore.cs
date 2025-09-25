using UnityEngine;

namespace Core.Display
{
    public class DisplayCore : BaseCore
    {
        [field: SerializeField]
        public float AtkDuration { get; private set; }
        [field: SerializeField]
        public float DeadDuration { get; private set; }
        [field: SerializeField]
        public CharacterEquipment Equipment { get; private set; }
        [SerializeField]
        Animator anim;
        [SerializeField]
        Transform skinTf;
        [SerializeField]
        Transform sensorTf;
        string currentAnim = "Idle";

        float _scale;
        public float Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                skinTf.localScale = Vector3.one * _scale;
            }
        }

        public override void Initialize(CoreSystem core)
        {
            base.Initialize(core);
            Scale = 1;
            Equipment?.Initialize();
        }

        public void SetSkinRotation(Quaternion rotation, bool isLocal)
        {
            if (isLocal)
            {
                skinTf.localRotation = rotation;
                sensorTf.localRotation = rotation;
            }
            else
            {
                skinTf.rotation = rotation;
                sensorTf.localRotation = rotation;
            }
        }

        public void ChangeAnim(string animName)
        {
            if (animName != currentAnim)
            {
                anim.ResetTrigger(currentAnim);
                currentAnim = animName;
                anim.SetTrigger(currentAnim);
            }
        }

        public void ResetAnim()
        {
            anim.ResetTrigger(currentAnim);
        }

        public void ChangeColor(COLOR color)
        {

        }
    }
}

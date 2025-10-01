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

        public Color Color { get; private set; }

        public override void Initialize(CoreSystem core)
        {
            base.Initialize(core);
            Equipment?.Initialize();
            Color = Equipment?.MainColor == Color.white ? Color.black : Equipment.MainColor;
        }

        public void SetSkinRotation(Vector3 vector, bool isLocal)
        {
            Quaternion qua = Quaternion.Euler(vector);
            SetSkinRotation(qua, isLocal);
        }

        public void SetSkinRotation(Quaternion quaternion, bool isLocal)
        {
            if (isLocal)
            {
                skinTf.localRotation = quaternion;
                sensorTf.localRotation = quaternion;
            }
            else
            {
                skinTf.rotation = quaternion;
                sensorTf.localRotation = quaternion;
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

    }
}

using UnityEngine;

namespace Core.Display
{
    public class DisplayCore : BaseCore
    {
        [SerializeField]
        Animator anim;
        [SerializeField]
        Transform skinTf;
        [SerializeField]
        Transform sensorTf;

        string currentAnim;

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
            var colorData = Resources.Load<ColorData>("ColorData");
            Color c = Color.white;
            switch (color)
            {
                case COLOR.BLUE:
                    c = colorData.blue;
                    break;
                case COLOR.RED:
                    c = colorData.red;
                    break;
                case COLOR.YELLOW:
                    c = colorData.yellow;
                    break;
                case COLOR.GREEN:
                    c = colorData.green;
                    break;
                default:
                    break;
            }
            foreach (var r in skinTf.GetComponentsInChildren<Renderer>())
            {
                foreach (var m in r.materials)
                {
                    m.color = c;
                }
            }
        }
    }
}

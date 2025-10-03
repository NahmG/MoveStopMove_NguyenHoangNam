using DG.Tweening;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField]
    Transform TF;
    BoosterController controller;

    float height = 3f;

    public void Init(BoosterController controller)
    {
        this.controller = controller;

        Vector3 pos = TF.position;
        pos.y = height;
        TF.position = pos;

        AnimationDrop();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character _char))
        {
            _char.BoosterUp();
            OnDespawn();
        }
    }

    void OnDespawn()
    {
        controller.Spawn();
    }

    void AnimationDrop()
    {
        TF.DOMoveY(0, .3f).SetEase(Ease.InQuad);
    }
}
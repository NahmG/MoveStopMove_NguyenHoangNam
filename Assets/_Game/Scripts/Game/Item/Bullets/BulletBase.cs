using System;
using UnityEngine;

public class BulletBase : GameUnit
{
    [Header("Setting")]
    [SerializeField]
    float speed, rotateSpeed;
    [SerializeField]
    float defaultSize;
    [SerializeField]
    BulletSkin skin;

    protected Character source;
    bool isFired;
    float maxRange;
    Vector3 moveDir;
    Vector3 startpos;
    Quaternion rotation;

    float _size;
    public float Size
    {
        get => _size;
        set
        {
            _size = value;
            TF.localScale = Vector3.one * _size;
        }
    }

    public void Fire(Vector3 moveDir, Character source, float size, float range, int modelId = -1, bool isBoost = false)
    {
        isFired = true;
        skin.ShowModel(modelId);

        this.source = source;
        this.moveDir = moveDir.normalized;
        skin.transform.rotation = Quaternion.LookRotation(moveDir);

        startpos = TF.position;
        Size = size;
    }

    public void OnDespawn()
    {
        isFired = false;
        HBPool.Despawn(this);
    }

    void Update()
    {
        if (isFired)
        {
            if (Vector3.Distance(TF.position, startpos) >= maxRange)
                OnDespawn();

            transform.Translate(speed * Time.deltaTime * moveDir);
            TF.Rotate(rotateSpeed * Time.deltaTime * Vector3.up);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character _target))
        {
            OnHitTarget(_target);
            OnDespawn();
        }
    }

    void OnHitTarget(Character target)
    {
        if (target == source || target.IsDead) return;
        //OnHit
        source.OnLevelUp(target.Stats.Level.Value);
        target.OnHit(10);
    }
}
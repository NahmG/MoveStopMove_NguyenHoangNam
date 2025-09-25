using System;
using UnityEngine;

public class BulletBase : GameUnit
{
    [Header("Setting")]
    [SerializeField]
    float lifeTime;
    [SerializeField]
    float speed, rotateSpeed;
    [SerializeField]
    float defaultSize;
    [SerializeField]
    BulletSkin skin;

    protected Character source;
    bool isFired;
    float timer;
    Vector3 moveDir;
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

    public void Fire(Vector3 moveDir, Character source, float size, int modelId = -1)
    {
        isFired = true;
        skin.ShowModel(modelId);

        this.source = source;
        this.moveDir = moveDir.normalized;
        skin.transform.rotation = Quaternion.LookRotation(moveDir);

        timer = Time.time;
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
            if (Time.time >= timer + lifeTime)
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
        }
    }

    void OnHitTarget(Character target)
    {
        if (target == source || target.IsDead) return;
        //OnHit
        source.OnLevelUp(target.Stats.Level.Value);
        target.OnHit(10);
        OnDespawn();
    }
}
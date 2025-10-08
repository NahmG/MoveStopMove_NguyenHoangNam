using System;
using DG.Tweening;
using NUnit.Framework;
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
    [SerializeField]
    Collider col;

    protected Character source;
    bool isFired;
    float maxRange;
    Vector3 moveDir;
    Vector3 startpos;
    bool isBoost;
    bool isStop;

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
        isStop = false;
        col.enabled = true;

        this.source = source;
        this.moveDir = moveDir.normalized;
        skin.transform.rotation = Quaternion.LookRotation(moveDir);

        maxRange = range;
        startpos = TF.position;
        Size = size;

        skin.ShowModel(modelId);
        this.isBoost = isBoost;
        if (isBoost)
        {
            TF.DOScale(Size * 2, 1f);
        }
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

            if (!isStop)
            {
                transform.Translate(speed * Time.deltaTime * moveDir);
                TF.Rotate(rotateSpeed * Time.deltaTime * Vector3.up);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character _target))
        {
            OnHitTarget(_target);
        }

        if (other.TryGetComponent<Obstacle>(out _))
        {
            OnHitObstacle();
        }
    }

    void OnHitTarget(Character target)
    {
        if (target == source || target.IsDead) return;
        //OnHit
        source.OnLevelUp(target.Stats.Level.Value);
        target.OnHit(10);
        if (!isBoost)
            OnDespawn();
    }

    void OnHitObstacle()
    {
        isStop = true;
        col.enabled = false;

        DOVirtual.DelayedCall(1.5f, () => OnDespawn());
    }
}
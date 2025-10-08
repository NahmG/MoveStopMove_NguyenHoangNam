
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Sensor;
using UnityEngine;

public class DetectTarget : BaseSensor
{
    [SerializeField]
    Character _char;
    [SerializeField]
    LayerMask obstacle;
    Collider sourceCol;
    Character _currentTarget;
    Collider[] colliders = new Collider[10];
    List<Collider> hostileColliders = new();
    float _radius;

    void Awake()
    {
        sourceCol = _char.GetComponent<Collider>();
    }

    public override void UpdateData()
    {
        base.UpdateData();

        _radius = _char.Range;

        UpdateTargetCollection();
        UpdateTargetState();

        if (_currentTarget != null)
        {
            Vector3 dir = _currentTarget.TF.position - _char.TF.position;
            dir.y = 0;
            Sensor.TargetDir = dir.normalized;
        }
    }

    void UpdateTargetState()
    {
        if (_currentTarget == null)
        {
            GetTarget(hostileColliders);
        }
        else if (Vector3.Distance(_currentTarget.TF.position, transform.position) > (_radius + .6f) || _currentTarget.IsDead || IsBlock(_currentTarget))
        {
            RemoveTarget();
        }
    }

    void UpdateTargetCollection()
    {
        Array.Clear(colliders, 0, colliders.Length);
        hostileColliders.Clear();

        Physics.OverlapSphereNonAlloc(transform.position, _radius, colliders, layer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != null)
            {
                hostileColliders.Add(colliders[i]);
            }
        }
    }

    void GetTarget(List<Collider> cols)
    {
        if (cols.Count == 1) return;

        cols.OrderBy(x => Vector3.Distance(x.transform.position, transform.position));

        int index = 0;
        while (index < cols.Count)
        {
            if (cols[index] != sourceCol && cols[index].TryGetComponent(out Character _target) && !_target.IsDead && !IsBlock(_target))
            {
                _currentTarget = _target;
                if (_char is Player && _currentTarget is Enemy _e)
                    _e.Core.DISPLAY.TurnIndicator(true);

                break;
            }
            else index++;
        }

        Sensor.Target = _currentTarget;
    }

    bool IsBlock(Character _target)
    {
        Vector3 dir = _target.TF.position - _char.TF.position;
        return Physics.Raycast(transform.position, dir, _radius + 1, obstacle);
    }

    public void RemoveTarget()
    {
        if (_char is Player && _currentTarget is Enemy _e)
            _e.Core.DISPLAY.TurnIndicator(false);

        _currentTarget = null;
        Sensor.Target = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        float _radius = _char.Range;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
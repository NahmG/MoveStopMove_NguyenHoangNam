
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.Sensor;
using UnityEngine;

public class DetectTarget : BaseSensor
{
    [SerializeField]
    Character _char;
    Collider sourceCol;

    Character _currentTarget;
    Collider[] colliders = new Collider[10];
    List<Collider> hostileColliders = new();

    void Awake()
    {
        sourceCol = _char.GetComponent<Collider>();
    }

    public override void UpdateData()
    {
        base.UpdateData();

        UpdateTargetCollection();
        UpdateTargetState();
    }

    void UpdateTargetState()
    {
        float _radius = _char.Stats.AtkRange.Value;

        if (_currentTarget == null)
        {
            GetTarget(hostileColliders);
        }
        else if (Vector3.Distance(_currentTarget.TF.position, transform.position) > (_radius + .6f) || _currentTarget.IsDead)
        {
            RemoveTarget();
        }

        Sensor.Target = _currentTarget;
    }

    void UpdateTargetCollection()
    {
        Array.Clear(colliders, 0, colliders.Length);
        hostileColliders.Clear();

        float _radius = _char.Stats.AtkRange.Value;

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
            if (cols[index] != sourceCol && cols[index].TryGetComponent(out Character _target) && !_target.IsDead)
            {
                _currentTarget = _target;
                if (_char is Player && _currentTarget is Enemy _e)
                    _e.TurnOnIndicator(true);

                break;
            }
            else index++;
        }
    }

    public void RemoveTarget()
    {
        if (_char is Player && _currentTarget is Enemy _e)
            _e.TurnOnIndicator(false);

        _currentTarget = null;
        Sensor.Target = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        float _radius = _char.Stats.AtkRange.Value;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
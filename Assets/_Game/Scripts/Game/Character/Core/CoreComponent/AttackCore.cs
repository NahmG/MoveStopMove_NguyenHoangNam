using Core;
using Core.Sensor;
using UnityEngine;

public class AttackCore : BaseCore
{
    [Header("Ref")]
    [SerializeField]
    Character _char;
    [SerializeField]
    SensorCore _sens;

    [Header("Set up")]
    [SerializeField]
    PoolType bulletType;
    [SerializeField]
    Transform shootPoint;
    [field: SerializeField]
    public AnimationEvent Event { get; private set; }

    protected bool _isAtkCooldown;
    public bool IsAtkCooldown => _isAtkCooldown;
    float timer;

    void Awake()
    {
        Event._OnActionExecute += UseSkill;
    }

    void OnDestroy()
    {
        Event._OnActionExecute -= UseSkill;
    }

    public override void Initialize(CoreSystem core)
    {
        base.Initialize(core);
        _isAtkCooldown = false;
    }

    public override void UpdateData()
    {
        base.UpdateData();
        UpdateAtkCooldown();
    }

    public void UseSkill()
    {
        ICharacter target = _sens.Target;
        if (target == null)
        {
            Debug.LogWarning("Target escape");
            return;
        }

        Vector3 moveDir = target.TF.position - _char.TF.position;
        moveDir.y = 0;

        BulletBase bullet = HBPool.Spawn<BulletBase>(bulletType, shootPoint.position, Quaternion.identity);
        float size = _char.Core.DISPLAY.Scale;
        bullet.Fire(moveDir, _char, size);

        _isAtkCooldown = true;
        timer = Time.time;
    }

    void UpdateAtkCooldown()
    {
        if (_isAtkCooldown)
        {
            if (Time.time >= timer + _char.Stats.AtkCoolDown.Value)
                _isAtkCooldown = false;
        }
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "Status Data/Character")]
public class CharacterStats : ScriptableObject
{
    [SerializeField]
    Stat _level;
    public Stat Level => _level;
    [SerializeField]
    Stat _hp;
    public Stat HP => _hp;
    [SerializeField]
    Stat _speed;
    public Stat Speed => _speed;
    [SerializeField]
    Stat _atkCooldown;
    public Stat AtkCoolDown => _atkCooldown;
    [SerializeField]
    Stat _atkRange;
    public Stat AtkRange => _atkRange;

    public void OnInit(CharacterStats stats)
    {
        _speed = new Stat(stats.Speed.Value);
        _hp = new Stat(stats.HP.Value);
        _atkCooldown = new Stat(stats.AtkCoolDown.Value);
        _atkRange = new Stat(stats.AtkRange.Value);
        _level = new Stat(stats.Level.Value);
    }

    public void Reset()
    {
        _hp.Reset();
        _speed.Reset();
        _atkRange.Reset();
        _atkCooldown.Reset();
        _level.Reset();
    }
}
using UnityEngine;

public abstract class Brick : GameUnit
{
    [SerializeField]
    Renderer rend;
    Material _material;

    public COLOR Color { get; private set; } = COLOR.NONE;

    protected virtual void Awake() { }

    public virtual void OnInit()
    {
    }

    public virtual void OnDespawn() { }

    public void ChangeColor(COLOR color)
    {
        var data = Resources.Load<ColorData>(CONSTANTS.COLOR_DATA_PATH);

        this.Color = color;
        if (_material == null)
            _material = rend.material;

        _material.color = color switch
        {
            COLOR.BLUE => data.blue,
            COLOR.RED => data.red,
            COLOR.YELLOW => data.yellow,
            COLOR.GREEN => data.green,
            _ => UnityEngine.Color.gray,
        };
    }
}
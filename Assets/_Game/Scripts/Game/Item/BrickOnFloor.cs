using UnityEngine;

public class BrickOnFloor : Brick
{
    [SerializeField]
    float respawnTime = 5f;
    public bool IsTaken { get; private set; } = false;

    float timer;
    bool isActive = true;

    protected override void Awake()
    {
        base.Awake();

        isActive = false;
        Disable();
    }

    public override void OnInit()
    {
        base.OnInit();
        isActive = true;
        Enable();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Character>(out var character))
        {
            if (character.Color == Color)
            {
                character.AddBrick();
                Disable();
            }
        }
    }

    public void UpdateData()
    {
        if (!isActive) return;

        if (IsTaken)
        {
            timer += Time.deltaTime;
            if (timer >= respawnTime)
            {
                Enable();
                timer = 0;
            }
        }
    }

    void Enable()
    {
        IsTaken = false;
        gameObject.SetActive(true);
        timer = 0;
    }

    void Disable()
    {
        IsTaken = true;
        gameObject.SetActive(false);
    }
}
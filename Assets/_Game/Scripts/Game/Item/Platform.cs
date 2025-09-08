using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using URandom = UnityEngine.Random;

public class Platform : MonoBehaviour
{
    [SerializeField]
    Transform brickHoldPoint, doorHoldPoint, bridgeHoldPoint;
    public List<BrickOnFloor> bricksOnFloors { get; private set; } = new();
    public List<Door> doors { get; private set; } = new();
    public List<Bridge> bridges { get; private set; } = new();

    public PLATFORM_TYPE Type;

    protected List<Character> characters;
    List<COLOR> colors = new();
    int totalBrickCount;
    Dictionary<COLOR, List<BrickOnFloor>> brickDict = new();

    void Awake()
    {
        characters = GameplayManager.Ins.characters;

        bricksOnFloors = brickHoldPoint
            ? brickHoldPoint.GetComponentsInChildren<BrickOnFloor>(true).ToList()
            : new();

        doors = doorHoldPoint
            ? doorHoldPoint.GetComponentsInChildren<Door>().ToList()
            : new();

        bridges = bridgeHoldPoint
            ? bridgeHoldPoint.GetComponentsInChildren<Bridge>(true).ToList()
            : new();

        totalBrickCount = bricksOnFloors.Count;

        doors?.ForEach(door => door._OnTriggerDoor += OnTriggerDoor);
    }

    void OnDestroy()
    {
        doors?.ForEach(door => door._OnTriggerDoor -= OnTriggerDoor);
    }

    void Update()
    {
        bricksOnFloors?.ForEach(brick => brick.UpdateData());
    }

    public void OnInit()
    {
        DistributeColorToBricks();
    }

    public void OnDespawn()
    {
        brickDict.Clear();
    }

    public void ActiveBricks(COLOR color)
    {
        GetColorsById(color).ForEach(b => b.OnInit());
    }

    protected virtual void OnTriggerDoor(Character character)
    {
        COLOR _color = character.Color;

        ActiveBricks(_color);

        character.SetTargetBrick(brickDict[_color]);
        if (character is Enemy enemy)
            enemy.SetPlatform(this);
    }

    void DistributeColorToBricks()
    {
        if (brickHoldPoint == null) return;

        //get all possible colors
        List<COLOR> colorList = Enum.GetValues(typeof(COLOR)).Cast<COLOR>().ToList();

        //distribute colors
        foreach (var color in colorList)
        {
            if (color == COLOR.NONE) continue;
            for (int i = 0; i < totalBrickCount / (colorList.Count - 1); i++)
            {
                colors.Add(color);
            }
        }

        //shuffle list
        colors = UTILS.Shuffle(colors);

        //apply colors to bricks
        for (int i = 0; i < totalBrickCount; i++)
        {
            bricksOnFloors[i].ChangeColor(colors[i]);
        }
    }

    List<BrickOnFloor> GetColorsById(COLOR Id)
    {
        if (!brickDict.ContainsKey(Id))
        {
            brickDict[Id] = bricksOnFloors.Where(b => b.Color == Id).ToList();
        }
        return brickDict[Id];
    }
}

public enum PLATFORM_TYPE
{
    NONE,
    NORMAL,
    FINISH
}
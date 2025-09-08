using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<Transform> startingPos;
    [SerializeField]
    List<Platform> platforms = new(4);

    List<Character> characters = new(10);

    void Awake()
    {
        characters = GameplayManager.Ins.characters;

        foreach (var character in characters)
        {
            if (character is Enemy e)
            {
                e._OnFindNearestBridge += FindTargetBridge;
                e._OnFindNearestDoor += FindTargetDoor;
                e._OnFindFinishLine += FindFinishLine;
            }
        }
    }

    void OnDestroy()
    {
        foreach (var character in characters)
        {
            if (character is Enemy e)
            {
                e._OnFindNearestBridge -= FindTargetBridge;
                e._OnFindNearestDoor -= FindTargetDoor;
                e._OnFindFinishLine -= FindFinishLine;
            }
        }
    }

    public void OnInit()
    {
        platforms.ForEach(f => f.OnInit());
    }

    /*
    Old: Find nearest bridge
    New: Bridge have 1st brick + bridge that have most color
    */
    Vector3 FindTargetBridge(Character character, Platform platform)
    {
        List<Bridge> bridges = platform.bridges;

        List<Vector3> endPoints = bridges.ConvertAll(b => b.EndPoint.position);
        float minDist = float.MaxValue;

        int index = 0;
        for (int i = 0; i < endPoints.Count; i++)
        {
            float dist = Vector3.Distance(character.TF.position, endPoints[i]);
            if (dist < minDist)
            {
                minDist = dist;
                index = i;
            }
        }
        return endPoints[index];

        // COLOR color = character.Color;

        // //get bridge that have 1st color
        // List<Bridge> bridge1 = bridges.Where(x => x.firstStep.Color == color).ToList();

        // //reorder list
        // if (bridge1.Count > 0)
        //     return bridge1.OrderByDescending(x => x.BrickCountByColor(color)).First().EndPoint.position;
        // else
        //     return bridges[Random.Range(0, bridges.Count)].EndPoint.position;
    }

    /*
    Find nearest door to enemy
    */
    Vector3 FindTargetDoor(Character character)
    {
        List<Door> doors = new();

        for (int i = 0; i < platforms.Count; i++)
        {
            doors.AddRange(platforms[i].doors);
        }

        List<Vector3> doorPoints = doors.ConvertAll(d => d.transform.position);
        float minDist = float.MaxValue;

        int index = 0;
        for (int i = 0; i < doorPoints.Count; i++)
        {
            float dist = Vector3.Distance(character.TF.position, doorPoints[i]);
            if (dist < minDist)
            {
                minDist = dist;
                index = i;
            }
        }
        return doorPoints[index];
    }

    Vector3 FindFinishLine(Character character)
    {
        return Vector3.zero;
    }

}
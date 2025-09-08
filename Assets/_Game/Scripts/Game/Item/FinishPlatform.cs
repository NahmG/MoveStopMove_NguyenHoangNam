using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEditor.UI;
using UnityEngine;

public class FinishPlatform : Platform
{
    [Header("Finish")]
    [SerializeField]
    List<Transform> finishSpot;

    protected override void OnTriggerDoor(Character character)
    {
        PlaceCharacter();

        Invoke(nameof(EndGame), 1f);

        void EndGame()
        {
            GameplayManager.Ins.OnGameEnd(character);
        }
    }

    public void PlaceCharacter()
    {
        characters.ForEach(cha => cha.OnDeath());

        characters = characters
            .OrderBy(c => Vector3.Distance(c.TF.position, finishSpot[1].position))
            .ToList();

        for (int i = 0; i < finishSpot.Count; i++)
        {
            characters[i].TF.position = finishSpot[i].position;
            characters[i].TF.rotation = finishSpot[i].rotation;

            ChangeSpotColor(finishSpot[i], characters[i].Color);
        }
    }

    void ChangeSpotColor(Transform spot, COLOR color)
    {
        var colorData = Resources.Load<ColorData>(CONSTANTS.COLOR_DATA_PATH);

        Material material = spot.GetComponentInChildren<Renderer>().material;
        material.color = color switch
        {
            COLOR.BLUE => colorData.blue,
            COLOR.RED => colorData.red,
            COLOR.YELLOW => colorData.yellow,
            COLOR.GREEN => colorData.green,
            _ => Color.grey
        };
    }
}
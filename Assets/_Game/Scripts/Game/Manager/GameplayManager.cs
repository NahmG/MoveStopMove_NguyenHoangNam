
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    [SerializeField]
    Player playerPref;
    [SerializeField]
    CameraFollow mainCam;

    [SerializeField]
    Player player;
    public Player Player => player;
    public List<Character> characters;
    [SerializeField]
    Level currentLevel;

    bool isGameEnd;

    void Start()
    {
        LevelManager.Ins.OnInit();
        UIManager.Ins.OpenUI<UIMainMenu>();

        currentLevel = LevelManager.Ins.LoadLevel();
        SetCharacterStartPosition();
    }

    public void StartLevel()
    {
        characters.ForEach(e => e.OnInit());
    }

    public void ReconstructLevel()
    {
        DestructLevel();
        ConstructLevel();
    }

    void ConstructLevel()
    {
        isGameEnd = false;
        if (currentLevel == null)
        {
            currentLevel = LevelManager.Ins.LoadLevel();
        }
        currentLevel.OnInit();

        mainCam.SetTarget(player.TF);

        SetCharacterStartPosition();
    }

    void DestructLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
            currentLevel = null;
        }
    }

    void SetCharacterStartPosition()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].TF.position = currentLevel.startingPos[i].position;
            characters[i].TF.rotation = currentLevel.startingPos[i].rotation;
        }
    }

    public void OnGameEnd(Character character)
    {
        isGameEnd = true;
        if (character is Player)
        {
            UIManager.Ins.OpenUI<UIWin>();
        }
        else
        {
            UIManager.Ins.OpenUI<UILose>();
        }

        //set cam target first place
        mainCam.SetTarget(character.TF);
    }


}
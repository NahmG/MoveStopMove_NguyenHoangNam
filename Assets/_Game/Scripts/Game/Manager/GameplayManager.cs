
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

        StartLevel();
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
            // currentLevel = LevelManager.Ins.LoadLevel();
        }

        mainCam.SetTarget(player.TF);
    }

    void DestructLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
            currentLevel = null;
        }
    }

    public void OnGameEnd()
    {
        isGameEnd = true;
    }
}
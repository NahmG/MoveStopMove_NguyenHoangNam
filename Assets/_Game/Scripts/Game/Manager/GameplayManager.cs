
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    public CameraFollow mainCam;

    [SerializeField]
    Player player;
    public Player Player => player;
    [SerializeField]
    Level currentLevel;

    List<Character> characters = new();
    bool isGameEnd;

    void Start()
    {
        LevelManager.Ins.OnInit();
        UIManager.Ins.OpenUI<UIMainMenu>();

        mainCam.ChangeCamera(CAMERA_TYPE.MENU);

        ReconstructLevel();
    }

    public void StartLevel()
    {
        characters.ForEach(x => x.Run());
        mainCam.ChangeCamera(CAMERA_TYPE.GAME_PLAY);
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

        mainCam.SetTarget(player.TF);

        //set up character
        EnemySpawn.Ins.OnInit();
        player.OnInit();

        characters.Clear();
        characters.Add(Player);
        characters.AddRange(EnemySpawn.Ins.Enemies);
    }

    void DestructLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
            currentLevel = null;
        }

        EnemySpawn.Ins.OnDespawn();
    }

    public void OnGameEnd()
    {
        isGameEnd = true;

    }
}
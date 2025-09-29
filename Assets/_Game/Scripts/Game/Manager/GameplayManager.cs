
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

    UIGameplay uiGameplay;
    List<Character> characters = new();
    bool isGameEnd;
    bool isRevive;

    void Start()
    {
        LevelManager.Ins.OnInit();
        UIManager.Ins.OpenUI<UIMainMenu>();
        uiGameplay = UIManager.Ins.GetUI<UIGameplay>();
        uiGameplay.Close();

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
        isRevive = false;
        if (currentLevel == null)
        {
            currentLevel = LevelManager.Ins.LoadLevel();
        }
        mainCam.SetTarget(player.TF);

        //set up character
        EnemySpawn.Ins.OnInit();
        player.OnInit();
        uiGameplay.SpawnIndicator(player);

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
        uiGameplay.DespawnAllIndicator();
    }

    public void OnGameEnd()
    {
        isGameEnd = true;
        if (!isRevive)
        {
            isRevive = true;
            UIManager.Ins.OpenUI<UIRevive>();
        }
        else
        {
            UIManager.Ins.OpenUI<UILose>();
        }
    }
}
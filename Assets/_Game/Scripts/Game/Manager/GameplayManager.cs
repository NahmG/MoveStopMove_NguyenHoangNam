
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    public CameraFollow mainCam;

    [SerializeField]
    Player player;
    public Player Player => player;
    Level currentLevel;

    UIGameplay uiGameplay;
    UIMainMenu uiMainMenu;

    bool isGameEnd;
    bool isRevive;

    [SerializeField]
    BoosterController booster;
    public BoosterController Booster => booster;

    void Start()
    {
        LevelManager.Ins.OnInit();

        uiGameplay = UIManager.Ins.GetUI<UIGameplay>();
        uiGameplay.Close();
        uiMainMenu = UIManager.Ins.GetUI<UIMainMenu>();
        uiMainMenu.Close();

        Action[] actions = new Action[]{
            ReconstructLevel,
            ()=>uiMainMenu.Open()
        };
        UIManager.Ins.OpenUI<UILoading>(actions);
    }

    public void LoadGame()
    {
        uiMainMenu.Open();
        ReconstructLevel();
    }

    public void StartLevel()
    {
        player.Run();
        EnemySpawn.Ins.Run();
        uiGameplay.Open();
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
        player.OnInit();
        EnemySpawn.Ins.OnInit();

        uiGameplay?.SpawnIndicator(player);
        booster.Spawn();
    }

    void DestructLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
            currentLevel = null;
        }

        EnemySpawn.Ins.OnDespawn();
        uiGameplay?.DespawnAllIndicator();
        mainCam.Reset();
        booster?.Despawn();
        HBPool.Collect(PoolType.BULLET);
    }

    public void RevivePlayer()
    {
        player.OnRevive();
        uiGameplay.Open();
    }

    public void Pause(bool isPause)
    {

    }

    public void OnGameEnd(bool isWin)
    {
        isGameEnd = true;
        UIManager.Ins.CloseAll();
        if (isWin)
        {
            UIManager.Ins.OpenUI<UIWin>();
        }
        else
        {
            if (!isRevive)
            {
                isRevive = true;
                UIManager.Ins.OpenUI<UIRevive>();
            }
            else
                UIManager.Ins.OpenUI<UILose>();
        }
    }
}
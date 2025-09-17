using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Level[] levels;

    [HideInInspector]
    public Level currentLevel;
    int levelIndex;
    public int Level => levelIndex;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void OnInit()
    {
        levelIndex = 1;
    }

    public void NextLevel()
    {
        levelIndex = levelIndex < levels.Length ? levelIndex + 1 : levelIndex;
    }

    public Level LoadLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(GetPrefabLevel(), transform);
        return currentLevel;
    }

    Level GetPrefabLevel()
    {
        return levelIndex > 1 ? levels[levelIndex - 1] : levels[0];
    }

    public Vector3 RandomPoint()
    {
        if (currentLevel == null)
        {
            Debug.LogWarning("Level not loaded!");
            return Vector3.zero;
        }

        Vector3 pos = Vector3.zero;

        if (currentLevel.RandomPoint(out Vector3 result))
        {
            pos = result;
        }
        return pos;
    }
}
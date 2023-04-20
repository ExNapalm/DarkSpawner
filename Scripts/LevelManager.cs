using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelData[] Levels;

    //Places all Level data within one Script
    [SerializeField] public int ThisCurrentLevel;
    [SerializeField] private string CurrentLevelName;
    [SerializeField] private int CurrentLevel, CurrentSize, CurrentWaveSize, CurrentWaveAmount, CurrentEnemyTypes;
    [SerializeField] private int[] CurrentWaveBulk;

    private void Awake()
    {
        CurrentLevel = ThisCurrentLevel;

    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateLevelInfo();
    }


    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateLevelInfo()
    {
        foreach (LevelData Lvl in Levels)
        {
            if (CurrentLevel == Lvl.levelID)
            {
                CurrentLevelName = Lvl.Levelname;
                CurrentSize = Lvl.Sizelimit;
                CurrentWaveSize = Lvl.Wavesize;
                CurrentEnemyTypes = Lvl.enemyTypes;
                CurrentWaveAmount = Lvl.WaveAmount;

                Debug.Log("LevelManager: " + CurrentLevelName + "|" + CurrentSize + "|" + CurrentWaveSize + "|" + CurrentEnemyTypes + "|" + CurrentWaveAmount);

                //Divides waves based on the initial large wave
                for (int v = 0; v < CurrentWaveAmount; v++)
                {
                    CurrentWaveBulk[v] = CurrentWaveSize / (v + 1);

                }
            }
        }


    }

    //Public getters for level data. Will be used for enemy and player entity spawning
    public void DebugLevelInfo()
    {
        UpdateLevelInfo();
    }

    public int GetSizeLimit()
    {
        return CurrentSize;
    }
    public int GetWaveAmount()
    {
        return CurrentWaveAmount;
    }
    public int GetWaveSize()
    {
        return CurrentWaveSize;
    }
    public int GetEnemyTypes()
    {
        return CurrentEnemyTypes;
    }


}

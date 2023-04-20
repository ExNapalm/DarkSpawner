using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnEnemyWaves : MonoBehaviour
{
    bool StartedGame;
    string monName;

    [SerializeField] EnemySpawner Spawner;
    [SerializeField] EnemyEntrance sDoor;
    [SerializeField] DeathCounter Death;
    [SerializeField] LevelManager LevelInfo;

    int spawnedEnemies;
    int deadEnemies;
    string enemyName;

    Transform spawnpoint;

    bool SpawningStage;//Stages
    bool FullStage;

    float TimeReduction;



    // Start is called before the first frame update
    void Start()
    {
        Death = GameObject.FindGameObjectWithTag("TheEnd").GetComponent<DeathCounter>();
        LevelInfo = GameObject.FindGameObjectWithTag("LevelSystem").GetComponent<LevelManager>();
        if (!LevelInfo)
        { Debug.Log("Failed to find the Level manager...[SEW]"); }

        Spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>();
        if (!Spawner)
        { Debug.Log("Spawner failed to get the Enemy Spawn to spawn spawn"); }

        //Find the Enemy Spawn Door for when the Game Starts
        sDoor = GameObject.FindGameObjectWithTag("sDoor").GetComponent<EnemyEntrance>();
        if (!sDoor)
        { Debug.Log("Failed to find the Knight Creator...[SEW]"); }

        spawnedEnemies = 0;

        spawnpoint = GameObject.FindGameObjectWithTag("wayPoint").transform;
        if (!spawnpoint)
        { Debug.Log("Failed to find the Spawn Pointr...[SEW]"); }

        SpawningStage = false;//Stages
        FullStage = false;

        TimeReduction = 1.5f;

        StartCoroutine(CheckforGameStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedEnemies == LevelInfo.GetWaveSize())//Stop spawning if all enemies have spawned
        {
            FullStage = true;
            StopCoroutine(CheckforGameStart());
            StopCoroutine(SpawnEnemyTimer());
            Debug.Log("All " + spawnedEnemies + " have spawned.");
        }
    }


    IEnumerator SpawnEnemyTimer()
    {
        if (!FullStage)
        {
            if (LevelInfo.GetEnemyTypes() > 1)//randomize spawns if enemy types available are bigger than 1
            {
                int randomize = Random.Range(0, 12);

                if (randomize >= 9) { enemyName = "Elite Knight"; }
                else if (randomize < 9 && randomize > 5) { enemyName = "Mage"; }
                else { enemyName = "Knight"; }
            }
            else { enemyName = "Knight"; }

            yield return new WaitForSeconds((2f + 1f) / TimeReduction);

            Debug.Log("Spawning " + enemyName + " at ." + spawnpoint);
            Spawner.SpawnEnemy(enemyName, spawnpoint.transform.position + new Vector3(Random.Range(-5, 5f), Random.Range(-5, 5f), Random.Range(0, 4f)), Quaternion.identity);
            spawnedEnemies++;
            Death.SetDeath(spawnedEnemies);
            TimeReduction += 0.2f;
            StartCoroutine(SpawnEnemyTimer());
        }
        else
        {
            Debug.Log("Now finish them.");

        }
    }

    IEnumerator CheckforGameStart()//Loop until Game has Started
    {
        if (SpawningStage == false)
        {

            yield return new WaitForSeconds(3);
            if (sDoor.GetDoorActive() == true)
            {
                Death.SetStarted();
                Debug.Log("Game has started, Enemies Have started spawning, SpawnEnemyTimer Activated");
                StartCoroutine(SpawnEnemyTimer());
                SpawningStage = true;
                //Checking if the door is active to start spawning enemies
                StartCoroutine(CheckforGameStart());

            }
            else
            {

                StartCoroutine(CheckforGameStart());
            }
        }
        else
        {

            Debug.Log("Spawning Stage Has Begun.");
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class SpawnCursor : MonoBehaviour
{
    Camera Cam = null;
    public LayerMask Tilemask;
    [SerializeField] private TextMeshProUGUI sizetext = null;

    bool Spawnerset = false;
    public bool StartedGame;
    string monName;
    int currentSize;//Size limit for the monsters
    bool Capacity;

    [SerializeField] EntityCreator Spawner;
    [SerializeField] EnemyEntrance sDoor;

    [SerializeField] LevelManager LS;
    [SerializeField] DeathCounter Death;

    // Start is called before the first frame update
    void Start()
    {
        Death = GameObject.FindGameObjectWithTag("TheEnd").GetComponent<DeathCounter>();
        Cam = Camera.main;
        Debug.Log("Current Active Camera: " + Cam.name);


        LS = GameObject.FindGameObjectWithTag("LevelSystem").GetComponent<LevelManager>();
        if (!LS)

            Spawner = GameObject.FindGameObjectWithTag("SpawnerP").GetComponent<EntityCreator>();
        if (!Spawner)
        { Debug.Log("Spawner failed to get the Entity Creator"); }

        //Find the Enemy Spawn Door for when the Game Starts
        sDoor = GameObject.FindGameObjectWithTag("sDoor").GetComponent<EnemyEntrance>();
        if (!sDoor)
        { Debug.Log("Failed to find the Entrance of Enemies                 Yay?"); }

        LS.DebugLevelInfo();

        Cam = Camera.main;

        currentSize = LS.GetSizeLimit();

        sizetext.SetText(currentSize.ToString());
        Spawnerset = false;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnatMouse();

        if(currentSize <= 0 && Spawnerset)
        {Capacity = true; }
        CheckifGameStarted(); //Check if enemies can spawn
    }

    public void SpawnatMouse()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = 100f;

        mousePos = Cam.ScreenToWorldPoint(mousePos);

        Debug.DrawRay(transform.position, mousePos - transform.position, Color.magenta);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.direction, mousePos, Color.magenta);
            if (Physics.Raycast(ray, out hit, 1000, Tilemask))//If a tilemask specifically is hit
            {
                Debug.Log(hit.transform.name + " has been hit with Spawn Cursor ");
                if (StartedGame == false && Spawnerset)
                {

                    Spawner.SpawnMonster(monName.ToString(), hit.transform.position, Quaternion.identity);

                    //Update Current Size 
                    currentSize -= Spawner.UtilizedSize();

                    sizetext.SetText(currentSize.ToString());

                    Death.SetEnti();
                }
            }
        }
    }
   public void SpawnSkeleton()
    {
        monName = "Skeleton";
        Debug.Log("Skeleton has been Loaded!(BTN)");
        Spawnerset = true;
    }
    public void SpawnHive()
    {
        monName = "Hivewasp";
        Spawnerset = true;
        Debug.Log("Hiveling has been Loaded!(BTN)");
    }

    public void SpawnGhastist()
    {
        monName = "Ghastist";
        Spawnerset = true;
        Debug.Log("Ghastist has been Loaded!(BTN)");
    }
    public void SpawnGolem()
    {
        monName = "Golem";
        Spawnerset = true;
        Debug.Log("Golem has been Loaded!(BTN)");
    }
    public void StartGame()
    {

        monName = " ";
        StartedGame = true;
        Spawnerset = false;
        Debug.Log("Game Button has been... AcTiVaTeD!");
    }


    void CheckifGameStarted()//<-----------If the Start Game Button, Or OverCapacity, spawn enemies
    {
        if (StartedGame)
        {
            sDoor.SetDoorToTrue();

        }

        if (Capacity)
        {
            StartGame();
        }

    }
}


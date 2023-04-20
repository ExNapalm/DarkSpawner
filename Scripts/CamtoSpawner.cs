using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamtoSpawner : MonoBehaviour
{
    [SerializeField] private Transform Cam;
    public Transform CamPoint;

    //Sets the Player Camera to the level spawn point
    void Start()
    {
        SetCamSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchCamPoint()
    {
        CamPoint.gameObject.SetActive(false);
        CamPoint = GameObject.FindGameObjectWithTag("CamSpawner").transform;
    }

    void SetCamSpawn()
    {
        Cam.position = CamPoint.position;
    }
}

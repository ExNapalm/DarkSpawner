using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntrance : MonoBehaviour
{

    [SerializeField] bool DoorActive; 


    public bool GetDoorActive()
    {
        return DoorActive;

    }

    public bool SetDoorToFalse()
    {
        DoorActive = false;
        return DoorActive;

    }

    public bool SetDoorToTrue()
    {
        DoorActive = true;
        return DoorActive;

    }
}

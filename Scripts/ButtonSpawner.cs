using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawner : MonoBehaviour
{
    public SpawnButton[] spawnButtons;



    public Sounds SetActive(string btnName)
    {
        foreach (SpawnButton Btn in spawnButtons)
        {
            Btn.Active = false;

            if (Btn.buttonName == btnName)
            { Btn.Active = true; }
        }


        return null;
    }

    public bool GetActive(string btnName)
    {
        bool acT = false;
        spawnButtons[1].buttonA.enabled = true;
        foreach (SpawnButton Btn in spawnButtons)
        {

            if (Btn.buttonName == btnName)
            { acT = Btn.Active; }
        }

        return acT;
    }
    
    

}

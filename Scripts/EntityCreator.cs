using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityCreator : MonoBehaviour
{
    public MonsterList[] Entity;

    int utilizedSize;
    public void SpawnMonster(string name, Vector3 location, Quaternion rot)
    {

        foreach (MonsterList skel in Entity)
        {
            if (name != skel.EntityName)
            { }
            else
            {
                if (!skel.EntityForm || skel.EntityName == null)
                { Debug.Log("Entity form is missing... no monster is here"); }

                else
                {
                    GameObject Entity = new GameObject();
                     Entity = Instantiate(skel.EntityForm.gameObject, location, Quaternion.identity);

                    //Update Stats of the created Object
                    Entity.GetComponent<Stats>().UpdateStats(skel.EntityName.ToString(), skel.stats.health, skel.stats.attack, skel.stats.defense, skel.stats.speed, skel.stats.sight, skel.stats.special);

                    //Update Size Utilized of Level
                    utilizedSize = (int)Entity.GetComponent<Stats>().GiveSpecial();

                }
            }

        }

    }

    public int UtilizedSize()
    {
        int us = utilizedSize;
        utilizedSize = 0;//Reset Used size after creation (and before just in case)
        Debug.Log("The Used Size was: " + us);
        return us;

    }

}

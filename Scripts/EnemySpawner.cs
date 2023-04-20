using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    public EnemyList[] Enemies;


    public void SpawnEnemy(string name, Vector3 location, Quaternion rot)
    {
            foreach (EnemyList ene in Enemies)
            {
                if (name == ene.EnemyName)
                {
                if (!ene.EnemyForm)
                { Debug.Log("Entity form is missing... no monster is here"); }

                GameObject Enem = Instantiate(ene.EnemyForm, location, rot);

                //Update Stats of the created Object
                Enem.GetComponent<Stats>().UpdateStats(ene.EnemyName, ene.stats.health, ene.stats.attack, ene.stats.defense, ene.stats.speed, ene.stats.sight, ene.stats.special);
                }

            }

    }
}

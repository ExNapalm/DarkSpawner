using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public bool anEnemy;
    [SerializeField] private string Name;
    [SerializeField] private float Health, Attack, Defense, Speed, Sight, Special;
    [SerializeField] HealthScript Healthsc;
    [SerializeField] EntityCreator EntityStats;
    [SerializeField] EnemySpawner EnemyStats;

    // Start is called before the first frame update
    private void Awake()
    {
        EnemyStats = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>();
        EntityStats = GameObject.FindGameObjectWithTag("SpawnerP").GetComponent<EntityCreator>();
        

    }


    void Start()
    {
        if (anEnemy)
        {
            foreach (EnemyList ene in EnemyStats.Enemies)
            {
                if (this.name == ene.EnemyForm.name)
                {
                    UpdateStats(ene.EnemyName, ene.stats.health, ene.stats.attack, ene.stats.defense, ene.stats.speed, ene.stats.sight, ene.stats.special);
                }


            }
        }
        else
        {
            foreach (MonsterList ene in EntityStats.Entity)
            {
                if (this.name == ene.EntityForm.name)
                {
                    UpdateStats(ene.EntityName, ene.stats.health, ene.stats.attack, ene.stats.defense, ene.stats.speed, ene.stats.sight, ene.stats.special);
                }

            }

        }
        Healthsc.SetMaxHealth((int)Health);

    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void UpdateStats(string name, float h, float a, float d, float s, float sig, float spec)
    {
        Health = h;
        Attack = a;
        Defense = d;
        Speed = s;
        Sight = sig;
        Special = spec;

        Healthsc.SetMaxHealth((int)Health);
    }

    public float GiveAttack()
    {
        return Attack;
    }

    public float GiveDefense()
    {
        return Defense;
    }

    public float GiveSpeed()
    {
        return Speed;
    }

    public float GiveSight()
    {
        return Sight;
    }

    public float GiveSpecial()
    {
        return Special;
    }

    public string GiveName()
    {
        return Name;
    }
    public int GiveHealth()
    {
        return (int)Health;
    }
}

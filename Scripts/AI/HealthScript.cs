using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour//Used for changing all entities Health
{
    public int maxHealth;
    [SerializeField] private int currentHealth;

    Stats stats;

    [SerializeField] private Animator anim;
    // Check if Animator is connected
    void Start()
    {
        if(!stats)
        stats =  GetComponent<Stats>();
        if (!anim) { GetAnimObject(); }

        SetMaxHealth(stats.GiveHealth());
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim) { GetAnimObject(); }
        
    }

    public void TakeDamage(int amount)//Take damage to current health
    {
        currentHealth -= amount;
        DeathCheck();//Check if dead
    }

    public void Heal(int amount)// Heal current health ( Same as take damage, easier to have a different method for organization)
    {
        currentHealth += amount;
        if(currentHealth > maxHealth) { currentHealth = maxHealth; }//Prevent overloaded health
    }

    public void DeathCheck()// Check if health has gone down to 0 (or less)
    {
        if(currentHealth <= 0)
        {
            //Ded
            anim.SetBool("isDead", true);
            Debug.Log(this.name + "has Died!");

            this.enabled = false;

            GetComponent<Collider>().enabled = false;
        }
    }

    //Support methods
    public void GetAnimObject()
    {
        anim.GetComponent<Animator>();
    }
    public void SetMaxHealth(int Mxh)//In case Max health of an entity has changed through outside forces
    {
        maxHealth = Mxh;
        currentHealth = maxHealth;
    }

    public int SetcurrentHealth()//Send Current Health to others, expecially Health bars
    {
        return currentHealth;
    }
}

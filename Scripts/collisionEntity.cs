using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionEntity : MonoBehaviour
{

    [SerializeField] public Stats stats;
    void Start()
    {
        if(!stats)
        { }
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            if(!stats)
            {
            Debug.Log(other.name);
            other.GetComponent<Animator>().SetTrigger("onHit");
            other.GetComponent<HealthScript>().TakeDamage(5);
            }


            else
            {
                Debug.Log(other.name);
                other.GetComponent<Animator>().SetTrigger("onHit");
                other.GetComponent<HealthScript>().TakeDamage(3 + (int)stats.GiveAttack());
            }
        }
    }
}

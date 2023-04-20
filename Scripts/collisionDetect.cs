using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionDetect : MonoBehaviour
{


    void Start()
    {
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Entity")
        {

            Debug.Log(other.name);
            other.GetComponent<Animator>().SetTrigger("onHit");
            other.GetComponent<HealthScript>().TakeDamage(3);
        }
    }
}

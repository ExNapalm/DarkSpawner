using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class DefaultAI : MonoBehaviour
{
    Helper Help;
    [SerializeField] SoundManager SM;
    public AIBase AI; // Grab AI Base and all it's variables

    public Transform centrePoint;

    public Stats stats;

    public HealthScript Health;

    Animator anim;
    private GameObject Enemy, Door;
    [SerializeField]
    private GameObject WeaponHand;

    bool isdead;

    public GameObject weapon;
    public Transform closestEnemy;
    float nextAttack;

    [SerializeField] DeathCounter Death;

    void Start()
    {
        Death = GameObject.FindGameObjectWithTag("TheEnd").GetComponent<DeathCounter>();
        anim = GetComponent<Animator>();
        AI.speedWalk += stats.GiveSpeed();//Update Movement based on stats
        AI.speedRun += stats.GiveSpeed();

        AI.sightRange += stats.GiveSight();


        AI.targetPosition = transform.position;

        AI.agent = GetComponent<NavMeshAgent>();

        AI.agent.isStopped = false;
        AI.agent.speed = AI.speedWalk;

        AI.isInSightRange = false;//all attack states are started as false
        AI.isInMeleeRange = false;
        AI.isInMagicRange = false;

      weapon =  Instantiate(weapon, WeaponHand.transform.position, weapon.transform.rotation);

        GetComponent<HealthScript>();

        SM = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();

    }

    private void Update() 
    {
        if (anim.GetBool("onHit"))
        {
            SoundSFXManager.instance.PlaySoundEffectClip(SM.GetSFX(18).sfx, transform, 1f);


        }
        if (anim.GetBool("isDead"))//Stops moving when dead
        {
            if (!isdead)
            { Death.ConsumeDeath(); SoundSFXManager.instance.PlaySoundEffectClip(SM.GetSFX(19).sfx, transform, 1f);  isdead = true; }


            AI.agent.speed = 0; AI.targetPosition = transform.position;
            anim.SetTrigger("onDeath");

            SoundSFXManager.instance.PlaySoundEffectClip(SM.GetSFX(19).sfx, transform, 1f);

            Destroy(gameObject, 4f);
        }
        else
        {
            SearchforDoor();
        SearchforEnemy();

        if (AI.isInSightRange && !AI.isInMeleeRange)
        {
            Chase();
        }

        if (AI.isInMeleeRange)
        {
            Attack();
        }

        else
        {
            Patroling();
        }


        if (Door) { AI.isDoorFound = true; }
        weapon.transform.position = WeaponHand.transform.position;
        }
    }


    private void Patroling()
    {//Random Movement
        anim.SetBool("isRunning", true);
        if (AI.agent.remainingDistance <= AI.agent.stoppingDistance)
        {
            Vector3 point;
            anim.SetBool("isRunning", false);

            if(AI.isDoorFound)
            {
                //If Door is found on the Level, head towards the Location
                if (RandomPointFind(AI.Door.position + new Vector3(AI.speedWalk, 0f, AI.speedWalk), AI.sightRange * 2, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 300.0f);

                    AI.agent.SetDestination(point);
                    AI.targetPosition = point;
                    FaceTarget();
                }

            }
            else// Search around Personal Area
            {
                if (RandomPointFind(centrePoint.position + new Vector3(AI.speedWalk, 0f, AI.speedWalk), AI.sightRange * 2, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 300.0f);

                    AI.agent.SetDestination(point);
                    AI.targetPosition = point;
                    FaceTarget();
                }

            }

        }

    }

    private void HeadtoDoor()
    {
        AI.agent.SetDestination(AI.Door.position);
    }

    private void Chase()
    {
        anim.SetBool("isRunning", true);
        AI.agent.SetDestination(AI.targetPosition);
        AI.agent.speed = AI.speedRun;

        float distance = Vector3.Distance(AI.targetPosition, transform.position);

        if(distance <= AI.sightRange)
        {
            AI.agent.SetDestination(AI.targetPosition);
            AI.isInSightRange = true;
            if(distance <= AI.agent.stoppingDistance)
            {
                AI.isInMeleeRange = true;
                Attack();
            }

        }
    }

   private void Attack()
    {
        if (Time.time >= nextAttack)
        {
            anim.SetTrigger("attack");
            nextAttack = Time.time + 6 / AI.attackspeed;
        }
        
        AI.agent.speed = AI.speedRun;
        FaceTarget();
        AI.agent.SetDestination(AI.targetPosition);

    }
    bool RandomPointFind(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;

        if(NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Entity")
        {
            AI.isInSightRange = true;
            closestEnemy = other.transform;
            AI.targetPosition = other.transform.position;
            Debug.Log("Entity has been found!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Entity")
        {
            AI.isInSightRange = true;
            AI.isInMeleeRange = true;
            closestEnemy = other.transform;
            AI.targetPosition = other.transform.position;
            Debug.Log("Attacking Entity!");
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Entity")
        {
            AI.isInSightRange = false;
            AI.isInMeleeRange = false;
            Debug.Log("Entity got Away (or Dead)!");
        }

    }

    void FaceTarget()
    {
        Vector3 direction = (AI.targetPosition - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * AI.timeToRotate);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AI.sightRange);


        Gizmos.color = Color.blue;//Show random Point Radius
        Gizmos.DrawWireSphere(transform.position, AI.sightRange * 2);


        if(weapon ==null)
        {
            return;
        }
        else
        {
            Gizmos.color = Color.white;//Show random Point Radius
            Gizmos.DrawWireSphere(weapon.transform.position, AI.attackrangeMelee);
        }
    }

    void SearchforEnemy()
    {
        Enemy = GameObject.FindGameObjectWithTag("Entity");
    }

    void SearchforDoor()
    {
        Door = GameObject.FindGameObjectWithTag("Door");
        AI.Door = Door.transform;
    }

}

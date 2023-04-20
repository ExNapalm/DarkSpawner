using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EntityAI : MonoBehaviour
{
    Helper Help;
    [SerializeField] SoundManager SM;
    public AIBase AI; // Grab AI Base and all it's variables

    public Transform centrePoint;

    public Stats stats;

    Animator anim;

    private GameObject Enemy;
    public Transform closestEnemy;

    public Transform Hand;
    float nextAttack;

    bool isdead;

    [SerializeField] DeathCounter Death;
    void Start()
    {
        Death = GameObject.FindGameObjectWithTag("TheEnd").GetComponent<DeathCounter>();
        GetComponent<HealthScript>();
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

        isdead = false;

        SM = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();

        //play Spawn SFX Sound on spawn
        Debug.Log("A " + stats.name);
        switch (stats.name)
        {
            case "Skeleton(Clone)":
                SoundSFXManager.instance.PlaySoundEffectClip(SM.GetSFX("Entity1Voice").sfx, transform, 1f);
                break;
            case "Hivekin(Clone)":
                SoundSFXManager.instance.PlaySoundEffectClip(SM.GetSFX("Entity2Voice").sfx, transform, 1f);
                break;
            case "Ghastist(Clone)":
                SoundSFXManager.instance.PlaySoundEffectClip(SM.GetSFX("Entity3Voice").sfx, transform, 1f);
                break;
            case "Golem(Clone)":
                SoundSFXManager.instance.PlaySoundEffectClip(SM.GetSFX("Entity4Voice").sfx, transform, 1f);
                break;
            default:
                SoundSFXManager.instance.PlaySoundEffectClip(SM.GetSFX("Entity2Small").sfx, transform, 1f);
                break;
        }
    }

    private void Update()
    {
        if (anim.GetBool("isDead"))//Stops moving when dead
        { 

            AI.agent.speed = 0; AI.targetPosition = transform.position;
            anim.SetTrigger("onDeath");

            if (!isdead)
            { Death.ConsumeEntity(); isdead = true; }


            switch (stats.name)//Play Death Sound
            {
                case "Skeleton(Clone)":
                    SoundSFXManager.instance.PlaySoundEffectClip(SM.GetSFX("Entity1Die").sfx, transform, 1f);
                    break;
                case "Hivekin(Clone)":
                    SoundSFXManager.instance.PlaySoundEffectClip(SM.GetSFX("Entity2Die").sfx, transform, 1f);
                    break;
                case "Ghastist(Clone)":
                    SoundSFXManager.instance.PlaySoundEffectClip(SM.GetSFX("Entity3Die").sfx, transform, 1f);
                    break;
                case "Golem(Clone)":
                    SoundSFXManager.instance.PlaySoundEffectClip(SM.GetSFX("Entity4Die").sfx, transform, 1f);
                    break;
                default:
                    SoundSFXManager.instance.PlaySoundEffectClip(SM.GetSFX("Implosion").sfx, transform, 1f);
                    break;
            }
            this.tag = "Untagged";//Reset tag so enemies go for another being
            Destroy(gameObject, 3f);


        } 
        else { 
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
        }
    }


    private void Patroling()
    {//Random Movement
        anim.SetBool("isRunning", true);
        if (AI.agent.remainingDistance <= AI.agent.stoppingDistance)
        {
            Vector3 point;
            anim.SetBool("isRunning", false);


            if (RandomPointFind(centrePoint.position + new Vector3(AI.speedWalk, 0f, AI.speedWalk), AI.sightRange * 2, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.magenta, 300.0f);

                AI.agent.SetDestination(point);
                AI.targetPosition = point;
                FaceTarget();
            }
        }

    }

    private void Chase()
    {
        anim.SetBool("isRunning", true);
        AI.agent.SetDestination(AI.targetPosition);
        AI.agent.speed = AI.speedRun;

        float distance = Vector3.Distance(AI.targetPosition, transform.position);

        if (distance <= AI.sightRange)
        {
            AI.agent.SetDestination(AI.targetPosition);
            AI.isInSightRange = true;
            if (distance <= AI.agent.stoppingDistance)
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

            switch (stats.name)         //play Spawn SFX Sound on attacking
            {
                case "Skeleton(Clone)":
                    SoundSFXManager.instance.PlaySoundEffectClip(SM.GetSFX("Entity1Attack").sfx, transform, 1f);
                    break;
                case "Hivekin(Clone)":
                    SoundSFXManager.instance.PlaySoundEffectClip(SM.GetSFX("Entity2Attack").sfx, transform, 1f);
                    break;
                case "Ghastist(Clone)":
                    SoundSFXManager.instance.PlaySoundEffectClip(SM.GetSFX("Entity3Attack").sfx, transform, 1f);
                    break;
                case "Golem(Clone)":
                    SoundSFXManager.instance.PlaySoundEffectClip(SM.GetSFX("Entity4Attack").sfx, transform, 1f);
                    break;
                default:
                    SoundSFXManager.instance.PlaySoundEffectClip(SM.GetSFX("Entity2Small").sfx, transform, 1f);
                    break;
            }
        }
        AI.agent.speed = AI.speedRun;
        FaceTarget();
        AI.agent.SetDestination(AI.targetPosition);




    }
    bool RandomPointFind(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            other.GetComponent<HealthScript>().TakeDamage(10);
            AI.isInSightRange = true;
            closestEnemy = other.transform;
            AI.targetPosition = other.transform.position;
            Debug.Log("Enemy has been found!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            AI.isInSightRange = true;
            AI.isInMeleeRange = true;
            closestEnemy = other.transform;
            AI.targetPosition = other.transform.position;
            Debug.Log("Attacking Enemy!");
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Enemy")
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


        if (Hand == null)
        {
            return;
        }
        else
        {
            Gizmos.color = Color.white;//Show random Point Radius
            Gizmos.DrawWireSphere(Hand.transform.position, AI.attackrangeMelee);
        }
    }
    

    void SearchforEnemy()
    {
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

}

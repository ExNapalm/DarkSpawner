using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class AIBase
{
    public NavMeshAgent agent;
    public Vector3 targetLastPosition;
    public Vector3 targetPosition;
    public Transform Door; //Find the door in the level

    public SphereCollider AttackRange;
    public LayerMask maskGround, maskObstacle, maskEnemy;

    //Movement 
    public Vector3 walkpoint;
    public float walkPointRange;
    public bool walkPointSet;
    public float timeToRotate, speedWalk, speedRun;

    //Attacking
    public float attackspeed;
    bool attackcooldown;

    //States
    public float sightRange, sightAngle, attackrangeMelee, attackRangeRanged;
    public bool isInMeleeRange, isInMagicRange, isInSightRange, isDoorFound;

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public float speed = 10f; // target's speed to next waypoint
    public int hitDamage = 1; // damage on hit
    public int lifePoints = 1; // amount of damage before dead

    public WayPath waypath;

    private Transform waypointTarget; // next current waypoint target
    private bool reachedTarget = false; // reached the final target
    private int waypointIndex = 0; // current target index
    private float targetWaypointDistanceThreshold = 1.2f; // distance from target that consider as target reached
    private float attackGateDistanceThreshold = 3.0f; // distance from Gate that consider as attackable distance

    private bool isDead = false; // whether or not this target has started dying
    private bool canAttack = true; // whether this enemy target can attack, set by timer
    private float attackIntervalsDelay = 5.0f; // in seconds 

    private void Start()
    {
        // get initial waypoint target
        waypointTarget = waypath.getWayPoint(0);
    }

    private void Update()
    {

        if (isDead) return;

        if (reachedTarget == false)
        {
            // move target according to next target way point
            Vector3 dir = waypointTarget.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            transform.LookAt(waypointTarget);

            // check distance to the target way point
            // switch target if withing the reach of the target way point
            if (Vector3.Distance(transform.position, waypointTarget.position) <= targetWaypointDistanceThreshold)
            {
                GetNextWaypoint();
            }
        }

        // check distance for attacking gate
        // start attacking the gate every x seconds
        if (Vector3.Distance(transform.position, GameMaster.instance.baseGateObj.transform.position) <= attackGateDistanceThreshold)
        {
            if (canAttack)
                StartCoroutine(AttackGate());
        }
    }

    private void GetNextWaypoint()
    {

        waypointIndex++;

        if (waypointIndex > waypath.waypoints.Length - 1)
        {
            reachedTarget = true;
            return;
        }

        waypointTarget = waypath.getWayPoint(waypointIndex);
    }

    public void TargetHit()
    {
        lifePoints--;
        if (lifePoints <= 0)
        {
            StartCoroutine(KillTarget());
        }
    }

    IEnumerator KillTarget()
    {
        isDead = true;
        gameObject.GetComponentInChildren<Animation>().Play("orcdie"); ; // destroy the target
        yield return new WaitForSeconds(2.3f);
        DestroyTarget();
    }

    private void DestroyTarget()
    {
        Destroy(gameObject);
    }

    IEnumerator  AttackGate()
    {
        GameMaster.instance.HitGate(hitDamage, gameObject);
        canAttack = false;
        yield return new WaitForSeconds(attackIntervalsDelay);
        canAttack = true;
    }

    
}

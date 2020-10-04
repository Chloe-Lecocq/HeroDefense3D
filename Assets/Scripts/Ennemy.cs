﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public float speed = 0.1f;
    private bool _isSpawningEnnemies = true;
    public int force = 5, currentWayPoint = 0;
    public bool entryReached, hasReachedCheckpoint = false;
    public Transform[] keypointsPath;
    public Transform targetWayPoint, buildings, entryGate;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_isSpawningEnnemies ) {
            if (!entryReached) {
                walkTo(entryGate);
            } else {
                if (currentWayPoint < 4) {
                    if(targetWayPoint == null)
                        targetWayPoint = keypointsPath[0].GetChild(currentWayPoint);
                    walkTo(targetWayPoint);
                } else {
                    walkTo(buildings.GetChild(0));
                }
            }
        }
    }

    public void walkTo(Transform goal){
        transform.LookAt(goal.transform);
        transform.Translate(transform.forward * speed, Space.World);
    }

    void OnTriggerEnter(Collider c)
    {
        switch (c.gameObject.tag)
        {
            case "EntryFilter":
                Debug.Log("OK");
                Physics.IgnoreCollision(transform.GetComponent<Collider>(), GetComponent<Collider>()); //allow for ennemies
                break;
            case "EntryGate":
                Debug.Log("Yeah ! First point");
                entryReached = true;
                break;
            case "Checkpoint":
                hasReachedCheckpoint = true;
                break;
            case "Finish":
                Debug.Log("Touched the house ! Mouhaha !!");
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
   
}
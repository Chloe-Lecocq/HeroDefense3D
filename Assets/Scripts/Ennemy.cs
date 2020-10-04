using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public float speed = 0.1f;
    private bool _isSpawningEnnemies = true;
    public int force = 5;
    public bool entryReached = false;
    public Transform entryGate;
    public Transform[] keypointsPath;
    public int currentWayPoint = 0;
    public bool hasReached = false;
    public Transform targetWayPoint;
    public Transform buildings;
    
    // Start is called before the first frame update
    void Start()
    {
        // transform.LookAt(Camera.main.transform);
        //transform.LookAt(entryGate.transform);
        // transform.position = Vector3.Lerp(this.transform.position, 
        //                                 new Vector3(entryGate.position.x, entryGate.position.y, entryGate.position.z), 
        //                                 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isSpawningEnnemies && !entryReached) {
            transform.LookAt(entryGate.transform);
            transform.Translate(transform.forward * speed, Space.World);
        } else if (_isSpawningEnnemies && entryReached) {
            ///transform.LookAt(buildings.GetChild(0).transform);
            //transform.Translate(transform.forward * speed, Space.World);
            if(targetWayPoint == null) {targetWayPoint = keypointsPath[0].GetChild(currentWayPoint);}
            walk();
        }
    }

    public void walk(){
        transform.LookAt(targetWayPoint.transform);
        transform.Translate(transform.forward * speed, Space.World);
        //transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, speed*Time.deltaTime, 0.0f);
        //transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position,   speed*Time.deltaTime);
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
                hasReached = true;
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
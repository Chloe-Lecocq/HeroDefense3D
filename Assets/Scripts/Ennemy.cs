using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public float speed = 0.1f;
    private bool _isSpawningEnnemies = true;
    public int force = 5;
    private bool entryReached = false;
    public Transform entryGate;
    public Transform[] keypointsPath;
    private int currentWayPoint = 0;
    private Transform targetWayPoint;
    public Transform buildings;
    protected Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, 50f);
        // transform.LookAt(Camera.main.transform);
        //transform.LookAt(entryGate.transform);
        // transform.position = Vector3.Lerp(this.transform.position, 
        //                                 new Vector3(entryGate.position.x, entryGate.position.y, entryGate.position.z), 
        //                                 0.2f);
        animator = GetComponent<Animator>();
        animator.SetTrigger("Walk");

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

            if(currentWayPoint < this.keypointsPath.Length)
            {
                if(targetWayPoint == null)
                    targetWayPoint = keypointsPath[0].GetChild(currentWayPoint);
                walk();
            }
        }
    }

    void walk(){
        // rotate towards the target
        transform.LookAt(targetWayPoint.transform);
        transform.Translate(transform.forward * speed, Space.World);
        //transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, speed*Time.deltaTime, 0.0f);
 
        // move towards the target
        //transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position,   speed*Time.deltaTime);

        if(transform.position == targetWayPoint.position)
        {
            currentWayPoint++;
            targetWayPoint = keypointsPath[0].GetChild(currentWayPoint);
        }
     } 

    void OnTriggerEnter(Collider c)
    {
        switch (c.gameObject.tag)
        {
            case "EntryFilter":
                Physics.IgnoreCollision(transform.GetComponent<Collider>(), GetComponent<Collider>()); //allow for ennemies
                break;
            case "EntryGate":
                entryReached = true;
                break;
            case "Finish":
                Destroy(this.gameObject);
                break;

            default:
                break;
        }
        

    }
   
}
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;
    public bool isWalking = false;
    Vector2 velocity;
    public Animator walk;

    void Start()
    {
        walk = GetComponent<Animator>();
    }


    void Update()
    {
        velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(velocity.x, 0, velocity.y);
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "EntryFilter") {
            Debug.Log("Nope");
            //Physics.IgnoreCollision(transform.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public float healthAtStart;
    protected float currentHealth;
    public ProgressBar ProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = healthAtStart;
        ProgressBar.start = healthAtStart;
        ProgressBar.setBarValue(currentHealth);

    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Controller.instance.setLost();
        }
    }

    void OnTriggerEnter(Collider c)
    {
        switch (c.gameObject.tag)
        {
            case "Enemy":
                currentHealth -= 100;
                ProgressBar.setBarValue(currentHealth);
                Destroy(c.gameObject);
                break;
            default:
                break;
        }
    }

}

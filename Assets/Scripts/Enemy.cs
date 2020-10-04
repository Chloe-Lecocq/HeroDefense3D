using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startHealth;
    private float health;
    private bool isDead = false;
    private Animator animator;

        void Start()
    {
        animator = GetComponent<Animator>();
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        GameManager.instance.UpScore();
        animator.SetTrigger("Die");
        gameObject.tag = "DeadEnemy";
        Destroy(gameObject, 1.5f);

    }
}

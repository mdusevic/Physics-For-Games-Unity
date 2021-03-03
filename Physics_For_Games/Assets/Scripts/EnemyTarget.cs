using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTarget : MonoBehaviour
{
    Rigidbody rb;
    public int maxHealth = 100;
    public int health;

    public HealthBar healthBar;

    private bool healthBarCreated = false;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = maxHealth;
    }

    void Update()
    {
        if (healthBar != null && !healthBarCreated)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBarCreated = true;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            health -= damage;

            healthBar.SetHealth(health);

            if (health <= 0)
            {
                health = 0;
                isDead = true;
                Die();
            }
        }
    }

    void Die()
    {
        Destroy(healthBar.gameObject);
        GetComponent<Animator>().enabled = false;
        GetComponent<Ragdoll>().RagdollOn = true;
        Destroy(gameObject, 4.0f);
    }
}

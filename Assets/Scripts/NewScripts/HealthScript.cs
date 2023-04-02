using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int health;
    public int maxHealth;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void SetHealth(int newHealth)
    {
        health = Mathf.Min(newHealth, maxHealth);
    }

    private void Die()
    {
        // Add code for when the object dies here
    }
}


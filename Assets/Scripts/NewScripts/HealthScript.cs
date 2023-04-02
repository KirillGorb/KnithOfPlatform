using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField]private int maxHealth;
    private int health;

    private void Awake()
        
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
    }
}


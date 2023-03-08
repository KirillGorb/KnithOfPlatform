using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    private void Start()
    {
        // Initialize current health to max health
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        // Reduce current health by damage amount
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            // Destroy game object if health drops to 0 or below
            Destroy(gameObject);
        }
    }
}

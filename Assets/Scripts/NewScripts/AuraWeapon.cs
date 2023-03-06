using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraWeapon : MonoBehaviour
{
    public float damage = 5f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // Deal damage to the enemy
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage * Time.deltaTime);
        }
    }
}
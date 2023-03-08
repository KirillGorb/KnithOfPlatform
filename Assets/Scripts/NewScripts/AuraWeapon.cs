using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraWeapon : MonoBehaviour
{
    public float damage = 5f;

    private void OnTriggerStay(Collider other)
    {
        if (other .TryGetComponent(out EnemyMovement enemy))
        {
            enemy.TakeDamage (damage * Time.deltaTime);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public float damage = 10f;
    public float cooldown = 1f;
    private bool canAttack = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && canAttack)
        {
            // Deal damage to the enemy and start cooldown
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        // Wait for cooldown before attacking again
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }
}
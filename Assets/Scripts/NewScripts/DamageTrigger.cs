using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.Events;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] private float radius = 1f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private int damage = 1;
    [SerializeField] private UnityEvent onDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (targetLayer == (targetLayer | (1 << other.gameObject.layer)))
        {
            var enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                onDamage.Invoke();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
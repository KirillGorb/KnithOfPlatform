using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int collisionDamage;
    public string collisionTag;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == collisionTag)
        {
            HealthScript health = coll.gameObject.GetComponent<HealthScript>();
            if (health != null)
            {
                health.TakeDamage(collisionDamage);
            }
        }
    }
}

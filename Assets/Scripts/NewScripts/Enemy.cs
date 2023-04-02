using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float speed = 5f; 
    private Rigidbody2D rb; 

    private Transform playerTransform; 
    private Vector2 direction; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    void Update()
    {        
        direction = playerTransform.position - transform.position;
        direction.Normalize(); 

        
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); 
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); 
        }
    }

    void FixedUpdate()
    {
        Vector2 movement = new Vector2(direction.x * speed * Time.fixedDeltaTime, 0);
        rb.MovePosition(rb.position + movement);
    }
}

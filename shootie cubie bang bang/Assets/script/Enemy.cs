using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float movespeed = 3f;
    Rigidbody2D rb;
    Vector2 direction;
    Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * movespeed * Time.fixedDeltaTime);
        }
        
    }

}

/*
 * 
 * Author: Spencer Wilson
 * Description: Contains controls for player movement and abilities.
 * 
 */ 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 newVelocity = new Vector2(x, y) * speed;

        rb.velocity = newVelocity;
    }
}

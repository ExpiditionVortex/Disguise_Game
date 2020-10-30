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
    public GameObject playerStandard;
    public GameObject reflector;

    public Rigidbody rbStandard;
    public Rigidbody rbReflector;
    public float speedStandard;
    public float speedReflector;
    public float rotSpeed;

    public bool reflectorMode;
    public bool reflectorModePrev;

    public GameObject potentialReflector;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown("space") && potentialReflector != null)
        {
            reflectorMode = !reflectorMode;

            if (reflectorMode)
                ReflectorSetup();
            else
                ReflectorDeactivation();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!reflectorMode)
            MoveStandard();
        else
            ReflectorController();

    }

    private void MoveStandard()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 newVelocity = new Vector2(x, y) * speedStandard;

        rbStandard.velocity = newVelocity;
    }

    private void ReflectorController()
    {
        MoveReflector();
        RotateReflector();
    }


    private void MoveReflector()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 newVelocity = new Vector2(x, y) * speedReflector;

        rbReflector.velocity = newVelocity;
    }

    private void RotateReflector()
    {
        if (Input.GetKey("q"))
        {
            reflector.transform.Rotate(0f, 0f, rotSpeed);
        }
        else if (Input.GetKey("e"))
        {
            reflector.transform.Rotate(0f, 0f, -rotSpeed);
        }
    }

    private void ReflectorSetup()
    {
        if (potentialReflector != null)
        {
            reflector = potentialReflector;
            reflector.transform.parent = gameObject.transform;
            rbReflector = reflector.GetComponent<Rigidbody>();


            playerStandard.SetActive(false);
            // Dragging standard player along w/ reflector
            playerStandard.transform.parent = reflector.transform;
            rbStandard.velocity = Vector2.zero;

            potentialReflector = reflector;
            reflectorMode = true;
        }

    }

    private void ReflectorDeactivation()
    {
        // Setting up player standard.
        playerStandard.transform.parent = gameObject.transform;
        playerStandard.SetActive(true);

        // Disengaging reflector.
        rbReflector.velocity = Vector2.zero;
        reflector.transform.parent = null;
        rbReflector = null;

        reflectorMode = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            potentialReflector = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(potentialReflector))
        {
            potentialReflector = null;
        }
    }
}

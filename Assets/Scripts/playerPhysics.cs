using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class playerPhysics : MonoBehaviour {
    public float acceleration = 0.1f;
    public float rotationSpeed = 1f;
    public float maxSpeed = 5f;
    public float forceMultiplier = 1f;
    public int health = 100;
    public GameController gm;

    // Use this for initialization
    void Start () {
        GetComponent<Rigidbody2D>().inertia = 3;
        gm = GameObject.FindObjectOfType<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
    }

    //TEST PLEASE DELETE
    public void bumpPlayer()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
    }

    //Checking to see if user hit a hardpoint.
    private void OnMouseDown()
    {
        /*
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Ray2D ray = new Ray2D(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
        }
        */
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        Vector2 myInput = GetInput();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //rb.velocity = (myInput * acceleration);



        //trying a new way

        //1. The closer the speed is to the desired input, the less force it will take.

        Vector2 desiredVelocity = myInput * maxSpeed;
        Vector2 actualVelocity = rb.velocity;

        Vector2 velocityDifference = desiredVelocity - actualVelocity;

        rb.AddForce(velocityDifference * forceMultiplier);


        /* OLD WAY
        myInput = myInput.normalized * acceleration * Time.deltaTime;

        Rigidbody2D myRB = GetComponent<Rigidbody2D>();
        Vector2 myVelocity = myRB.velocity;

        Vector2 myTotal = myVelocity + myInput;
        
        //If the new total magnitude of the velocity vector is less than that maxSpeed
        if (myTotal.magnitude < maxSpeed)
        {
            Debug.Log(myTotal);
            myRB.velocity = new Vector2(myRB.velocity.x + myInput.x, myRB.velocity.y + myInput.y);
        }
        else {
            Vector2 velocityDirection = new Vector2(myRB.velocity.x + myInput.x, myRB.velocity.y + myInput.y).normalized;
            myRB.velocity = velocityDirection * Mathf.Sqrt(maxSpeed);
        }
        */
    }

    public Vector2 GetInput()
    {

#if UNITY_STANDALONE
        Vector2 myInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
#else
        Vector2 myInput = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
#endif
        if (myInput.sqrMagnitude > 1)
        {
            myInput = myInput.normalized;
        }

        return myInput;
    }

    private void RotatePlayer()
    {

        GetComponent<Rigidbody2D>().transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }

    public void Hurt(int amount)
    {
        health -= amount;
        gm.UpdateHealth(health);
    }

    public void OnClick()
    {
        Debug.Log("clicked");
    }
    
}

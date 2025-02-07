using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;



public class playermove : MonoBehaviour
{     
    float horizontalinput;
    float verticalinput;
     public float Gravity = 1f;
    public float groundDrag;
    public float jumpforce = 5;
    public Movementstate state;

    public enum Movementstate
    {
        walkking,
        running,
        inair
    }
    public float jumpcooldown = 5f;
public float airmultiplier;
public bool canjump = true;
    public float playerheight;
    public LayerMask whatisground;
    public bool grounded;

    Vector3 movedirection;
    private float moveSpeed = 10f;
    public float walkspeed;
    public float runspeed;
    public KeyCode sprintkey = KeyCode.LeftShift;
       private Vector3 _moveInput;
       private KeyCode jumpkey = KeyCode.Space;
       public Transform orentation;
       private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
         Physics.gravity *= Gravity;
         canjump = true;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }
        void Update()
    {
        input(); 
        statehandler();

        grounded = Physics.Raycast(transform.position, Vector3.down, playerheight * 0.5f + 0.2f, whatisground);

        //drag stuffs 
        if(grounded)
        {
            rb.drag = groundDrag;
        }
        else 
        {
            rb.drag = 0;
        }
       
    }
      private void FixedUpdate()
    {
         movement();
    }


    private void input()
    {
        horizontalinput = Input.GetAxisRaw("Horizontal");
        verticalinput = Input.GetAxisRaw("Vertical"); 

        if(Input.GetKey(jumpkey) && canjump && grounded)
        {
            canjump = false;
            jump();
            Invoke(nameof(resetjump), jumpcooldown);
        }
    }

    private void statehandler()
    {
        //put in sprint state
        if(grounded && Input.GetKey(sprintkey))
        {
            state = Movementstate.running;
            moveSpeed = runspeed;
        }
        //put in walking state
        else if(grounded)
        {
            state = Movementstate.walkking;
            moveSpeed = walkspeed;
        }

        // put in air state
        else
        {
            state = Movementstate.inair;
        }
    }

  
    private void movement()
    {
        movedirection = orentation.forward * verticalinput + orentation.right *horizontalinput;
            //on ground
            if(grounded)
            {
        rb.AddForce(movedirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }
        //in air
        else if(!grounded)
        {
            rb.AddForce(movedirection.normalized * moveSpeed * 10f * airmultiplier, ForceMode.Force);
        }
    }

    private void jump()
    {
     //reset y velocity for always jumping same height
     rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

     rb.AddForce(transform.up * jumpforce, ForceMode.Impulse);
    }
   private void resetjump()
   {
    canjump = true;
   }
}

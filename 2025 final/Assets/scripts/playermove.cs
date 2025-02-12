using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;



public class playermove : MonoBehaviour
{     
    public float maxSlope;
        
    private RaycastHit slopehit;
    public float crouchspeed;
    public float crouchyscale;
    public float startyscale;
    private AudioSource source;
    public AudioClip jumping;
    public AudioClip groundpound;
    public KeyCode crouchkey = KeyCode.LeftControl;
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
        inair,
        crouching
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
         source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        startyscale = transform.localScale.y;

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
       //speedcontrol();
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
            source.PlayOneShot(jumping);
        }

    //start crouch

    if (Input.GetKeyDown(crouchkey))
    {
        transform.localScale = new Vector3(transform.localScale.x, crouchyscale,transform.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    }
    //stop crouch
    if (Input.GetKeyUp(crouchkey))
    {
        transform.localScale = new Vector3(transform.localScale.x, startyscale, transform.localScale.z);
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
            if (Input.GetKeyDown(crouchkey))
            {
                rb.AddForce(Vector3.down * jumpforce, ForceMode.Impulse);
                source.PlayOneShot(groundpound);
            }
        }

        if (Input.GetKey(crouchkey) && grounded)
        {
            state = Movementstate.crouching;
            moveSpeed = crouchspeed;
        }
    }

  
    private void movement()
    {
        movedirection = orentation.forward * verticalinput + orentation.right *horizontalinput;

        //on slope
        if(OnSlope())
        {
            rb.AddForce(Slopedirection() * moveSpeed * 10f, ForceMode.Force);
        }
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

    private void speedcontrol()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);


        //limit velocity

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 LimitedVel  = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(LimitedVel.x, rb.velocity.y, LimitedVel.z);
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
   private bool OnSlope()
   {
    if(Physics.Raycast(transform.position, Vector3.down, out slopehit, playerheight * 0.5f + 0.3f))
    {
        float angle = Vector3.Angle(Vector3.up, slopehit.normal);
        return angle < maxSlope && angle != 0;
    }

    return false;
   }

   private Vector3 Slopedirection()
   {
    return Vector3.ProjectOnPlane(movedirection, slopehit.normal).normalized;
   }
}

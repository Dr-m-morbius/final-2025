using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class playermove : MonoBehaviour
{
    public float moveSpeed = 1f;
    public bool running = false;
    public float runSpeed =2f;
    public Transform theCamera;
    public Transform groundCheckpoint;
    public bool candash = true;
    public GameObject player;
         
             public float jumpForce = 10f;
         public float gravityModifier = 1f;
         
         public Transform firepoint;
         public float dashTime = 1f;

public GameObject bullet;
       private Vector3 _moveInput;
       public float JumpForce = 15f;
        public float dashForce = 15f;
       private Rigidbody _playerRb;
       public LayerMask WhatIsGround;
    private CapsuleCollider playercollide;
         public float mouseSensitivity = 1f;
       private CharacterController _characterController;
        [SerializeField] private bool _isOnGround;
        private ammo _ammo;
        private Rigidbody rb;

            private bool _canPlayerJump;
    // Start is called before the first frame update
    void Start()
    {
         _characterController = GetComponent<CharacterController>();
         Cursor.lockState = CursorLockMode.Locked;
         playercollide = GetComponent<CapsuleCollider>();
        _playerRb = GetComponent<Rigidbody>();
        _ammo = GetComponent<ammo>();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 15;

        }
        else
        {
            moveSpeed = 5;
        }
            //shooting 
            if(Input.GetMouseButtonDown(0) && _ammo.GetAmmoAmount() > 0)
{
    //find crosshair
    RaycastHit hit;
    if(Physics.Raycast(theCamera.position, theCamera.forward, out hit, 50f))
    {
        if(Vector3.Distance(theCamera.position,hit.point) > 2f)
        {
            firepoint.LookAt(hit.point);
        }
    }
    else
    {
        firepoint.LookAt(theCamera.position + (theCamera.forward * 30f));
    }

    //bullet born
    Instantiate(bullet, firepoint.position, firepoint.rotation);

    //kill ammo
    _ammo.RemoveAmmo();
}

        
                //Player jump setup
    float yVelocity = _moveInput.y;
        //playermovement
          Vector3 forwardDirection = transform.forward * Input.GetAxis("Vertical");
        Vector3 horizontalDirection = transform.right * Input.GetAxis("Horizontal");

        _moveInput = (forwardDirection + horizontalDirection).normalized;
       _moveInput *= moveSpeed;
        
        

        //camrea 
         Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        theCamera.rotation = Quaternion.Euler(theCamera.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

               //Apply Jumping
               
        _moveInput.y = yVelocity;
        _moveInput.x += Physics.gravity.y * gravityModifier * Time.deltaTime;

        _moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        //Check if character controller is on the ground
        if(_characterController.isGrounded)
        {
            _moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        //Check if player can jump
        _canPlayerJump = Physics.OverlapSphere(groundCheckpoint.position, 0.5f, WhatIsGround).Length > 0;

        //Make player jump
        if(Input.GetKeyDown(KeyCode.Space) && _canPlayerJump)
        {
            _moveInput.y = jumpForce;
        }
       
        if(Input.GetKeyDown(KeyCode.LeftShift)&& candash)
        {
        
            dashTime = 1;
            dashTime -= Time.deltaTime;
            candash = false;
        }
        _characterController.Move(_moveInput * Time.deltaTime);
        if(dashTime <= 0)
        {
            candash = true;
        }
         if(dashTime >= 0)
        {
            dashTime -= Time.deltaTime;
        
        }
          
    }
}

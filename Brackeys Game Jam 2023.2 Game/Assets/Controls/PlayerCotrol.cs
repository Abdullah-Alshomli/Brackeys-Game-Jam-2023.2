using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCotrol : MonoBehaviour
{
    [Header("Movement Settings")]
    private CustomInput input;
    private Rigidbody rb;
    [SerializeField] private float speed = 5;
    [SerializeField] private float currentSpeed = 0;
    [SerializeField] private float jump = 5;
    [SerializeField] private float movingDownSpeed = 400;
    
    [Header("Ground Check Settings")]
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private float radiusOfGroundCheck = 0.3f;
    // layers the player can jump from
    [SerializeField] private LayerMask groundLayer;

    [Header("Mouse Settings")]
    // layers the mouse can aim at
    [SerializeField] private LayerMask backgroundLayer;

    public int mouseOnLayer = 0;

    public static Vector3 mousePostion = Vector3.zero;
    
    [Header("Gun Settings")]
    [SerializeField] private GameObject gun;
    [SerializeField] private float gunCooldown = 0.1f;
    private float gunTime = 0;

    [Header("Hook Settings")] 
    [SerializeField] private float hookCooldown = 1;
    [SerializeField] private float hookSpeed = 2;

    private float hooktime = 0;
    private bool isHooking = false;
    private bool canHook = false;

    public static Vector3 hookPostion = new Vector3(0,0,0);
    private LineRenderer lineRend;

    [Header("Pause Settings")]
    [SerializeField] private GameObject popup;
    
    
    
    // Start is called before the first frame update
    void Awake()
    {
        input = new CustomInput();
        rb = this.GetComponent<Rigidbody>();
        lineRend = FindObjectOfType<LineRenderer>();
        lineRend.enabled = false;
    }
    
    private void FixedUpdate()
    {
        //moving the player
        //rb.transform.position += Vector3.right * (currentSpeed * Time.deltaTime);
        if (IsGrounded())
        {
            rb.AddForce(Vector3.right * (currentSpeed), mode: ForceMode.VelocityChange);
            //rb.velocity = Vector3.ClampMagnitude(rb.velocity, 15);
        }
        else
        {
            rb.AddForce(Vector3.right * (currentSpeed/2), mode: ForceMode.VelocityChange);
            //rb.velocity = Vector3.ClampMagnitude(rb.velocity, 15);
        }
        
        
        
        //hook-shot movement
        Vector3 hookshotDir = (hookPostion - transform.position).normalized;
        
        if (isHooking)
        {
            lineRend.enabled = true;
            rb.AddForce(hookshotDir * hookSpeed ,mode: ForceMode.Impulse);
        }
        else
        {
            lineRend.enabled = false;
            isHooking = false;
        }

        hooktime -= Time.deltaTime;
        gunTime -= Time.deltaTime;
    }

    private void Update()
    {
        // mouse screen position to world position
        screenPositionToWorldPosition();
        
        // gun aim at mouse
        Vector3 newPostion = new Vector3(mousePostion.x, mousePostion.y, gun.transform.position.z);
        gun.transform.LookAt(newPostion);

        //rotate player depending on the mouse
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        bool isMouseOnRightSide = mousePostion.x > transform.position.x;

        if (isMouseOnRightSide)
        {
            transform.localScale =
                new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y,
                transform.localScale.z);
        }
    }


    private void OnEnable()
    {
        input.Enable();
        
        
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCanceled;

        input.Player.Jump.performed += OnJumpPerformed;
        input.Player.Jump.canceled += OnJumpCanceled;
        
        input.Player.Hook.performed += OnHookPerformed;
        input.Player.Hook.canceled += OnHookCanceled;
        
        input.Player.MoveDown.performed += OnMoveDown;
        
        input.Player.Fire.performed += OnFire;

        input.Player.Pause.performed += OnPause;
        input.Player.Pause.canceled -= OnPause;
         
    }

    

    private void OnDisable()
    {
        input.Disable();
        
        
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCanceled;
        
        input.Player.Jump.performed -= OnJumpPerformed;
        input.Player.Jump.performed -= OnJumpCanceled;
        
        input.Player.Hook.performed -= OnHookPerformed;
        input.Player.Hook.canceled -= OnHookCanceled;

        input.Player.MoveDown.performed -= OnMoveDown;
        
        input.Player.Fire.performed -= OnFire;
    }
    
    
    // Moving
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        currentSpeed = speed * value.ReadValue<float>();
    }
    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        currentSpeed = speed * 0;
    }
    
    // Jumping
    private void OnJumpPerformed(InputAction.CallbackContext value)
    {
        if (IsGrounded())
        {
            //the player is on a ground
            
            rb.AddForce(Vector3.up * jump , mode: ForceMode.Impulse);
        }
        // stop hooking
        isHooking = false;
    }
    private void OnJumpCanceled(InputAction.CallbackContext obj)
    {
        Debug.Log(obj.ReadValueAsButton());
    }
    
    // Shankiling
    private void OnHookPerformed(InputAction.CallbackContext value)
    {
        if (canHook)
        {
            if (hooktime <= 0 && mouseOnLayer == Layers.Ground)
            {
                if (isHooking)
                {
                    isHooking = false;
                }
                isHooking = true;
                hookPostion = mousePostion;
                hooktime = hookCooldown;
            }
        }
    }
    
    private void OnHookCanceled(InputAction.CallbackContext obj)
    {
        Debug.Log(obj.ReadValueAsButton());
    }
    
    // Moving down 
    private void OnMoveDown(InputAction.CallbackContext obj)
    {
        rb.AddForce(Vector3.down * movingDownSpeed, mode: ForceMode.Impulse);
    }


    // shooting
    private void OnFire(InputAction.CallbackContext obj)
    {
        if (gunTime <= 0)
        {
            gun.GetComponent<Shooting>().Shoot();
            gunTime = gunCooldown;
        }


    }
    
    // pause popup
    private void OnPause(InputAction.CallbackContext obj)
    {

    }
    

    
    /// <summary>
    /// check for if the player is on a ground
    /// </summary>
    /// <returns>true if the player is on a ground layer</returns>
    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.transform.position, radiusOfGroundCheck, groundLayer);
    }


    // mouse screen position to world position
    private void screenPositionToWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 

        if (Physics.Raycast(ray, out RaycastHit hit,1000,backgroundLayer)) 
        {
            // the mouse hit a background layer
            
            /*use for hook*/
            mouseOnLayer = hit.transform.gameObject.layer;

            Vector3 mouseWorldPos = hit.point; 
            //removing depth
            mouseWorldPos.z = 0;
            mousePostion = mouseWorldPos;
        }
    }

    public void startHook()
    {
        canHook = true;
    }


}

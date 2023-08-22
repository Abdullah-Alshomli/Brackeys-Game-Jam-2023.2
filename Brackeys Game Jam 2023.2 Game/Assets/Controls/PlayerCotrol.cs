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
    [SerializeField] private float hookSpeed = 5;
    [SerializeField] private float movingDownSpeed = 400;
    
    [Header("Ground Check Settings")]
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private float radiusOfGroundCheck = 0.3f;
    // layers the player can jump from
    [SerializeField] private LayerMask groundLayer;

    [Header("Mouse Settings")]
    // layers the mouse can aim at
    [SerializeField] private LayerMask backgroundLayer;

    private Vector3 mousePostion = Vector3.zero;
    
    [Header("Gun Settings")]
    [SerializeField] private GameObject gun;
    
    // Start is called before the first frame update
    void Awake()
    {
        input = new CustomInput();
        rb = this.GetComponent<Rigidbody>();
    }
    
    
    private void FixedUpdate()
    {
        //moving the player
        rb.transform.position += Vector3.right * (currentSpeed * Time.deltaTime);
    }

    private void Update()
    {
        // mouse screen position to world position
        screenPositionToWorldPosition();
        
        // gun aim at mouse
        gun.transform.LookAt(mousePostion);

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
    }
    private void OnJumpCanceled(InputAction.CallbackContext obj)
    {
        Debug.Log(obj.ReadValueAsButton());
    }
    
    // Shankiling
    private void OnHookPerformed(InputAction.CallbackContext value)
    {
        rb.AddForce((mousePostion - transform.position).normalized * hookSpeed ,mode: ForceMode.Impulse);
    }
    private void OnHookCanceled(InputAction.CallbackContext obj)
    {
        Debug.Log(obj.ReadValueAsButton());
    }
    
    // Moving down 
    private void OnMoveDown(InputAction.CallbackContext obj)
    {
        rb.AddForce(Vector3.down * movingDownSpeed, mode: ForceMode.Acceleration);
    }


    // shooting
    private void OnFire(InputAction.CallbackContext obj)
    {
        gun.GetComponent<Shooting>().Shoot();
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
            
            Vector3 mouseWorldPos = hit.point; 
            //removing depth
            mouseWorldPos.z = 0;
            mousePostion = mouseWorldPos;
        }
    }
}

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
    
    
    /// <summary>
    /// check for if the player is on a ground
    /// </summary>
    /// <returns>true if the player is on a ground layer</returns>
    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.transform.position, radiusOfGroundCheck, groundLayer);
    }
}

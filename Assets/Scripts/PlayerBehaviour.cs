using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float Gravity;
    [SerializeField] private float TurnSpeed;
    [SerializeField] private float JumpForce;

    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask GroundMask;

    private Controls controls;
    private CharacterController controller;

    private Vector2 direction;
    private Vector2 turn;
    private bool buttonactive;

    private Vector3 TurnDirection;
    private Vector3 PlayerDirection;
    private Vector3 DirectionToMove;
    private Vector3 Velocity;

    private float GroundDistance;
    private bool IsGrounded;

    /*private float TurnSmoothTime;
    private float TurnSmoothVelocity;*/


    private void OnEnable()
    {
        controls = new Controls();
        controls.Enable();

        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMoveCanceled;

        controls.Player.Aim.performed += OnAimPerformed;
        controls.Player.Aim.canceled += OnAimPerformed;

        controls.Player.Jump.performed += OnJumpPerformed;
        controls.Player.Jump.canceled += OnJumpCanceled;

        controller = GetComponent<CharacterController>();

        GroundDistance = 0.4f;

        //TurnSmoothTime = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDirection = new Vector3(direction.x, 0, direction.y);
        DirectionToMove = new Vector3(PlayerDirection.x, Velocity.y, PlayerDirection.z);
        TurnDirection = new Vector3(0, turn.x, 0);

        transform.Rotate(TurnDirection * TurnSpeed * Time.deltaTime);
        DirectionToMove = transform.TransformDirection(DirectionToMove);
        controller.Move(DirectionToMove * Speed * Time.deltaTime);

        Velocity.y += Gravity * Time.deltaTime;

        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if(IsGrounded && buttonactive)
        {
            Velocity.y = JumpForce;
        }
        else
        {
            Velocity.y = Gravity;
        }

        //Brackeys mode -> doesn't work meh
        /*DirectionToMove = transform.TransformDirection(DirectionToMove); //Necessaire

        float TargetAngle = Mathf.Atan2(PlayerDirection.x, PlayerDirection.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
        float Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngle, ref TurnSmoothVelocity, TurnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, Angle, 0f);

        Vector3 MoveDirection = Quaternion.Euler(0f, TargetAngle, 0f) * (DirectionToMove);
        controller.Move(MoveDirection * Speed * Time.deltaTime);*/
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        direction = obj.ReadValue<Vector2>();
        //Debug.Log(direction);
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        direction = Vector2.zero;
    }

    private void OnAimPerformed(InputAction.CallbackContext obj)
    {
        turn = obj.ReadValue<Vector2>();
        //Debug.Log(turn);
    }

    private void OnAimCanceled(InputAction.CallbackContext obj)
    {
        turn = Vector2.zero;
    }

    public void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        buttonactive = obj.ReadValueAsButton();
        Debug.Log("Yes !");
    }

    private void OnJumpCanceled(InputAction.CallbackContext obj)
    {
        buttonactive = false;
    }

}

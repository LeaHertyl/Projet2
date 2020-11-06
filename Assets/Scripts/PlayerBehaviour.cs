using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float Gravity;

    [SerializeField] private float TurnSpeed;
    
    private Controls controls;
    private CharacterController controller;

    private Vector2 direction;
    private Vector2 turn;
    private Vector3 TurnDirection;
    private Vector3 PlayerDirection;
    private Vector3 DirectionToMove;

    private Vector2 AimDirection;


    private void OnEnable()
    {
        controls = new Controls();
        controls.Enable();

        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMoveCanceled;

        controls.Player.Aim.performed += OnAimPerformed;

        controller = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerDirection = new Vector3(direction.x, 0, direction.y);
        DirectionToMove = new Vector3(PlayerDirection.x, Gravity, PlayerDirection.z);

        TurnDirection = new Vector3(0, turn.y, 0);
        gameObject.transform.Rotate(TurnDirection * TurnSpeed * Time.deltaTime);

        DirectionToMove = transform.TransformDirection(DirectionToMove);

        controller.Move(DirectionToMove * Speed * Time.deltaTime);
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
        Debug.Log(turn);
    }

}

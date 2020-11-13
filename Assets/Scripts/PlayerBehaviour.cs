﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float Gravity;
    [SerializeField] private float TurnSpeed;
    [SerializeField] private float JumpForce;

    [SerializeField] private Camera PlayerCamera;

    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask GroundMask;

    private Controls controls;
    private CharacterController controller;

    private Vector2 direction;
    private bool isjumping;

    private Vector3 PlayerDirection;
    private Vector3 DirectionToMove;

    private float GroundDistance;
    private bool IsGrounded;


    private void OnEnable()
    {
        controls = new Controls();
        controls.Enable();

        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMoveCanceled;

        controls.Player.Jump.performed += OnJumpPerformed;
        controls.Player.Jump.canceled += OnJumpCanceled;

        controller = GetComponent<CharacterController>();

        GroundDistance = 0.4f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DirectionToMove = ApplyMove() + ApplyJump() + ApplyGravity();
        controller.Move(DirectionToMove * Time.deltaTime);

        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        direction = obj.ReadValue<Vector2>();
        PlayerDirection = new Vector3(direction.x, 0, direction.y); //placer cette ligne dans la fonction a la place de l'Update est plus opti
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        direction = Vector2.zero;
        PlayerDirection = Vector3.zero;
    }

    public void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        isjumping = true;
        Debug.Log("Yes !");
    }

    private void OnJumpCanceled(InputAction.CallbackContext obj)
    {
        isjumping = false;
    }

    private Vector3 ApplyMove()
    {
        if(PlayerDirection == Vector3.zero)
        {
            return Vector3.zero;
        }

        var rotation = Quaternion.LookRotation(PlayerDirection);
        rotation *= Quaternion.Euler(0, PlayerCamera.transform.rotation.eulerAngles.y, 0); //on ajoute a la rotation du joueur, la rotation en y de la camera
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, TurnSpeed * Time.deltaTime);

        var MoveDirection = rotation * Vector3.forward; //il va se déplacer tout droit mais orienté selon la rotation
        return MoveDirection.normalized * Speed; //faudrait faire une variable sérialisée pour changer la vitesse du perso
    }

    private Vector3 ApplyGravity()
    {
        var DirectionToFall = new Vector3(0, Gravity, 0);
        return DirectionToFall;
    }

    private Vector3 ApplyJump()
    {
        if(isjumping == false)
        {
            return Vector3.zero;
        }

        var ForceJump = new Vector3(0, JumpForce, 0);
        return ForceJump;
    }
}

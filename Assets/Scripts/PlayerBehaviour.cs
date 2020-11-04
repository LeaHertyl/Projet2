using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float MaxSpeed;

    //[SerializeField] private float TurnSpeed;
    
    private Controls controls;
    private Vector2 direction;
    private Vector3 PlayerDirection;

    private Vector2 AimDirection;

    private Rigidbody Myrb;

    /*private Vector3 playerdirection;
    private Vector3 relativdirection;

    private Vector3 LookForward;
    private Quaternion PlayerRotation;*/


    private void OnEnable()
    {
        controls = new Controls();
        controls.Enable();

        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMoveCanceled;

        controls.Player.Aim.performed += OnAimPerformed;

        Myrb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerDirection = new Vector3(direction.x, 0, direction.y);

        if(Myrb.velocity.sqrMagnitude < MaxSpeed)
        {
            Myrb.AddForce(PlayerDirection * Speed);
        }

        /*relativdirection = Camera.main.transform.TransformDirection(playerdirection);
        relativdirection.y = 0;
        relativdirection.Normalize();

        LookForward = Vector3.RotateTowards(this.transform.forward, relativdirection, TurnSpeed * Time.fixedDeltaTime, 0);
        PlayerRotation = Quaternion.LookRotation(LookForward);

        Myrb.MovePosition(Myrb.position + relativdirection * Speed);
        Myrb.MoveRotation(PlayerRotation);*/

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
        AimDirection = obj.ReadValue<Vector2>();
    }
}

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

    [SerializeField] LayerMask GroundMask;

    [SerializeField] private HealthBar healthBarAffiche;
    [SerializeField] private HealthBar healhBarPlayer;
    [SerializeField] private int MaxHealth;
    [SerializeField] private int currentHealth;

    [SerializeField] private Transform PlayerFeet;

    private Controls controls;
    private CharacterController controller;

    private Vector2 direction;
    private bool isjumping;

    public bool isPicking;

    private Vector3 PlayerDirection;
    private Vector3 DirectionToMove;
    private Vector3 MoveDirection;

    /*
    private RaycastHit Camhit; //RaycastHit pour avoir des informations sur l'objet hit par le raycast
    private Ray Camraycast;
    [SerializeField] private float MaxDistanceToPick;

    [SerializeField] private LayerMask FoodLayerMask;
    [SerializeField] private LayerMask FruitLayerMask;*/


    private void OnEnable()
    {
        controls = new Controls();
        controls.Enable();

        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMoveCanceled;

        controls.Player.Jump.performed += OnJumpPerformed;
        controls.Player.Jump.canceled += OnJumpCanceled;

        controls.Player.Pick.performed += OnPickPerformed;
        /*
        controls.Player.Pick.canceled += OnPickCanceled;*/

        controller = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        healthBarAffiche.SetMaxHealth(MaxHealth);
        healhBarPlayer.SetMaxHealth(MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        DirectionToMove = ApplyMove() + ApplyJump() + ApplyGravity();
        controller.Move(DirectionToMove * Time.deltaTime);

        /*
        Debug.DrawRay(PlayerCamera.transform.position, transform.TransformDirection(Vector3.forward) * MaxDistanceToPick, Color.red); //permet d'afficher le rayon

        var Foodraycast = Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out Camhit, MaxDistanceToPick, FoodLayerMask);
        var Fruitraycast = Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out Camhit, MaxDistanceToPick, FruitLayerMask);

        
        //l'origine du raycast,sa direction, les informations sur l'objet collide, la distance max de l'objet collide, le Layer sur lequel sont les objets qu'on veut collider
        if (Foodraycast) 
        {
            Interactable interactable = Camhit.collider.GetComponent<Interactable>();

            if(interactable != null)
            {
                Debug.Log("wut");
            }
        }

        if (Fruitraycast)
        {
            Interactable interactable = Camhit.collider.GetComponent<Interactable>();

            if(interactable != null)
            {
                Debug.Log("ah");
            }
            //Affichage de quel bouton on doit enclencher pour ramasser l'objet
            //si on clique dessus : l'objet disparait et la vie du joueur remonte
        }*/
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
        TakeDamage(20);
    }

    private void OnJumpCanceled(InputAction.CallbackContext obj)
    {
        isjumping = false;
    }

    public void OnPickPerformed(InputAction.CallbackContext obj)
    {
        isPicking = true;
        Debug.Log("is picking input activated");
    }

    /*
    public void OnPickCanceled(InputAction.CallbackContext obj)
    {
        isPicking = false;
    }*/

    private Vector3 ApplyMove()
    {
        if(PlayerDirection == Vector3.zero)
        {
            var rotation2 = Quaternion.LookRotation(PlayerDirection);
            rotation2 *= Quaternion.Euler(0, PlayerCamera.transform.rotation.eulerAngles.y, 0); //on ajoute a la rotation du joueur, la rotation en y de la camera
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation2, TurnSpeed * Time.deltaTime);

            MoveDirection = rotation2 * Vector3.zero;
            return MoveDirection.normalized;
        }

        var rotation = Quaternion.LookRotation(PlayerDirection);
        rotation *= Quaternion.Euler(0, PlayerCamera.transform.rotation.eulerAngles.y, 0); //on ajoute a la rotation du joueur, la rotation en y de la camera
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, TurnSpeed * Time.deltaTime);

        MoveDirection = rotation * Vector3.forward; //il va se déplacer tout droit mais orienté selon la rotation
        return MoveDirection.normalized * Speed;
    }

    private Vector3 ApplyGravity()
    {
        var startRaycastPos = PlayerFeet.position;
        var Groundraycast = Physics.Raycast(startRaycastPos, Vector3.down, 0.1f, GroundMask); //point de depart, direction, taille (0.1f = 10cm), masque

        var DirectionToFall = Vector3.zero;

        if (Groundraycast)
        {
            DirectionToMove.y = 0;
        }
        else
        {
            //on met time.deltatime ici en + de l'update parce qu'on veut par rapport au temps ecoule au carre (cette valeur doit etre mutliplie par elle meme pour appliquer la gravite)
            DirectionToFall = new Vector3(0, DirectionToMove.y + Gravity * Time.deltaTime, 0); 
        }

        return DirectionToFall;
    }

    private Vector3 ApplyJump()
    {
        if (!isjumping || DirectionToMove.y != 0) //on verifie si on est déjà en train de sauter ou si on est train de tomber
        {
            return Vector3.zero;
        }

        //vitesse = racine carre de (hauteur souhaitee x -2 x gravite)
        //la fonction Mathf.Sqrt() calcul pour nous la racine carree
        var heightSpeed = Mathf.Sqrt(JumpForce * -2 * Gravity);
        var JumpVector = new Vector3(0, heightSpeed, 0);
        return JumpVector;
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBarAffiche.SetHeatlh(currentHealth);
        healhBarPlayer.SetHeatlh(currentHealth);
    }
}
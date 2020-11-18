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

    [SerializeField] private Camera PlayerCamera;

    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask GroundMask;

    [SerializeField] private HealthBar healthBarAffiche;
    [SerializeField] private HealthBar healhBarPlayer;
    [SerializeField] private int MaxHealth;
    [SerializeField] private int currentHealth;

    private Controls controls;
    private CharacterController controller;

    private Vector2 direction;
    private bool isjumping;

    private Vector3 PlayerDirection;
    private Vector3 DirectionToMove;
    private Vector3 MoveDirection;

    private float GroundDistance;
    private bool IsGrounded;

    private RaycastHit Camhit; //RaycastHit pour avoir des informations sur l'objet hit par le raycast
    private Ray Camraycast;
    [SerializeField] private float MaxDistanceToPick;

    [SerializeField] private LayerMask FoodLayerMask;
    [SerializeField] private LayerMask FruitLayerMask;

    [SerializeField] private GameObject Head;


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
        currentHealth = MaxHealth;
        healthBarAffiche.SetMaxHealth(MaxHealth);
        healhBarPlayer.SetMaxHealth(MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        DirectionToMove = ApplyMove() + ApplyJump() + ApplyGravity();
        controller.Move(DirectionToMove * Time.deltaTime);

        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask); //raycast

        Debug.DrawRay(PlayerCamera.transform.position, transform.TransformDirection(Vector3.forward) * MaxDistanceToPick, Color.red); //permet d'afficher le rayon

        //l'origine du raycast,sa direction, les informations sur l'objet collide, la distance max de l'objet collide, le Layer sur lequel sont les objets qu'on veut collider
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out Camhit, MaxDistanceToPick, FoodLayerMask)) 
        {
            Debug.Log("wut"); 
            //Affichage de quel bouton on doit enclencher pour ramasser l'objet
            //si on clique dessus : l'objet disparait et va dans l'inventaire du joueur
        }

        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out Camhit, MaxDistanceToPick, FruitLayerMask))
        {
            Debug.Log("ah");
            //Affichage de quel bouton on doit enclencher pour ramasser l'objet
            //si on clique dessus : l'objet disparait et la vie du joueur remonte
        }
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

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBarAffiche.SetHeatlh(currentHealth);
        healhBarPlayer.SetHeatlh(currentHealth);
    }
}

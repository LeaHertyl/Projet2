using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float Gravity;
    [SerializeField] private float TurnSpeed;
    [SerializeField] private float JumpForce;

    [SerializeField] private Camera Player1Camera;

    [SerializeField] LayerMask GroundMask;

    [SerializeField] private HealthBar healthBar1Affiche;
    [SerializeField] private HealthBar healhBar1Player;
    public int MaxHealth;
    public int currentHealth1;

    [SerializeField] private Canvas XButton;
    [SerializeField] private Transform Player1Feet;

    [SerializeField] private GameObject prefabToInstantiate;
    [SerializeField] private Transform hand1Position;

    private Controls controls;
    private CharacterController controller;
    private Animator myAnim;

    private Vector2 direction;
    private bool isjumping;

    [HideInInspector] public bool isPicking;
    [HideInInspector] public bool isThrowing;
    [HideInInspector] public bool grabSomething;


    private Vector3 PlayerDirection1;
    private Vector3 DirectionToMove1;
    private Vector3 MoveDirection1;
    private Vector3 JumpVector;


    /// <summary>
    /// la fonction OnEnable() fonctionne comme la fonction Start() mais est appelee a chaque fois que le script est active
    /// </summary>
    private void OnEnable()
    {
        //on active les inputs
        controls = new Controls();
        controls.Enable();

        //on indique quelles fonctions lancer quand l'input Move de l'action Map Player est enclenche et laché
        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMoveCanceled;

        //on indique quelles fonctions lancer quand l'input Jump de l'action Map Player est enclenche et laché
        controls.Player.Jump.performed += OnJumpPerformed;
        controls.Player.Jump.canceled += OnJumpCanceled;

        //on indique quelles fonctions lancer quand l'input Pick de l'action Map Player est enclenche et laché
        controls.Player.Pick.performed += OnPickPerformed;
        controls.Player.Pick.canceled += OnPickCanceled;

        //on indique quelles fonctions lancer quand l'input Throw de l'action Map Player est enclenche et laché
        controls.Player.Throw.performed += OnThrowPerformed;
        controls.Player.Throw.canceled += OnThrowCanceled;

        //on recupere le composant characterController de l'objet auquel ce script est associe
        controller = GetComponent<CharacterController>();
        myAnim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth1 = MaxHealth; //on indique qu'au lancement du jeu la sante du joueur est au maximum
        healthBar1Affiche.SetMaxHealth(MaxHealth); //on indique qu'au lancement du jeu la barre de vie sur l'ecran du joueur est pleine
        healhBar1Player.SetMaxHealth(MaxHealth); //on indique qu'au lancement du jeu la barre de vie au dessus du joueur est pleine

        grabSomething = false; //on indique que le booleen est false -> le joueur ne tient rien au lancement du jeu
    }

    // Update is called once per frame
    void Update()
    {
        //on donne pour valeur au Vector3 l'addition des trois vector3 correspondant a ce que les fonctions ApplyMove(), ApplyJump() et ApplyGravity retournent
        DirectionToMove1 = ApplyMove() + ApplyJump() + ApplyGravity();
        controller.Move(DirectionToMove1 * Time.deltaTime); //on applique la fonction Move au character controller de l'objet auquel est associe ce script en utilisant le Vector3 calcule ci-dessus

        var IsRunning = PlayerDirection1.x != 0 || PlayerDirection1.z != 0;
        myAnim.SetBool("isRunning1", IsRunning);

        var IsJumping = DirectionToMove1.y != 0;
        myAnim.SetBool("isJumping1", IsJumping);
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        //quand l'input est enclenche, on donne au vector2 direction la valeur d'obj
        direction = obj.ReadValue<Vector2>();
        //on transforme le Vector2 ci-dessus en Vector3 en passant 0 en paramètre y car on ne veut pas que le Player bouge sur cet axe
        PlayerDirection1 = new Vector3(direction.x, 0, direction.y); //placer cette ligne dans la fonction a la place de l'Update est plus opti
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        //quand l'input n'est pas enclenche, tous les vecteurs qui font avancer le Player passent a 0 pour qu'il s'arrete
        direction = Vector2.zero;
        PlayerDirection1 = Vector3.zero;
    }

    public void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        isjumping = true; //quand l'input est enclenche, on passe le booleen a true
        Debug.Log("Yes !");
        //TakeDamage(20);
    }

    private void OnJumpCanceled(InputAction.CallbackContext obj)
    {
        isjumping = false; //quand l'input n'est pas enclenche, on passe le booleen a false
    }

    public void OnPickPerformed(InputAction.CallbackContext obj)
    {
        isPicking = true; //quand l'input est enclenche, on passe le booleen a true
    }

    private void OnPickCanceled(InputAction.CallbackContext obj)
    {
        isPicking = false; //quand l'input n'est pas enclenche, on passe le booleen a false
    }

    public void OnThrowPerformed(InputAction.CallbackContext obj)
    {
        //quand l'input est enclenche, si le Player tient quelque chose on rentre dans la condition
        if (grabSomething)
        {
            isThrowing = true; //on passe le booleen a true pour pouvoir declancher la condition de lancement de l'objet
            grabSomething = false; //on passe le booleen a false -> indique que le Player ne tient plus rien
        }
    }

    private void OnThrowCanceled(InputAction.CallbackContext obj)
    {
        isThrowing = false; //quand l'input n'est pas enclenche, on passe le booleen a false -> le Player ne peux plus lancer
    }

    private Vector3 ApplyMove()
    {
        //si l'input de deplacement n'est pas enclenche (si la condition est rempli, ce qui est en dehors du if ne sera jamais applique)
        if(PlayerDirection1 == Vector3.zero)
        {
            var rotation2 = Quaternion.LookRotation(PlayerDirection1); //on cree une variable qui va permettre d'adapter le vector3 PlayerDirection en rotation
            rotation2 *= Quaternion.Euler(0, Player1Camera.transform.rotation.eulerAngles.y, 0); //on ajoute a la rotation du joueur, la rotation en y de la camera
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation2, TurnSpeed * Time.deltaTime); //on applique a la rotation definie ci-dessus de l'objet auquel ce script est associe

            MoveDirection1 = rotation2 * Vector3.zero; //Le Player ne va pas se deplacer mais tourner en fonction de l'orientation de la camera
            return MoveDirection1.normalized; //on retourne le Vector3 calcule ci-dessus normalise pour qu'il ait toujours la meme longueur
        }

        var rotation = Quaternion.LookRotation(PlayerDirection1); //on cree une variable qui va permettre d'adapter le vector3 PlayerDirection en rotation
        rotation *= Quaternion.Euler(0, Player1Camera.transform.rotation.eulerAngles.y, 0); //on ajoute a la rotation du joueur, la rotation en y de la camera
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, TurnSpeed * Time.deltaTime); //on applique a la rotation definie ci-dessus de l'objet auquel ce script est associe

        MoveDirection1 = rotation * Vector3.forward; //le Player va se déplacer tout droit mais oriente selon la rotation
        return MoveDirection1.normalized * Speed; //on retourne le Vector3 calcule ci-dessus multiplie par la vitesse definie du Player
    }

    private Vector3 ApplyGravity()
    {
        var startRaycastPos = Player1Feet.position; //on cree une variable correspondant a la position de l'objet reference en tant que PlayerFeet
        var Groundraycast = Physics.Raycast(startRaycastPos, Vector3.down, 0.1f, GroundMask); //point de depart du raycast, direction, taille (0.1f = 10cm), masque avec lequel il va verifier la collision

        var DirectionToFall = Vector3.zero; //on cree une variable de type Vector3 egale a 0

        //si le raycast rencontre un objet qui a le masque GroundMask -> si le Player est sur ou tres proche du sol vu que la taille 
        if (Groundraycast)
        {
            DirectionToMove1.y = 0; //le parametre Y de la variable qui defini le deplacement du Player = 0 -> on n'applique plus la gravite quand le Player ne tombe plus
        }
        else
        {
            //on met time.deltatime ici en + de l'update parce qu'on veut par rapport au temps ecoule au carre (cette valeur doit etre mutliplie par elle meme pour appliquer la gravite)
            //on applique la gravite sur l'axe y du vector3 qu'on va retourner et utiliser pour le mouvement general du Player
            DirectionToFall = new Vector3(0, DirectionToMove1.y + Gravity * Time.deltaTime, 0); 
        }

        return DirectionToFall; //on retourne le Vector3 DirectionToFall -> sa valeur depend de ce qui a ete calcule -> de si le Player est en train de tomber ou non
    }

    private Vector3 ApplyJump()
    {
        //si l'input de saut n'est pas enclenche ou que le Player bouge sur l'axy y
        if (!isjumping || DirectionToMove1.y != 0)
        {
            return Vector3.zero; //on retourne un vector3 nul -> n'aura aucune incidence sur l'addition des vector3 qui permet de calculer le mouvement du Player
        }

        //vitesse = racine carre de (hauteur souhaitee x -2 x gravite)
        //la fonction Mathf.Sqrt() calcul pour nous la racine carree
        var heightSpeed = Mathf.Sqrt(JumpForce * -2 * Gravity); //on calcule la hauteur du saut du Player en fonction de la force de son saut et de la gravite qui lui est appliquee
        JumpVector = new Vector3(0, heightSpeed, 0); //on applique la valeur calculee ci-dessus au parametre Y d'un vector3 -> le saut et la gravite n'agissent que sur l'axe Y du Player
        Debug.Log(JumpVector);

        return JumpVector; //on retourne le Vector3 calcule ci-dessus pour l'ajouter a l'additon des vector3 permettant de calculer le mouvement du Player
    }

    /// <summary>
    /// fonction publique pour y acceder dans d'autres scripts avec pour parametres un entier damage -> sa valeur sera definie lors de l'appel de la fonction
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        currentHealth1 -= damage; //on enleve la valeur des degats a la sante restante au Player a cet instant
        healthBar1Affiche.SetHeatlh(currentHealth1); //on met a jour la barre de vie affichee sur l'ecran du Player
        healhBar1Player.SetHeatlh(currentHealth1); //on met a jour la barre de vie affichee au dessus du Player
    }

    /// <summary>
    /// fonction publique pour y acceder dans d'autres scripts avec pour parametres un entier hill -> sa valeur sera definie lors de l'appel de la fonction
    /// </summary>
    /// <param name="hill"></param>
    public void Hill(int hill)
    {
        currentHealth1 += hill; //on ajoute la valeur du soin a la sante restante du Player a cet instant
        healthBar1Affiche.SetHeatlh(currentHealth1); //on met a jour la barre de vie affichee sur l'ecran du Player
        healhBar1Player.SetHeatlh(currentHealth1); //on met a jour la barre de vie affichee au dessus du Player
    }


    public void InstantiateFood()
    {
        Instantiate(prefabToInstantiate, hand1Position); //quand la fonction est appelee, on instancie le prefab reference dans l'inspector a la position du gameObject reference en tant qu'handPosition
    }

    public void InstantiateXButton()
    {
        Instantiate(XButton);
    }

    public void DestroyXButton()
    {
        Destroy(XButton);
    }

    private void OnTriggerEnter(Collider other)
    {
        //si le Player est traverse par un autre objet qui a le tag Food
        if(other.tag == "Food")
        {
            TakeDamage(20); //on lance la fonction TakeDamage() en indiquant que le Player perd 20 points de vie
        }
    }


    //INVENTORY VERSION
    /*public void InstantiateFirstFood()
    {
        var Inventory = GameObject.FindWithTag("GameManager");
        var InventoryCount = Inventory.GetComponent<Inventory>();
        inventoryObjects = InventoryCount.nmbObjects;

        if(inventoryObjects == 1)
        {
            Instantiate(prefabToInstantiate, handPosition);
        }
        //else if(inventoryObjects > 1)
        {
            //Instantiate(prefabToInstantiate, handPosition);
        }//
  
    }

    public void InstantiateOtherFood()
    {
        Instantiate(prefabToInstantiate, handPosition);
    }*/
}

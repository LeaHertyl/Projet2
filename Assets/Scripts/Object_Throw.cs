using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Throw : MonoBehaviour
{
    [SerializeField] private float throwForce;
    [SerializeField] private float radius;
    [SerializeField] private GameObject ExplosionEffect;
    [SerializeField] private AudioSource GroundExplosion;

    private bool isthrowing; //on cree un booleen
    private bool isthrowing2;
    private float distance1;
    private float distance2;
    private Rigidbody myRB; //on cree une variable de type Rigibody

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>(); //on recupere le composant Rigidbody de l'objet auquel ce script est associe
    }

    // Update is called once per frame
    void Update()
    {
        //on recupere le script PlayerBehaviour qui se trouve sur le GameObject ayant le tag Player 1
        var Player1 = GameObject.FindWithTag("Player1");
        var Player1Script = Player1.GetComponent<PlayerBehaviour>();
        var Player1Transform = Player1.GetComponent<Transform>();

        //on recupere le composant Transform qui se trouve sur le GameObject ayant le tag Camera1
        var Player1Camera = GameObject.FindWithTag("Camera1");
        var Camera1Transform = Player1Camera.GetComponent<Transform>();
        
        isthrowing = Player1Script.isThrowing; //on associe la valeur de la variable isThrowing du script PlayerBehaviour a la varibale isthrowing

        //on recupere le script PlayerBehaviour qui se trouve sur le GameObject ayant le tag Player 1
        var Player2 = GameObject.FindWithTag("Player2");
        var Player2Script = Player2.GetComponent<Player2Behaviour>();
        var Player2Transform = Player2.GetComponent<Transform>();

        //on recupere le composant Transform qui se trouve sur le GameObject ayant le tag Camera1
        var Player2Camera = GameObject.FindWithTag("Camera2");
        var Camera2Transform = Player2Camera.GetComponent<Transform>();

        isthrowing2 = Player2Script.isThrowing; //on associe la valeur de la variable isThrowing du script PlayerBehaviour a la varibale isthrowing

        distance1 = Vector3.Distance(Player1Transform.position, transform.position); //verifie la distance entre le player et l'objet a ramasser
        distance2 = Vector3.Distance(Player2Transform.position, transform.position); //verifie la distance entre le player et l'objet a ramasser

        //si isthrowing == true -> si isThrowing == true -> si le Player a enclenche l'input pour lancer les objets et qu'il avait un objet dans la main
        if (isthrowing && distance1 < distance2)
        {
            myRB.useGravity = true; //on active la gravite sur le composant Rigidbody de l'objet -> pour qu'il ait une physique et retombe apres avoir ete lance
            myRB.AddForce(Camera1Transform.forward * throwForce); //on ajoute une force a l'objet en direction de la ou regarde la camera pour le lancer devant le Player selon une force definie
        }

        if(isthrowing2 && distance2 < distance1)
        {
            myRB.useGravity = true;
            myRB.AddForce(Camera2Transform.forward * throwForce);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            Explod(); //on appelle la fonction Explod() quand l'objet en trigger un autre
        }

        if(other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {
            Explod();
        }
    }

    private void Explod()
    {
        //on stock l'objet qu'on va instancier dans une variable pour pouvoir l'appeler et y faire reference ailleurs
        Instantiate(ExplosionEffect, transform.position, transform.rotation); //on instancie l'effet de particules d'explosion au même endroit que l'objet auquel ce script est associe
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Throw : MonoBehaviour
{
    [SerializeField] private float throwForce; //on cree une variable serialisee de type float
    [SerializeField] private GameObject ExplosionEffect;

    private bool isthrowing; //on cree un booleen
    private Rigidbody myRB; //on cree une variable de type Rigibody
    private GameObject ExplosionToDestroy;

    //private Transform transform;

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
        var PlayerScript = Player1.GetComponent<PlayerBehaviour>();

        //on recupere le composant Transform qui se trouve sur le GameObject ayant le tag Camera1
        var Player1Camera = GameObject.FindWithTag("Camera1");
        var Camera1Transform = Player1Camera.GetComponent<Transform>();
        
        isthrowing = PlayerScript.isThrowing; //on associe la valeur de la variable isThrowing du script PlayerBehaviour a la varibale isthrowing

        //si isthrowing == true -> si isThrowing == true -> si le Player a enclenche l'input pour lancer les objets et qu'il avait un objet dans la main
        if (isthrowing)
        {
            myRB.useGravity = true; //on active la gravite sur le composant Rigidbody de l'objet -> pour qu'il ait une physique et retombe apres avoir ete lance
            myRB.AddForce(Camera1Transform.forward * throwForce); //on ajoute une force a l'objet en direction de la ou regarde la camera pour le lancer devant le Player selon une force definie
        }

        //si la position en y de l'objet est inferieure ou egale a -20
        if(transform.position.y <= -20)
        {
            Destroy(gameObject); //on detruit le gameObject associe a ce script -> permet de supprimer les objets quand ils ont ete lance et qu'on ne les voit plus dans la scene et qu'ils ne tombent pas a l'infini
            Destroy(ExplosionToDestroy); //on detruit le gameObject correspondant aux particules d'explosion pour qu'elles se détruisent en même temps que l'objet
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Explod(); //on appelle la fonction Explod() quand l'objet en trigger un autre
    }

    private void Explod()
    {
        //on stock l'objet qu'on va instancier dans une variable pour pouvoir l'appeler et y faire reference ailleurs
        ExplosionToDestroy = Instantiate(ExplosionEffect, transform.position, transform.rotation); //on instancie l'effet de particules d'explosion au même endroit que l'objet auquel ce script est associe
    }
}

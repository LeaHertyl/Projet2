using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float radius; //on cree une variable serialisee de type float

    private float distance1; //on cree une variable de type float
    private float distance2;
    private bool hasInteracted; //on cree un booleen
    private bool hasInteracted2;

    // Start is called before the first frame update
    void Start()
    {
        hasInteracted = false; //on indique qu'au lancement du jeu, la valeur du booleen est False
        hasInteracted2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        //on recupere le composant Transform du GameObject qui a le tag Player1
        var Player1 = GameObject.FindWithTag("Player1");
        var Player1Transform = Player1.GetComponent<Transform>();

        //on recupere le composant Transform du GameObject qui a le tag Player2
        var Player2 = GameObject.FindWithTag("Player2");
        var Player2Transform = Player2.GetComponent<Transform>();

        distance1 = Vector3.Distance(Player1Transform.position, transform.position); //verifie la distance entre le player et l'objet a ramasser
        distance2 = Vector3.Distance(Player2Transform.position, transform.position); //verifie la distance entre le player et l'objet a ramasser

        //si le joueur est a moins d'une certaine distance et qu'il n'a pas encore interagi avec l'objet
        if (distance1 <= radius && !hasInteracted)
        {
            Interact(); //on appelle la fonction Interact
            hasInteracted = true; //on passe le booléen a true pour indiquer que l'interaction a déjà eu lieu
        }

        if(distance2 <= radius && !hasInteracted2)
        {
            Interact2();
            hasInteracted2 = true;
        }

        //INVENTORY VERSION
        /*if(IsThrowing == true)
        {
            Thrown();
        }*/
    }

    private void OnDrawGizmosSelected ()
    {
        //permet d'afficher une shpère autour de l'objet pour voir la zone dans laquelle le Player peut interagir avec lui
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    /// <summary>
    /// Fonction "globale" qu'on va pouvoir modifier en fonction du type d'interraction qu'on veut avoir avec les differents objets
    /// </summary>
    public virtual void Interact()
    {
        //Indique ce qu'il va se passer une fois que le joueur a interragi avec l'objet
        //Le contenu de la fonction est override dans un autre script
    }

    public virtual void Interact2()
    {
        //Indique ce qu'il va se passer une fois que le joueur a interragi avec l'objet
        //Le contenu de la fonction est override dans un autre script
    }


    //INVENTORY VERSION
    /// <summary>
    /// Fonction globale qu'on va pouvoir modifier
    /// </summary>
    /*public virtual void Thrown()
    {
        //indique ce qui va se passer quand le joueur a laché l'objet
        Debug.Log(transform.name + "is threw");
    }*/
}

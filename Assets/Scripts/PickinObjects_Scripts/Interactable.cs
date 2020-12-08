using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float radius;

    private float distance;
    private bool hasInteracted;
    private bool isThrew;

    private bool PickingBool;
    private bool TruePick;

    private bool IsThrowing;

    // Start is called before the first frame update
    void Start()
    {
        hasInteracted = false;
    }

    // Update is called once per frame
    void Update()
    {
        var Player1 = GameObject.FindWithTag("Player1");
        var Player1Transform = Player1.GetComponent<Transform>();

        distance = Vector3.Distance(Player1Transform.position, transform.position); //verifie la distance entre le player et l'objet a ramasser

        if (distance <= radius && !hasInteracted) //si le joueur est a moins d'une certaine distance et qu'il n'a pas encore interagi avec l'objet
        {
            Interact(); //on appelle la fonction Interact
            hasInteracted = true; //on passe le booléen a true pour indiquer que l'interaction a déjà eu lieu
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
        //Debug.Log("interact with " + transform.name);
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

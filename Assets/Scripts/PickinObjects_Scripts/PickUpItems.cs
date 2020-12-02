﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : Interactable
{
    //la classe n'est pas MonoBehaviour -> ca permet d'attribuer a tous les objets qui auront ce script de deriver de la classe Interactable -> d'avoir tous ses paramètres de base
    //si on ajoute rien a ce code, tous les objets qui ont ce code auront quand meme les parametres defini dans le script Interactable
    
    [SerializeField] private Items item; //permet de referencer les items crees dans le script Items issu de la classe ScriptableObjects

    private bool WasPickedUp;
    private bool WasThrew;


    /// <summary>
    /// permet de modifier ce qui va se passer quand la fonction virtual void Interact est declenchee
    /// </summary>
    public override void Interact()
    {
        base.Interact(); //on va executer ce qui est indique de base dans la fonction, dans le script Interactable

        //en plus des actions de la fonction de base, les objets qui auront ce script exectuteront la fonction PickUp quand le player entrera dans la zone definie par le raduis du gizmos de l'objet a ramasser
        PickUp();
    }


    //INVENTORY VERSION
    /*public override void Thrown()
    {
        base.Thrown();

        RemoveFromList();
    }*/

    public void PickUp()
    {
        Debug.Log("pick up " + item.name);

        //INVENTORY VERSION
        /*WasPickedUp = Inventory.instance.Add(item); //WasPickedUp == true si un item a ete ajoute a l'inventaire

        if (WasPickedUp)
        {
            Destroy(gameObject);
            Debug.Log("objet detruit");
        }*/


        //NO INVENTORY VERSION -> refaire le if mais avec une autre condition
    }


    //INVENTORY VERSION
    /*public void RemoveFromList()
    {
        WasThrew = Inventory.instance.Remove(item);

        if(WasThrew)
        {
            Debug.Log("yey he is remove");
        }
    }*/
}

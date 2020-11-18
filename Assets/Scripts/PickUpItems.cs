using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : Interactable
{
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    private void PickUp()
    {
        Debug.Log("pickup");
        //Place l'objet dans l'inventaire
        Destroy(gameObject);
    }
}

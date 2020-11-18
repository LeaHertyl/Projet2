using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : Interactable
{
    [SerializeField] private Items item;

    private bool WasPickedUp;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    private void PickUp()
    {
        Debug.Log("pickup" + item.name);
        WasPickedUp = FindObjectOfType<Inventory>().Add(item); //dans la vidéo 04 Brackeys utilise des Singleton mais j'ai pas compris alors j'utilise la methode la moins fiable pour le moment

        if(WasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}

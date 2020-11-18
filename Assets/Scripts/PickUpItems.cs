using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : Interactable
{
    [SerializeField] private Items item;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    private void PickUp()
    {
        Debug.Log("pickup" + item.name);
        FindObjectOfType<Inventory>().Add(item);
        Destroy(gameObject);
    }
}

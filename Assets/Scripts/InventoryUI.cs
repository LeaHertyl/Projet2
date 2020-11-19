using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventoryref;

    // Start is called before the first frame update
    void Start()
    {
        inventoryref = Inventory.instance; //on appelle l'instance faite avec l'inventaire
        inventoryref.onItemChangedCallback += UpdateUI; //on veut ajouter UpdateUI a la liste de methodes a effectuer quand l'evenement onItemChangedCallback est appele
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateUI()
    {
        Debug.Log("Updating UI");
    }
}

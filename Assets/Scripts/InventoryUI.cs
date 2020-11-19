using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI; //on ajoute la fonction UpdateUI aux evenements qui doivent etre joues quand on ajoute/supprime un element dans la liste
        //ca ne fonctionne pas
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

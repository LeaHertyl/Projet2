using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform itemsParent; //on va referencer le parent de tous les Slot pour pouvoir recuperer tous ses enfants au lieu d'aller chercher les Slots un par un

    private Inventory inventoryref;
    private InventorySlot[] slots; //on cree une array composée du nombre de slots -> +/- une liste dont le nombre de composant ne peux pas changer/etre modifie (7)

    // Start is called before the first frame update
    void Start()
    {
        inventoryref = Inventory.instance; //on appelle l'instance faite avec l'inventaire
        inventoryref.onItemChangedCallback += UpdateUI; //on veut ajouter UpdateUI a la liste de methodes a effectuer quand l'evenement onItemChangedCallback est appele

        //moins performant de le mettre dans l'Update mais necessaire si les Slots sont amenes a changer
        slots = itemsParent.GetComponentsInChildren<InventorySlot>(); //on recupere tous les enfants qui ont le composants InventorySlot (bien mettre le GetComponentS au pluriel -> pls enfants a recuperer

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateUI()
    {
        Debug.Log("Updating UI");

        for (int i = 0; i < slots.Length; i++) //verifie que ca ne depasse pas la taille de l'array qui contient le nombre de slots
        {
            if(i < inventoryref.itemList.Count) //si la condition est verifiee : il y a un item a ajouter
            {
                //on recupere le slot n°i qui est dans l'array InventorySlot[]
                //on appelle la fonction AddItem sur ce slot
                //AddItem = fonction d'InventorySlot qui va determiner ce qui se passe quand l'item est ajoute dans le Slot
                //on ajoute l'item dans la liste itemList (pas sur de celle la)
                slots[i].AddItem(inventoryref.itemList[i]);
            }
            else //s'il n'y a pas d'item a ajouter
            {
                slots[i].ClearSlot(); //on effectue la fonction ClearSlot d'InventorySlot sur le slot sur lequel on n'a rien a mettre
                Debug.Log("clearslot");
                
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;

    private Items item; //tout ce que va porter le nom item ci-dessous va être ce qui a ete cree a partir du script Items

    private void Start()
    {
        icon.enabled = false;
    }

    /// <summary>
    /// fonction qui va determiner ce qui se passe quand l'item est ajoute dans le Slot
    /// </summary>
    /// <param name="newItem"></param>
    public void AddItem(Items newItem)
    {
        item = newItem;

        icon.sprite = item.icon; //on indique que l'image qui va etre affichee dans le slot est l'icone precedemment referencee de l'item
        icon.enabled = true; //on active l'affichage de l'icone
    }

    /// <summary>
    /// fonction qui va determiner ce qui se passe dans le Slot quand il est vide/quand on supprime un item
    /// </summary>
    public void ClearSlot()
    {
        Destroy(item);

        icon.sprite = null;
        icon.enabled = false;
    }

    public void UseItem()
    {
        if(item != null) //s'il y a un item a utiliser
        {
            item.Use(); //on utilise la fonction Use du script Items (qui existe de base sur tous les items crees donc)
        }
    }
}

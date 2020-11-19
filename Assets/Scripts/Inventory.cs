using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Items> itemList;

    private int SlotsNumber;

    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance founded !");
            return;
        }

        instance = this;
    }
    #endregion 

    //il faut que je me renseigne sur les delegate, j'ai pas tout compris -> ca fait en sorte que quand la methode est appelee, elle execute tous les evenements qui lui sont associes ? contient des fonctions au lieu de data ?
    public delegate void OnItemChanged(); 
    public OnItemChanged onItemChangedCallback; //on appelle/trigger cet evenement des qu'un element est ajoute ou supprime dans la liste

    // Start is called before the first frame update
    void Start()
    {
        itemList = new List<Items>();
        SlotsNumber = 7;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Add(Items item)
    {
        if(itemList.Count >= SlotsNumber)
        {
            Debug.Log("not enough space");
            return false;
        }

        itemList.Add(item);
        Debug.Log("yo"); //ca fonctionne

        if(itemList != null)

        if(onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
            Debug.Log("ici ?"); //c'est ici que ca ne fonctionne pas -> on ne rentre jamais dans le if
        }

        return true;
    }

    public void Remove(Items item)
    {
        itemList.Remove(item);

        if(onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}

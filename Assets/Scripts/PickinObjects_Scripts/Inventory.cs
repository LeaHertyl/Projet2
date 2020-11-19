using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Items> itemList; //on cree un liste contenant des objets de la classe Items -> des objets derives du script Items

    private int SlotsNumber;

    #region Singleton
    //Singleton : créer une instance à partir d'une variable statique (une variable partagee par toutes les instances de la classe)
    //cree une reference globale au projet a l'inventaire
    //variable static : elle est gardée en memoire et sera toujours la meme, meme dans des scripts differents -> c'est devenue une instance
    //on cree une variable de meme type que la classe
    public static Inventory instance; 

    private void Awake()
    {
        //si au lancement du script on trouve déjà une instance de ce nom, on met un message d'erreur
        if (instance != null)
        {
            Debug.LogWarning("More than one instance founded !");
            return;
        }
            instance = this; //permet de pouvoir acceder a cette instance partout en ecrivant Inventory.instance

    }
    #endregion 

    //evenement auquel on associe differentes methodes -> quand l'evenement se produit, toutes les methodes sont appliquees (sorte de groupe de methodes indissociables)
    //quand l'evenement se produit, on n'aura pas besoin d'appeller toutes les methodes
    //pour pouvoir modifier l'UI de l'inventaire, on veut savoir quand ce dernier a change -> quand on a ajoute ou retirer un item
    public delegate void OnItemChanged(); //defini le type du delegate
    //on veut implanter le delegate : onItemChangedCalback est l'evenement associe a ce delegate -> c'est lui qu'on va verifier avant de lancer le groupe de methodes
    public OnItemChanged onItemChangedCallback; //on appelle/trigger cet evenement des qu'un element est ajoute ou supprime dans la liste

    // Start is called before the first frame update
    void Start()
    {
        itemList = new List<Items>();
        SlotsNumber = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     /// <summary>
     /// 
     /// </summary>
     /// <param name="item"></param>
     /// <returns></returns>
    public bool Add(Items item)
    {
        if(item.isFood)
        {
            if (itemList.Count >= SlotsNumber)
            {
                Debug.Log("not enough space");
                return false; //retourne false au booleen de la fonction ->
            }

            itemList.Add(item); //on ajoute l'item concerne par cette fonction dans la liste itemList
            Debug.Log("Objet ajoute a la liste");
            //avant que je rajoute la suite, tout fonctionne tres bien jusque là

            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke(); //pour le moment ca n'invoque rien
                Debug.Log("ici ?"); //c'est ici que ca ne fonctionne pas -> on ne rentre jamais dans le if -> ne detecte pas que l'item a été ajouté a la liste
            }
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

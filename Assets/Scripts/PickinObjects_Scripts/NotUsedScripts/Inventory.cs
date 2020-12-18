using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Items> itemList; //on cree un liste contenant des objets de la classe Items -> des objets derives du script Items

    private int SlotsNumber;

    public int nmbObjects;

    private bool IsThrowing;

    /*
    private int Hillfunction;*/


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
        SlotsNumber = 7;

        //pour moi c'est ici qu'il faut venir chercher l'objet qui contient le script, puis son composant script, puis attribuer son booleen a une variable propre a ce script
        //mais ca ne fonctionne pas, quand je fais ca et que je Debug la valeur du booleen sous le 1er if ca fonctionne, des que je met item.isFood && booleen = true ca ne rentre plus dans la boucle
        //est-ce que c'est parce que, une fois que je suis dans le perimetre du radius c'est trop tard ?
        //en vrai peut etre parce que du coup la virtual fonction Interact s'est déjà appliquee
        //est-ce que j'ai vraiment besoin de devoir appuyer sur un bouton pour ramasser la bouffe ? (oui allez arrete c'est quand meme mieux)
        //en vrai osef je pense
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
        var Player1 = GameObject.FindWithTag("Player1");
        var Player1Script = Player1.GetComponent<PlayerBehaviour>();

        if (item.isFood)
        {
            if (itemList.Count >= SlotsNumber)
            {
                Debug.Log("not enough space");
                return false; //retourne false au booleen de la fonction ->
            }
            else
            {
                itemList.Add(item); //on ajoute l'item concerne par cette fonction dans la liste itemList
                Debug.Log("Objet ajoute a la liste");
                //avant que je rajoute la suite, tout fonctionne tres bien jusque là
                nmbObjects += 1;
                Debug.Log("nombre = " + nmbObjects);
                //Player1Script.InstantiateFirstFood(); -> ligne commentee pour ne pas avoir d'erreur
            }

            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }
        else if(item.isFruit)
        {
            //remonte la vie du joueur si elle est en dessous de 100
        }

        return true;
    }

    public bool Remove(Items item)
    {
        if(item.isThrown)
        {
            itemList.Remove(item);
            nmbObjects += 1;
            Debug.Log("ici");

            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }

        return true;
    }
}

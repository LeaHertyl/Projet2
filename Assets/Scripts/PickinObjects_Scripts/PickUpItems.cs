using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : Interactable
{
    //la classe n'est pas MonoBehaviour -> ca permet d'attribuer a tous les objets qui auront ce script de deriver de la classe Interactable -> d'avoir tous ses paramètres de base
    //si on ajoute rien a ce code, tous les objets qui ont ce code auront quand meme les parametres defini dans le script Interactable
    
    [SerializeField] private Items item; //permet de referencer les items crees dans le script Items issu de la classe ScriptableObjects
    
    private bool Grab; //on cree une varibale de type booleen
    private bool Grab2;
    public bool isPicked; //on cree une varibale de type booleen, publique pour pouvoir y acceder depuis un autre script
    public bool isPicked2;

    //INVENTORY VERSION
    /*private bool WasPickedUp;
    private bool WasThrew;*/

    private void Start()
    {
        isPicked = false; //on attribue la valeur false au booleen au lancement du jeu
        isPicked2 = false;
    }


    /// <summary>
    /// permet de modifier ce qui va se passer quand la fonction virtual void Interact est declenchee
    /// </summary>
    public override void Interact()
    {
        base.Interact(); //on va executer ce qui est indique de base dans la fonction, dans le script Interactable

        //en plus des actions de la fonction de base, les objets qui auront ce script exectuteront la fonction PickUp quand le player entrera dans la zone definie par le raduis du gizmos de l'objet a ramasser
        PickUp();
    }

    public override void Interact2()
    {
        base.Interact2(); //on va executer ce qui est indique de base dans la fonction, dans le script Interactable

        //en plus des actions de la fonction de base, les objets qui auront ce script exectuteront la fonction PickUp quand le player entrera dans la zone definie par le raduis du gizmos de l'objet a ramasser
        PickUp2();
    }


    public void PickUp()
    {
        //Debug.Log("pick up " + item.name);

        //on recupere le script PlayerBehaviour qui se trouve sur le GameObject ayant le tag Player 1
        var Player1 = GameObject.FindWithTag("Player1");
        var Player1Script = Player1.GetComponent<PlayerBehaviour>();

        var Player1currentHealth = Player1Script.currentHealth1; //on associe la valeur de la variable currentHealth du script PlayerBehaviour a la varibale Player1currentHealth
        var Player1maxHealth = Player1Script.MaxHealth; //on associe la valeur de la variable MaxHealth du script PlayerBehaviour a la varibale Player1maxhealth

        Grab = Player1Script.grabSomething; //on associe la valeur de la variable grabsomething du script PlayerBehaviour a la varibale Grab


        if (item.isFood)
        {
            //si la variable Grab est fausse -> si la variable grabsomething de PlayerBehaviour est fausse -> si le Player ne tient rien dans sa main
            if (Grab == false)
            {
                //isGrabed = true;
                Player1Script.grabSomething = true; //on passe la variable grabsomething du script PlayerBehaviour a true -> indique qu'on tient quelque chose et va permettre de le lancer
                Player1Script.InstantiateFood(); //on lance la fonction InstantiateFood du script PlayerBehaviour -> fait apparaitre la nourriture a lancer dans la main du Player

                isPicked = true; //on passe le booleen a true -> va indiquer au point de spwan qu'il est vide

                Destroy(gameObject); //on detruit le GameObject auquel ce script est associe
            }

        }
        //si le booleen True de l'item avec lequel on veut interagir est isFruit
        else if (item.isFruit)
        {

            //si la sante actuelle du Player n'est pas egale a sa sante maximale -> s'il a subit des degats
            if(Player1currentHealth != Player1maxHealth)
            {
                Player1Script.Hill(20); //on lance la fonction Hill du script PlayerBehaviour

                isPicked = true; //on passe le booleen a true -> va indiquer au point de spwan qu'il est vide

                Destroy(gameObject);//on detruit le GameObject auquel ce script est associe
            }

        }

        }

    public void PickUp2()
    {
        //on recupere le script PlayerBehaviour qui se trouve sur le GameObject ayant le tag Player 2
        var Player2 = GameObject.FindWithTag("Player2");
        var Player2Script = Player2.GetComponent<Player2Behaviour>();

        var Player2currentHealth = Player2Script.currentHealth2; //on associe la valeur de la variable currentHealth du script PlayerBehaviour a la varibale Player1currentHealth
        var Player2maxHealth = Player2Script.MaxHealth; //on associe la valeur de la variable MaxHealth du script PlayerBehaviour a la varibale Player1maxhealth

        Grab2 = Player2Script.grabSomething; //on associe la valeur de la variable grabsomething du script PlayerBehaviour a la varibale Grab

        if(item.isFood)
        {
            if (Grab2 == false)
            {
                Player2Script.grabSomething = true;
                Player2Script.InstantiateFood();

                isPicked2 = true;

                Destroy(gameObject);
            }
        }
        else if (item.isFruit)
        {
            //dans ce cas de figure, ca va soigner les deux joueur en meme temps si leur vie est != du max
            if (Player2currentHealth != Player2maxHealth)
            {
                Player2Script.Hill(20);

                isPicked2 = true;

                Destroy(gameObject);
            }
        }

    }


        //INVENTORY VERSION
        /*WasPickedUp = Inventory.instance.Add(item); //WasPickedUp == true si un item a ete ajoute a l'inventaire

        if (WasPickedUp)
        {
            Destroy(gameObject);
            Debug.Log("objet detruit");
        }*/


        //NO INVENTORY VERSION -> refaire le if mais avec une autre condition
    

    //INVENTORY VERSION
    /*public override void Thrown()
    {
        base.Thrown();

        RemoveFromList();
    }*/


    //INVENTORY VERSION
    /*public void RemoveFromList()
    {
        WasThrew = Inventory.instance.Remove(item);

        if(WasThrew)
        {
            Debug.Log("yey he is remove");
        }
    }*/
}

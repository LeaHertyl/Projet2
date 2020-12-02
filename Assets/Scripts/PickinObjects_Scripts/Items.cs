using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//permet de créer des nouveaux items depuis le menu des Assets de Unity
//indique a Unity la methode qu'on veut utiliser pour creer nos items
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Items : ScriptableObject
{
    //permet de créer une nouvelle classe de type ScriptableObject -> des asset custom qu'on va pouvoir creer dans son projet, avec des proprietes predefinies
    //permet de créer des objets qui auront directement pour paramètre tout ce qu'on va écrire dans ce script
    //sorte de modèle de base pour créer des objets plus facilement/rapidement
    //ci-dessous on va donc créer des paramètres de bases, vides ou nul par défaut, qu'on pourra modifier ensuite sur les différents objets créés
    //va permettre de créer rapidement tous les items de nourriture
    //on va créer des variables publiques pour pouvoir les récupérer dans d'autres scripts

    new public string name = "New Item";
    public Sprite icon = null;
    public bool isFood = true;
    public bool isFruit = false;

    //public bool isThrown = false;

    //virtual parce qu'on veut peut etre qu'il ne se passe pas la meme chose quand on utilise des objets differents
    public virtual void Use()
    {
        //Use the item
        //Something happen -> lancer l'item

        //pour le moment on ne peut meme pas debug la fonction parce que je ne sais pas comment utiliser l'item -> Brackeys fait un evenement OnClick sur le boutton de l'inventaire
        //j'aimerais simplement utiliser le premier item de la liste
        Debug.Log("using " + name); 
    }

}

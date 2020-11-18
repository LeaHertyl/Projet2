using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//permet de créer des nouveaux items depuis le menu des Assets de Unity
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Items : ScriptableObject
{
    //permet de créer une nouvelle classe de type ScriptableObject
    //permet de créer des objets qui auront directement pour paramètre tout ce qu'on va écrire dans ce script
    //sorte de modèle de base de pour créer des objets plus facilement/rapidement
    //ci-dessous on va donc créer des paramètres de bases, vides ou nul par défaut, qu'on pourra modifier ensuite sur les différents objets créés
    //va permettre de créer rapidement tous les items de nourriture
    //on va créer des variables publiques pour pouvoir les récupérer dans d'autres scripts

    new public string name = "New Item"; 
    public Sprite icon = null;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scriptableobject : ScriptableObject
{
    //permet de créer une nouvelle classe de type ScriptableObject
    //permet de créer des objets qui auront directement pour paramètre tout ce qu'on va écrire dans ce script
    //sorte de modèle de base de pour créer des objets plus facilement/rapidement
    //ci-dessous on va donc créer des paramètres de bases, vides ou nul par défaut, qu'on pourra modifier ensuite sur les différents objets créés
    //va permettre de créer rapidement tous les items de nourriture

    [SerializeField] private string Objectname = "New Item";
    [SerializeField] private Sprite icon = null;

}

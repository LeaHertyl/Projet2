using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroy : MonoBehaviour
{
    [SerializeField] private float Timer; //on cree une variable serialisee pour pouvoir referencer sa valeur dans l'inspector de type float

    private void Update()
    { 
        //on indique que ce qui se passe si la valeur de la variable Timer est superieure a 0
        if(Timer > 0f)
        {
            Timer -= Time.deltaTime; //on fait diminuer cette variable
        }
        //on indique ce qui se passe sinon -> si la valeur de la variable est inferieur a 0
        else
        {
            Destroy(gameObject); //on detruit le GameObject auquel ce script est associe
        }
    }
}

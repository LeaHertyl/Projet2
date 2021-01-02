using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] Transform OtherPlayercamera; //on cree une variable serialisee pour referencer la position de la camera du Player adverse

    /// <summary>
    /// fonction qui est lancee toutes les frames mais apres la fonction Update, ce qui permet de recuperer la nouvelle position de la camera du Player adverse
    /// </summary>
    void LateUpdate()
    {
        transform.LookAt(transform.position + OtherPlayercamera.forward); //on fait en sorte que la barre de vie en 2D soit toujours face à la camera du Player adverse et donc toujours visible par lui
    }
}

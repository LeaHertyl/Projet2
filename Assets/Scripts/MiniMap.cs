using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] Transform Player; //on reference dans l'inspector le player dont on veut recuperer la position

    private Vector3 newPosition; //on cree un Vector3 qui va permettre de calculer la nouvelle position que la camera doit prendre

    /// <summary>
    /// la fonction LateUpdate va s'effectuer apres Update et FixedUpdate et va permettre de recuperer la nouvelle position du Player après qu'il ait bouge
    /// </summary>
    private void LateUpdate()
    {
        newPosition = Player.position; //on associe au Vector3 la position du Player
        newPosition.y = transform.position.y; //on change la valeur de la position de la camera sur l'axe y car on veut qu'elle reste au niveau ou elle a ete placee et pas qu'elle se mette au niveau du Player
        transform.position = newPosition; //on applique la nouvelle position calculee a la position de la camera pour qu'elle suive le Player en restant au dessus de lui

        transform.rotation = Quaternion.Euler(90f, Player.eulerAngles.y, 0f); //permet de faire tourner la caméra sur l'axe y en fonction de la rotation du Player
    }
}

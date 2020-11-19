using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private Transform player;

    private float distance;
    private bool hasInteracted;

    private bool PickingBool;
    private bool TruePick;

    // Start is called before the first frame update
    void Start()
    {
        hasInteracted = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= radius && !hasInteracted)
        {
            Interact();
            hasInteracted = true;
        }
    }

    private void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    /// <summary>
    /// Fonction "globale" qu'on va pouvoir modifier en fonction du type d'interraction qu'on veut avoir avec les differents objets
    /// </summary>
    public virtual void Interact()
    {
        //Indique ce qu'il va se passer une fois que le joueur a interragi avec l'objet
        Debug.Log("interact with " + transform.name);
    }
}

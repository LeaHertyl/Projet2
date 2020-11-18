using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private Transform player;

    private float distance;
    private bool hasInteracted;


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

    public virtual void Interact()
    {
        Debug.Log("interact with " + transform.name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Throw : MonoBehaviour
{
    private bool isthrowing;
    private int inventoryObjects;

    private Rigidbody myRB;
    //private Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        //transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        var Player1 = GameObject.FindWithTag("Player1");
        var PlayerScript = Player1.GetComponent<PlayerBehaviour>();
        
        isthrowing = PlayerScript.isThrowing;

        if (isthrowing == true)
        {
            myRB.useGravity = true;
            Debug.Log(isthrowing);
        }

        if(transform.position.y <= -20)
        {
            Destroy(gameObject);
        }
    }
}

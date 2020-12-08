using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Throw : MonoBehaviour
{
    [SerializeField] private float throwForce;

    private bool isthrowing;
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

        var Player1Camera = GameObject.FindWithTag("Camera1");
        var Camera1Transform = Player1Camera.GetComponent<Transform>();
        
        isthrowing = PlayerScript.isThrowing;

        if (isthrowing == true)
        {
            myRB.useGravity = true;
            myRB.AddForce(Camera1Transform.forward * throwForce);
            //Debug.Log(isthrowing);
        }

        if(transform.position.y <= -20)
        {
            Destroy(gameObject);
        }
    }
}

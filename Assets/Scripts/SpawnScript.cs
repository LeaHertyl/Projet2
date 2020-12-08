using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject[] ObjectToCollect;
    private int random;
    private GameObject obj;

    private bool Grabed;

    private bool isFull;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(0, ObjectToCollect.Length);
        obj = ObjectToCollect[random];
        Debug.Log(obj);
        Instantiate(obj, transform.position, Quaternion.identity);

        isFull = true;

        timer = 10;
    }

    // Update is called once per frame
    void Update()
    {

        var objectscript = obj.GetComponent<PickUpItems>();
        Grabed = objectscript.isPicked;

        Debug.Log(Grabed);

        if (isFull == false)
        {
            timer = 10;

            timer -= Time.deltaTime;
            //Debug.Log(timer);
        }

        if(timer <= 0)
        {
            Debug.Log("hello");
        }


        /*if (Grabed == true)
        {
            Debug.Log("grabed is true");
        }*/

    }
}

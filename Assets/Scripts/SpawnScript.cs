using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject[] ObjectToCollect;
    private int random;
    private GameObject obj;

    private float timer;

    private PickUpItems pick;

    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(0, ObjectToCollect.Length);
        obj = ObjectToCollect[random];

        Debug.Log(obj);
        pick = Instantiate(obj, transform.position, Quaternion.identity).GetComponent<PickUpItems>();

        timer = 2;
    }

    // Update is called once per frame
    void Update()
    {

        if (pick != null)
        {

            if(pick.isPicked)
            {
                pick = null;
            }

        }
        else
        {
            timer -= Time.deltaTime;
        }

        if(timer <= 0)
        {
            timer = 2;

            pick = Instantiate(obj, transform.position, Quaternion.identity).GetComponent<PickUpItems>();
        }


        /*if (Grabed == true)
        {
            Debug.Log("grabed is true");
        }*/

    }
}

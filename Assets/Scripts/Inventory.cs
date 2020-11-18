using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Items> itemList;

    private int SlotsNumber;

    // Start is called before the first frame update
    void Start()
    {
        itemList = new List<Items>();
        SlotsNumber = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Add(Items item)
    {
        if(itemList.Count >= SlotsNumber)
        {
            Debug.Log("not enough space");
            return false;
        }
        else
        {
            itemList.Add(item);
            return true;
        }
    }

    public void Remove(Items item)
    {
        itemList.Remove(item);
    }
}

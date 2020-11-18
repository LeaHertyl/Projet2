using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Items> itemList;

    // Start is called before the first frame update
    void Start()
    {
        itemList = new List<Items>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(Items item)
    {
        itemList.Add(item);
    }

    public void Remove(Items item)
    {
        itemList.Remove(item);
    }
}

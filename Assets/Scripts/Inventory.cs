using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Items> itemList;

    private int SlotsNumber;

    //il faut que je me renseigne sur les delegate, j'ai pas tout compris -> ca fait en sorte que quand la methode est appelee, elle execute tous les evenements qui lui sont associes ?
    private delegate void OnItemChanged(); 
    private OnItemChanged onItemChangedCallback;

    // Start is called before the first frame update
    void Start()
    {
        itemList = new List<Items>();
        SlotsNumber = 7;
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

        itemList.Add(item);
        return true;

        if(onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    public void Remove(Items item)
    {
        itemList.Remove(item);
    }
}

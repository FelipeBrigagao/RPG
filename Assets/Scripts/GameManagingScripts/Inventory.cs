using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one inventory!");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    


    public int inventorySize = 20;

    public List<Item> itens = new List<Item>();

    public bool AddItem(Item item)
    {
        if (!item.isDefaultItem)
        {
            if(itens.Count >= inventorySize)
            {
                Debug.Log("Inventory full");
                return false;
            }

            itens.Add(item);

            onItemChangedCallBack?.Invoke();

        }
        return true;
    
    }

    public void RemoveItem(Item item)
    {
        itens.Remove(item);

        onItemChangedCallBack?.Invoke();
    }


}

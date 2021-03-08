using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryUIGO;

    public Transform slotsParent;

    Inventory inventory;

    InventorySlot[] slots;
    
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;

        inventory.onItemChangedCallBack += UpdateUI;


        slots = slotsParent.GetComponentsInChildren<InventorySlot>();

    }


    // Update is called once per frame
    void Update()   
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUIGO.SetActive(!inventoryUIGO.activeSelf);
        }
    }

    void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.itens.Count)
            {
                slots[i].AddItem(inventory.itens[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    } 
}

using UnityEngine;

public class ItemPickup : Interactable
{
    //Scriptable Object: Objeto criado no project que funciona como um molde para todos os objetos, guardando todas as instâncias que esses terão, como nome, Icone, efeitos entre outros.
    public Item item; 

    public override void Interact()
    {
        base.Interact();

        PickUp();

    }

    void PickUp()
    {
        Debug.Log("Pick up " + item.name );

        bool wasPickedUp = Inventory.instance.AddItem(this.item);

        if (wasPickedUp)
        {
            Destroy(gameObject);

        }
    }

}

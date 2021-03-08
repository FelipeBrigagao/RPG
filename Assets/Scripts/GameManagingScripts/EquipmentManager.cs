using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    #region Singleton
    
    public static EquipmentManager instance;
    
    private void Awake()
    {

        if(instance != null)
        {
            Debug.LogWarning("Equipment panel duplicated");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnEquipmentChanged(Equipment newEquip, Equipment oldEquipment);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    public Transform shield;
    public Transform sword;


    public Equipment[] defaultItens;
    public SkinnedMeshRenderer targetMesh;
    Equipment[] currentEquipment; //Equipamentos que estão equipados atualmente no player
    SkinnedMeshRenderer[] currentMeshes; //Mesh de cada equipamento equipado no player

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length; //busca o enum que está dentro do script do equipment e vê o tamanho desse pra saber quantas partes de equipamento serão necessárias 
        currentEquipment = new Equipment[numSlots];

        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaultItems();

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnnequipAll();
        }
    }
   

    /*
    public void Equip(Equipment newItem)
    {
        int equipSlotIndex = (int) newItem.equipmentSlot; //o equipmentSlot corresponde a um enum no Equipment, e dependendo da posição desse slot um index é atribuido a esse

        Equipment oldItem = Unnequip(equipSlotIndex); //Verifica no unnequip se tinha um item já equipado, para retorná-lo ao inventário

        onEquipmentChanged?.Invoke(newItem, oldItem);

        currentEquipment[equipSlotIndex] = newItem;


        SetEquipmentBlendShapes(newItem, 100);

        //instancia o gfx do equipamento, setando o corpo do player como pai e atribuindo os bones do corpo ao do equipamento, para esse se movimentar corretamente
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.SetParent(targetMesh.transform);

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[equipSlotIndex] = newMesh;

    }

    */

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentSlot;

        Equipment oldItem = Unnequip(slotIndex);


        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, null);
        }

        SetEquipmentBlendShapes(newItem, 100);
        currentEquipment[slotIndex] = newItem;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        currentMeshes[slotIndex] = newMesh;

        if (newItem != null && newItem.equipmentSlot == EquipmentSlot.Weapon)
        {
            newMesh.rootBone = sword;
        }
        else if (newItem != null && newItem.equipmentSlot == EquipmentSlot.Shield)
        {
            newMesh.rootBone = shield;
        }
        else
        {
            newMesh.transform.parent = targetMesh.transform;
            newMesh.bones = targetMesh.bones;
            newMesh.rootBone = targetMesh.rootBone;
        }


    }

    public Equipment Unnequip(int equipmentSlotIndex)
    {

        Equipment oldItem = null;

        if (currentEquipment[equipmentSlotIndex] != null)
        {
            if(currentMeshes[equipmentSlotIndex] != null)
            {
                Destroy(currentMeshes[equipmentSlotIndex].gameObject);
            }


            oldItem = currentEquipment[equipmentSlotIndex];
            inventory.AddItem(oldItem);
            currentEquipment[equipmentSlotIndex] = null;

            SetEquipmentBlendShapes(oldItem, 0);

            onEquipmentChanged?.Invoke(null, oldItem);
        }
        else
        {
            Debug.Log("No itens equipped in this part");
        }

        return oldItem;
    }

    public void UnnequipAll()
    {
        for(int i = 0; i < currentEquipment.Length; i++)
        {
            Unnequip(i);
        }
        EquipDefaultItems();
    }

    void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach(EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            Debug.Log("Equipment: " + item.name + "Region: " + blendShape);
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }


    void EquipDefaultItems()
    {
        foreach(Equipment item in defaultItens)
        {
            Equip(item);
        }
    }

}

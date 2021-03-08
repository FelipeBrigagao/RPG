using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{


    private void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChange;
    }


    void OnEquipmentChange(Equipment newItem, Equipment oldItem)
    {

        if (newItem != null)
        {
            armor.AddModifier(newItem.armourModifier);
            damage.AddModifier(newItem.damageModifier);

        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armourModifier);
            damage.RemoveModifier(oldItem.damageModifier);

        }
    }


    public override void Die()
    {
        base.Die();

        PlayerManager.instance.KillPlayer();
    }
}

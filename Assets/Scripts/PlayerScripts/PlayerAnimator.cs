using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    public WeaponAnimations[] weaponAnimations;

    Dictionary<Equipment, AnimationClip[]> weaponAnimationsDict;


    protected override void Start()
    {
        base.Start();

        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;

        weaponAnimationsDict = new Dictionary<Equipment, AnimationClip[]>();

        foreach(WeaponAnimations a in weaponAnimations)
        {
            weaponAnimationsDict.Add(a.weapon, a.clips);
        }


    }

    public void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null && newItem.equipmentSlot == EquipmentSlot.Weapon)
        {
            anim.SetLayerWeight(1, 1); //fechar a mão do character se estiver segurando alguma arma

            if (weaponAnimationsDict.ContainsKey(newItem)) // verifica se tem a arma equipada no dictionary de animações por arma
            {
                currentAttackAnimations = weaponAnimationsDict[newItem]; //se tem, as animações de ataque serão as correspondentes àquela arma
            }

        
        } else if (newItem == null && oldItem != null && oldItem.equipmentSlot == EquipmentSlot.Weapon)
        {
            anim.SetLayerWeight(1, 0);
            currentAttackAnimations = defaultAttackAnimations;

        }

        if (newItem != null && newItem.equipmentSlot == EquipmentSlot.Shield)
        {
            anim.SetLayerWeight(2, 1);

        }
        else if (newItem == null && oldItem != null && oldItem.equipmentSlot == EquipmentSlot.Shield)
        {
            anim.SetLayerWeight(2, 0);
        }
    }

    [System.Serializable]
    public struct WeaponAnimations
    {
        public Equipment weapon;
        public AnimationClip[] clips;
    }
}

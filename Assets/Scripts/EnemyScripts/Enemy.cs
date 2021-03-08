using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemyStats))]
public class Enemy : Interactable
{
    GameObject player;
    EnemyStats enStats;

    private void Start()
    {
        player = PlayerManager.instance.player;
        enStats = GetComponent<EnemyStats>();
    }

    public override void Interact()
    {
        base.Interact();

        //Attack enemy

        CharacterCombat playerCombat = player.GetComponent<CharacterCombat>(); //script de combate do player, onde é mandado os status do enemy, que é onde está o script atual 
        playerCombat.Attack(enStats);
    }
}

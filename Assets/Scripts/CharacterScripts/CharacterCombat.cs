using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    CharacterStats myStats;

    float attackDelay = .6f;
    float attackCooldown = 0f;
    float combatCooldown = 5f;
    float lastAttackTime = 0f;



    public bool inCombat {get; private set;}
    public event Action OnAttack;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;        

        if(Time.time - lastAttackTime > combatCooldown)
        {
            inCombat = false;
        }
    }

    public void Attack(CharacterStats targetStats)
    {
    
        if(attackCooldown <= 0)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            OnAttack?.Invoke();

            attackCooldown = 1 / myStats.attackSpeed.GetValue();

            inCombat = true;

            lastAttackTime = Time.time;

        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.damage.GetValue());

        if(stats.currentHealth <= 0)
        {
            inCombat = false;
        }

    }
   
}

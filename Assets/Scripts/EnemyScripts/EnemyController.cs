using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    float lookRadius = 10f;

    float turnToLookSpeed = 5f;

    Transform target;
    NavMeshAgent agent;
    CharacterCombat enemyCombat;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        target = PlayerManager.instance.player.transform;

        enemyCombat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);


            if(distance <= agent.stoppingDistance)
            {
                //Attack target

                CharacterStats targetStats = target.GetComponent<CharacterStats>();

                if(targetStats != null)
                {
                    enemyCombat.Attack(targetStats);

                }


                LookAtPlayer();
            }


        }

    }

    void LookAtPlayer()
    {
        //agent.updateRotation = false;

        Vector3 lookDirection = (target.position - transform.position).normalized;
        Quaternion lookAngle = Quaternion.LookRotation(new Vector3(lookDirection.x, 0, lookDirection.z), Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookAngle, turnToLookSpeed * Time.deltaTime);

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}

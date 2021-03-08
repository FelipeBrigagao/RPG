using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    float rotationSpeed;
    float targetInteractionDistance;

    Transform targetInteractionPoint;
    Transform target;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 5f * Time.deltaTime;

        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }



    IEnumerator FollowingTarget()
    {
        while(target != null)
        {
            MoveToPoint(targetInteractionPoint.position);

            if (Vector3.Distance(transform.position, targetInteractionPoint.position) <= targetInteractionDistance)
            {
                agent.updateRotation = false;
                LookAtTarget(target.position);
            }
            else if(!agent.updateRotation)
            {

                agent.updateRotation = true;
            }
            yield return null;
        }
    }


    public void FollowTarget(Interactable newTarget)
    {
        targetInteractionDistance = newTarget.interactingRadius;

        agent.stoppingDistance = targetInteractionDistance * .8f;

        target = newTarget.transform;
        targetInteractionPoint = newTarget.interactionPoint;

        StartCoroutine(FollowingTarget());
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0;
        agent.updateRotation = true;

        target = null;
        targetInteractionPoint = null;
    }

    void LookAtTarget(Vector3 targetPosition)
    {
        Vector3 targetDirection = (targetPosition - transform.position).normalized;
        float targetDirAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, targetDirAngle, 0);
        //targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        //transform.eulerAngles = new Vector3(0, targetDirAngle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
    }


}

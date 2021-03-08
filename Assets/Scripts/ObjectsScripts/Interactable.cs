using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactingRadius = 3f;

    public Transform interactionPoint;

    bool onFocus = false;
    bool hasInteracted = false;
    
    Transform player;



    private void Update()
    {
        if (onFocus && !hasInteracted)
        {
            float distanceToPlayer = Vector3.Distance(interactionPoint.position, player.position);
            if (distanceToPlayer <= interactingRadius)
            {
                hasInteracted = true;
                Interact();   
            }
        }
    }


    //Método em que cada objeto terá o seu próprio, por isso será sobrescrito na classe referente ao objeto
    public virtual void Interact()
    {
        //Debug.Log("Interacted with " + transform.name);
        //Gonna be overwriten
    }

    public void ObjectIsOnFocus(Transform playerTransform)
    {
        hasInteracted = false;
        onFocus = true;
        player = playerTransform;
    }

    public void RemoveObjectFocus()
    {
        hasInteracted = false;
        onFocus = false;
        player = null;
    }


    private void OnDrawGizmosSelected()
    {
        if (interactionPoint == null)
        {
            interactionPoint = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionPoint.position, interactingRadius);
    }

}

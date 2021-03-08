using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    float rayMovementReach = 100f;

    LayerMask movementMask;
    Camera cam;

    PlayerMovement mov;

    Interactable focus;

    private void Start()
    {
        cam = Camera.main;
        movementMask = LayerMask.GetMask("Ground");

        mov = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButton(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(ray, out hitInfo, rayMovementReach, movementMask))
            {
                //Debug.Log("Hit " + hitInfo.collider.name + " "+ hitInfo.point);

                //stop focusing the target

                if (!ReferenceEquals(focus, null))//verifica se tem alguma coisa no foco, tem que usar o reference equals porque o != estava dando erro, pois quando o objeto é pego e destruido a instancia fica missing
                {
                    RemoveFocus();
                }

                mov.MoveToPoint(hitInfo.point);

            }


        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, rayMovementReach))
            {

                //see if something interactable has been hit
                Interactable interactable = hitInfo.transform.GetComponent<Interactable>();

                //focus on something
                if(interactable != null)
                {
                    SetFocus(interactable);
                    
                }

                

            }


        }
    }

    void SetFocus(Interactable newFocus)
    {

        if(newFocus != focus)
        {

            //Debug.Log("Outro Focus");

            if(!ReferenceEquals(focus, null))
            {
                RemoveFocus();
            }

            focus = newFocus;
            mov.FollowTarget(focus);

        }
        
        focus.ObjectIsOnFocus(transform);


    }

    void RemoveFocus()
    {
        Debug.Log("Removendo Focus");

        focus.RemoveObjectFocus();
        mov.StopFollowingTarget();
        focus = null;
    }

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus;
    public LayerMask movementMask;

    Camera cam;
    PlayerMotor motor;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);
                // Move our player to what we hit
                motor.MoveToPoint(hit.point);
                RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Got to the if statement");
            // We create a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If the ray hits
            if (Physics.Raycast(ray, out hit, 100))
            {
                
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                //Debug.Log(interactable.name);
                if (interactable != null)
                {
                    
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus (Interactable newFocus)
    {
        if(newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        
        newFocus.OnFocused(transform);
        
    }

    void RemoveFocus ()
    {
        if (focus != null)
            focus.OnDefocused();
        
        
        focus = null;
        motor.StopFollowingTarget();
    }
}

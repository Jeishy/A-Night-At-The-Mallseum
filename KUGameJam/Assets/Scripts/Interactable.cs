using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{

    public string interactbutton;

    public float interactDistance = 3f;
    public LayerMask interactlayer;

    public Image interactIcon;

    public bool isInteracting;


    // Start is called before the first frame update
    void Start()
    {
        if (interactIcon != null)
        {
            interactIcon.enabled = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.position);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, interactDistance, interactlayer))
        {

            if (!isInteracting)
            {
                if (interactIcon != null)
                {
                    interactIcon.enabled = true;
                }

                if(Input.GetButtonDown(interactbutton))
                {
                /*    if(hit.collider.CompareTag("Door")
                    {
                        hit.collider.GetComponent<Door>.ChangeDoorState();
                    }
                */}
            }
        }
    }
}

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
            //Debug.DrawRay(GameObject.FindGameObjectWithTag("Player").transform.position, Vector3.Normalize(transform.position - GameObject.FindGameObjectWithTag("Player").transform.position)* 50, Color.green);
            if (isInteracting == false)
            {
                interactIcon.enabled = false;
                if (interactIcon != null)
                {
                    interactIcon.enabled = true;
                }
                


                if (Input.GetButtonDown(interactbutton))
                {
                /*    if(hit.collider.CompareTag("Door")
                    {
                        hit.collider.GetComponent<Door>.ChangeDoorState();
                    }*/
                    if(hit.collider.CompareTag("Note"))
                    {
                        
                        hit.collider.GetComponent<Note>().ShowNoteImage();
                    }


                }
                
            }
        }
    }
}

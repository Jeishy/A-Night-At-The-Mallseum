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

    public DialogueManager dialogueManager;

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, interactDistance, interactlayer))
        {
            Debug.Log("Interacting");
            Debug.DrawRay(transform.position, Vector3.Normalize(hit.point - transform.position)* 100f, Color.green);
            if (isInteracting == false)
            {
                /*interactIcon.enabled = false;
                if (interactIcon != null)
                {
                    interactIcon.enabled = true;
                }*/
                


                if (Input.GetButtonDown(interactbutton))
                {
                /*    if(hit.collider.CompareTag("Door")
                    {
                        hit.collider.GetComponent<Door>.ChangeDoorState();
                    }*/
                    if(hit.collider.CompareTag("Note"))
                    {
                        GameObject noteGo = hit.collider.gameObject;
                        GameManager.Instance.NoteCollect(noteGo);

                        Note note = hit.collider.GetComponent<Note>();
                        DialogueTrigger dialogueTrigger = hit.collider.GetComponent<DialogueTrigger>();

                        note.ShowNoteImage();
                        TriggerDialogue(dialogueTrigger.dialogue);
                    }
                    else if (hit.collider.CompareTag("Battery"))
                    {
                        Battery battery = hit.collider.gameObject.GetComponent<Battery>();
                        battery.BatteryCollect();
                    }


                }
                
            }
        }
    }
    public void TriggerDialogue(Dialogue dialogue)
    {
        dialogueManager.StartDialogue(dialogue);
    }

}

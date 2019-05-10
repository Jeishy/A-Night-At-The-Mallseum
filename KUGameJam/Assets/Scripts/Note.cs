using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{

    public Image Noteimage;

    public AudioClip pickupSound;
    public AudioClip putawaySound;

    // Start is called before the first frame update
    void Start()
    {
        Noteimage.enabled = false;
    }

    public void ShowNoteImage()
    {
        Noteimage.enabled = true;
        GetComponent<AudioSource>().PlayOneShot(pickupSound);
    }

    public void HideNoteImage()
    {
        Noteimage.enabled = false;
        GetComponent<AudioSource>().PlayOneShot(putawaySound);
    }
}

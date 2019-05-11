using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private List<GameObject> CollectedCollectables = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Function called when player dies
    public void Die()
    {
        // Cause player gameobject to rotate to the side and fall to the ground
        // Pause the game
        // Show death canvas
    }

    // Function called when player collects a note
    public void NoteCollect(GameObject collectableGO)
    {
        // Add collectable to collected collectables list
        CollectedCollectables.Add(collectableGO); 
        // Show note canvas
        // Load notes to text box
    }
}

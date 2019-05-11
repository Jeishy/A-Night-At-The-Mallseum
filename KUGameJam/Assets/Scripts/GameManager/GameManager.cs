using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance = null;
    [HideInInspector] public FirstPersonController FirstPersonController;

    private Transform _deathTrans;
    private GameObject _deathPanelGO;
    private List<GameObject> CollectedCollectables = new List<GameObject>();
    private Transform _playerTrans;
    private Transform _originalTrans;

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

    private void Start()
    {
        _deathPanelGO = GameObject.Find("DeathPanel");
        _deathPanelGO.SetActive(false);
        _playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        FirstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        _originalTrans = _playerTrans;
        _deathTrans = GameObject.Find("DeathTransform").transform;
    }

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Die();
        }
    }

    // Function called when player dies
    public void Die()
    {
        // Set mouse look sens to 0
        FirstPersonController.m_MouseLook.XSensitivity = 0f;
        FirstPersonController.m_MouseLook.YSensitivity = 0f;
        // Pause game
        PauseGame();
        // Show death canvas
        _deathPanelGO.SetActive(true);
    }

    public void Win()
    {
        // Open the door
    }

    // Function called when player collects a note
    public void NoteCollect(GameObject collectableGO)
    {
        // Add collectable to collected collectables list
        CollectedCollectables.Add(collectableGO);
        if (CollectedCollectables.Count == 9)
            Win();
    }

    private void PauseGame()
    {
        // Disable fps controller
        FirstPersonController.enabled = false;
        // Lock and show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Pause the game
        Time.timeScale = 0;
    }
}

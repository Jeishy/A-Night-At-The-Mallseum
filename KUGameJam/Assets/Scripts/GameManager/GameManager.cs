using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance = null;
    [HideInInspector] public FirstPersonController FirstPersonController;

    [SerializeField] private GameObject[] _doorPieces = new GameObject[9];

    private Transform _deathTrans;
    private GameObject _deathPanelGO;
    private GameObject[] _notes;
    private Transform _playerTrans;
    private Transform _originalTrans;
    private int _numNotesCollected;

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
        _doorPieces = GameObject.FindGameObjectsWithTag("DoorPiece");
        _notes = GameObject.FindGameObjectsWithTag("Note");
        _numNotesCollected = 0;
        // Disable all door pieces
        foreach (GameObject doorPiece in _doorPieces)
        {
            doorPiece.SetActive(false);
        }
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
        // Interact with door
    }

    // Function called when player collects a note
    public void NoteCollect(GameObject collectableGO)
    {
        _numNotesCollected++;
        // Enable door piece that corresponds to same note piece index
        int collectNoteIndex = Array.IndexOf(_notes, collectableGO);

        _doorPieces[collectNoteIndex].SetActive(true);
        if (_numNotesCollected == 9)
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

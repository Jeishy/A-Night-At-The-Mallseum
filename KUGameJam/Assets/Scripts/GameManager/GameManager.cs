using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance = null;
    [HideInInspector] public bool IsPlayerDead;
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
        IsPlayerDead = false;
        _originalTrans = _playerTrans;
        _deathTrans = GameObject.Find("DeathTransform").transform;
    }

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Die");
            Die();
        }

        if (IsPlayerDead)
        {
            _playerTrans.rotation = Quaternion.Lerp(_originalTrans.rotation, _deathTrans.rotation, Time.time * 0.4f);
        }
    }

    // Function called when player dies
    public void Die()
    {
        // Set mouse look sens to 0
        FirstPersonController.m_MouseLook.XSensitivity = 0f;
        FirstPersonController.m_MouseLook.YSensitivity = 0f;

        IsPlayerDead = true;
        // Pause game
        StartCoroutine(PauseGame());
        // Show death canvas
        _deathPanelGO.SetActive(true);
    }

    // Function called when player collects a note
    public void NoteCollect(GameObject collectableGO)
    {
        // Add collectable to collected collectables list
        CollectedCollectables.Add(collectableGO); 
        // Show note canvas
        // Load notes to text box
    }

    private IEnumerator PauseGame()
    {
        yield return new WaitForSeconds(2f);
        // Pause the game
        Time.timeScale = 0;
    }
}

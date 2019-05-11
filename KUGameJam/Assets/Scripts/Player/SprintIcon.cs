using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class SprintIcon : MonoBehaviour
{
    private Image _sprintIconImage;
    [SerializeField] private FirstPersonController _fpsController;

    // Start is called before the first frame update
    void Start()
    {
        _sprintIconImage = GetComponent<Image>();
        _sprintIconImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log((_fpsController.m_Stamina / _fpsController.m_MaxStamina));

        if (!_fpsController.m_IsWalking)
        {
            // Show sprint icon
            _sprintIconImage.enabled = true;
        }
        else
        {
            _sprintIconImage.enabled = false;
        }
    }
}

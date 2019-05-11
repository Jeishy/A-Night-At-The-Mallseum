using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowPowerIcon : MonoBehaviour
{
    private Image _powerIconImage;
    [SerializeField] private Flashlight _flashlight;

    // Start is called before the first frame update
    void Start()
    {
        _powerIconImage = GetComponent<Image>();
        _powerIconImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_flashlight.CurrentFlashlightMeter <= 0.2f * _flashlight._maxFlashlightMeter)
        {
            _powerIconImage.enabled = true;
            if (_flashlight.CurrentFlashlightMeter <= 0)
                _powerIconImage.enabled = false;
        }
        else
        {
            _powerIconImage.enabled = false;
        }
    }
}

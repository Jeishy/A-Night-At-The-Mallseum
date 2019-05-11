using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [Tooltip("The max flashlight meter amount")]
    public int _maxFlashlightMeter;                                   // The max flashlight meter value

    [Tooltip("The rate at wich the flashlight meter decreases")]
    [SerializeField] private int _lightMeterDepletionRate;                               // The depletion rate of the flashlight

    [Tooltip("The collider for the light from the flashlight")]
    [SerializeField] private Collider _lightCol;                                        // The light's collider (The light coming from the flashlight)

    [SerializeField] private Light _light;                                              // The flashlight's spot light

    private int _currentFlashlightMeter;                                                 // The current flashlight meter value
    public int CurrentFlashlightMeter { get { return _currentFlashlightMeter; } set { _currentFlashlightMeter = value; } } 
 

    public bool IsLightOn;                                                            // Boolean used for checking if the light is turned on
    private float _lightTime;
    private bool _canFlashlightTurnOn;



    // Start is called before the first frame update
    void Start()
    {
        // Set the current flash light meter to the maximum at the beginning of the game
        _currentFlashlightMeter = _maxFlashlightMeter;
        IsLightOn = false;
        _canFlashlightTurnOn = true;
        // Disable light and light collider
        _light.enabled = false;
        _lightCol.enabled = false;
    }

    private void Update()
    {
        // Check if mouse 1 pressed
        if (Input.GetButtonDown("Interact") && _canFlashlightTurnOn && !IsLightOn)
        {
            // Turn on flashlight
            TurnOnFlashlight();
        }
        else if (Input.GetButtonDown("Interact") && _canFlashlightTurnOn && IsLightOn)
        {
            // Turn off flashlight if interact button is pressed
            TurnOffFlashlight();
        }

        if (_currentFlashlightMeter < 10 && _canFlashlightTurnOn)
        {
            TurnOffFlashlight();
            // Set current light meter value to 0
            _currentFlashlightMeter = 0;
            _canFlashlightTurnOn = false;
        }

        // Deplete light meter if flashlight is turned on
        if (IsLightOn)
        {
            LightMeterDepletion();
        }

        // If the light meter is recharged to above the threshold
        if (_currentFlashlightMeter >= 10)
        {
            _canFlashlightTurnOn = true;
        }
    }

    // Function for shining flashlight
    private void TurnOnFlashlight()
    {
        // Enable collider when shining
        IsLightOn = true;
        _lightCol.enabled = true;
        // Enable light
        _light.enabled = true;
    }
    
    private void TurnOffFlashlight()
    {
        // Disable collider
        IsLightOn = false;
        _lightCol.enabled = false;
        // Disable light
        _light.enabled = false;
    }

    private void LightMeterDepletion()
    {
        if (_lightTime <= Time.time)
        {
            // Deplete light meter every second
            _lightTime = Time.time + 1f;
            _currentFlashlightMeter -= _lightMeterDepletionRate;
        }
    }

    public void IncreaseFlashlightMeter(float rechargeAmount)
    {
        int recharge = Mathf.RoundToInt(rechargeAmount);
        _currentFlashlightMeter += recharge;
    }

    public int GetCurrentFlashlightMeter()
    {
        return _currentFlashlightMeter;
    }

    public void SetCurrentFlashLightMeter(int value)
    {
        _currentFlashlightMeter = value;
    }
}

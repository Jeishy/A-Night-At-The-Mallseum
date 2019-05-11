using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    [SerializeField] [Range(0.01f, 1.0f)] private float _rechargePercentage;
    [SerializeField] private Flashlight _flashlight;

    private float _rechargeAmount;
    private float _maxFlashlightMeter;

    // Start is called before the first frame update
    void Start()
    {
        // Set recharge amount to be a percentage of the max flashlight meter
        _rechargeAmount = _flashlight._maxFlashlightMeter * _rechargePercentage;
    }

    public void BatteryCollect()
    {
        if (_flashlight.GetCurrentFlashlightMeter() < _flashlight._maxFlashlightMeter)
        {
            _flashlight.IncreaseFlashlightMeter(_rechargeAmount);
            if (_flashlight.GetCurrentFlashlightMeter() > _flashlight._maxFlashlightMeter)
                _flashlight.SetCurrentFlashLightMeter(50);
            Destroy(gameObject);
        }
    }
}

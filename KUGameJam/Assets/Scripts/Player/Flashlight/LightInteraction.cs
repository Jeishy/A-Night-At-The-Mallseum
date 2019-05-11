using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteraction : MonoBehaviour
{
    private EnemyAI _enemyAI;
    private bool _isInLightConeRange;

    private void Start()
    {
        _enemyAI = GetComponent<EnemyAI>();
        _isInLightConeRange = false;
    }


}

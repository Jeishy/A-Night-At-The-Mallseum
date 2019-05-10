using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script on DDOL GO
// All children GOs are not destroyed on load
public class DDOL : MonoBehaviour
{
    public static DDOL Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOLLoader : MonoBehaviour
{
    [SerializeField] private GameObject _ddolPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        if (DDOL.Instance == null)
            Instantiate(_ddolPrefab);
    }
}

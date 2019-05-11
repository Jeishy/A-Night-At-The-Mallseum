using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour

    

{
    Light light;
    public float minWaitTime;
    public float maxWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine(Flashing());
    }

    IEnumerator Flashing ()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            light.enabled = !light.enabled;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

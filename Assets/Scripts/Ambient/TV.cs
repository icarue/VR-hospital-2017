using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : Ambient
{
    float waitTime;
    void OnEnable()
    {
        waitTime = Random.Range(5, 7);
    }
    public override void StartAmb()
    {
        gameObject.SetActive(true);
        StartCoroutine(waitAndDeactivate());
    }

    IEnumerator waitAndDeactivate()
    {
        yield return new WaitForSeconds(waitTime);
        endAmb();
        gameObject.SetActive(false);
    }
}

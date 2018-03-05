using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCR : Ambient {

    float waitTime;
    TextMesh textMesh;
    string clockText;
    Color clockColor;

    [SerializeField]
    float maxLastingTime;

    bool isOn = true;
    void Awake()
    {
        textMesh = GetComponent<TextMesh>();
        clockColor = textMesh.color;
    }

    void OnEnable()
    {
        waitTime = Random.Range(5, maxLastingTime);
        InvokeRepeating("blink", 0f, 0.5f);
    }

    void blink()
    {
        if (isOn)
        {
            textMesh.color = Color.clear; 
        } else
        {
            textMesh.color = clockColor;
        }
        isOn = !isOn;
    }

    public override void StartAmb()
    {
        this.enabled = true;
        StartCoroutine(waitAndDeactivate());
    }

    IEnumerator waitAndDeactivate()
    {
        yield return new WaitForSeconds(waitTime);
        CancelInvoke("blink");
        endAmb();
        this.enabled = false;
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] values values;
    [Header("Food")]
    [SerializeField] Slider foodslider;
    [Header("Water")]
    [SerializeField] Slider waterslider;
    [Header("Jobs")]
    [SerializeField] GameObject jobsites;
    [SerializeField] Slider jobslider;

    // Update is called once per frame
    void Update()
    {
        jobslider.maxValue = values.Getjobs();
        if (values.Getjobs() != 0)
        {
            jobslider.value = values.Gettakenjobs();
            Debug.Log(values.Gettakenjobs() / values.Getjobs());
        }
    }
}

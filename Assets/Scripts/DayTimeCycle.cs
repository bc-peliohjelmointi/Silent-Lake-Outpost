using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimeCycle : MonoBehaviour
{
    public GameObject Sun;
    public GameObject Moon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DayCycleTrigger"))
        {
            if(Sun.activeSelf)
            {
                Sun.SetActive(false);
                Moon.SetActive(true);
            }
            else
            {
                Moon.SetActive(false);
                Sun.SetActive(true);
            }
        }
    }
}

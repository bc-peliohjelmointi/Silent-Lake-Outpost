using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBarriers : MonoBehaviour
{
    [SerializeField] GameObject generatorBarrier;
    [SerializeField] GameObject propFlaslight;
    [SerializeField] GameObject pickUpFlashlightUI;
    [SerializeField] GameObject turnOnGeneratorUI;


    private void Update()
    {
        IsFlashlightPicked();
    }
    private void OnTriggerEnter(Collider barrier)
    {
        if (barrier.CompareTag("GeneratorBarrier"))
        {
            turnOnGeneratorUI.SetActive(false);
            pickUpFlashlightUI.SetActive(true);
        }
    }

    private void IsFlashlightPicked()
    {
        if (!propFlaslight.activeSelf)
        {
            pickUpFlashlightUI.SetActive(false);
            generatorBarrier.SetActive(false);
        }
    }
}

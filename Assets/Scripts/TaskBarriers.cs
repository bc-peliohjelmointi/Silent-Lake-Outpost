using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBarriers : MonoBehaviour
{
    // variables for trying to leave without flashlight
    [SerializeField] GameObject generatorBarrier;
    [SerializeField] GameObject propFlaslight;
    [SerializeField] GameObject pickUpFlashlightUI;
    [SerializeField] GameObject turnOnGeneratorUI;

    // variables for taking the wrong path 
    [SerializeField] GameObject wrongPathText;

    private void Update()
    {
        IsFlashlightPicked();
    }
    private void OnTriggerEnter(Collider barrier)
    {
        // trigger for trying to go down without a flashlight
        if(turnOnGeneratorUI.activeSelf)
        {
            if (barrier.CompareTag("GeneratorBarrier"))
            {
                turnOnGeneratorUI.SetActive(false);
                pickUpFlashlightUI.SetActive(true);
            }
        }

        if (barrier.CompareTag("WrongPathBarrier"))
        {
            wrongPathText.SetActive(true);
            Invoke("TurnOffPathText", 3f);
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

    private void TurnOffPathText()
    {
        wrongPathText.SetActive(false);
    }
}

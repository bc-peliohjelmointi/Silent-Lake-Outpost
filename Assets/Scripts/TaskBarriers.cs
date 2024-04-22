using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class is made by Leevi. It is used for barriers so that the player cannot leave.
/// </summary>
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

    /// <summary>
    /// Used for activating a guide prompt by colliding with the trigger.
    /// </summary>
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

    /// <summary>
    /// used for disabling the barrier on the tower stairs upon picking up the flashlight.
    /// </summary>
    private void IsFlashlightPicked()
    {
        if (!propFlaslight.activeSelf)
        {
            pickUpFlashlightUI.SetActive(false);
            generatorBarrier.SetActive(false);
        }
    }

    /// <summary>
    /// Used to display a wrong path promp message when walking the wrong way
    /// </summary>
    private void TurnOffPathText()
    {
        wrongPathText.SetActive(false);
    }
}

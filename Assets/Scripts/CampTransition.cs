using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using StarterAssets;


/// <summary>
/// This script is made by Leevi. It handles the transitions between places.
/// </summary>
public class CampTransition : MonoBehaviour
{
    // variables for camp transition
    [SerializeField] GameObject darkeningEffect;
    [SerializeField] Transform targetPosition;
    FirstPersonController fpsController;
    [SerializeField] GameObject playerCameraRoot;
    [SerializeField] GameObject turnOffFireUI;
    [SerializeField] GameObject campTransitionTrigger;

    // variables for back to tower transition
    [SerializeField] Transform towerTargetPosition;
    [SerializeField] GameObject goToSleepUI;
    [SerializeField] GameObject backToTowerUI;
    [SerializeField] GameObject NoLeavingBarrier;
    [SerializeField] GameObject WrongPathTrigger;

    // variables for cabin transition
    [SerializeField] Transform cabinTargetPosition;
    [SerializeField] GameObject hikeToCabinDialogue;
    [SerializeField] GameObject arrivedAtCabinDialogue;

    // variables for leaving cabin to tower transition
    [SerializeField] Transform cabinToTowerPosition;
    [SerializeField] GameObject batteryDialogue;

    [SerializeField] GameObject footSteps;

    private bool transitionToCamp = false;
    private bool transitionToTower = false;
    private bool transitionToCabin = false;
    private bool fromCabinToTower = false;

    public bool canSleep = false;

    private void Start()
    {
        fpsController = GetComponent<FirstPersonController>();
    }


    /// <summary>
    /// Made for transforming the player position to while the black screen fade effect is active and playercontroller is inactive
    /// </summary>
    private void Update()
    {
        if (!fpsController.enabled && darkeningEffect.activeSelf)
        {
            if(transitionToCamp)
            {
                transform.position = targetPosition.position;
                transform.rotation = targetPosition.rotation;
                playerCameraRoot.SetActive(false);
            }
            
            else if(transitionToTower)
            {
                transform.position = towerTargetPosition.position;
                transform.rotation = towerTargetPosition.rotation;
                playerCameraRoot.SetActive(false);
            }

            else if (transitionToCabin)
            {
                transform.position = cabinTargetPosition.position;
                transform.rotation = cabinTargetPosition.rotation;
                playerCameraRoot.SetActive(false);
            }

            else if(fromCabinToTower)
            {
                transform.position = cabinToTowerPosition.position;
                transform.rotation = cabinToTowerPosition.rotation;
                playerCameraRoot.SetActive(false);
            }
        }

        DisableFootsteps();
    }

    /// <summary>
    /// Activating the black screen transition effect when touching the triggers in the scene
    /// </summary>
    private async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ToCampTransition"))
        {
            WrongPathTrigger.SetActive(false);
            transitionToCamp = true;
            darkeningEffect.SetActive(true);
            await Task.Delay(3000);
            fpsController.enabled = false;
            await Task.Delay(4000);
            transitionToCamp = false;
            fpsController.enabled = true;
            playerCameraRoot.SetActive(true);
            darkeningEffect.SetActive(false);
            turnOffFireUI.SetActive(true);
            Invoke("TurnOffUIPrompt", 10f);
            campTransitionTrigger.SetActive(false);
        }

        else if (other.CompareTag("ToTowerTransition"))
        {
            canSleep = true;
            NoLeavingBarrier.SetActive(true);
            transitionToTower = true;
            darkeningEffect.SetActive(true);
            await Task.Delay(3000);
            backToTowerUI.SetActive(false);
            fpsController.enabled = false;
            await Task.Delay(4000);
            transitionToTower = false;
            fpsController.enabled = true;
            playerCameraRoot.SetActive(true);
            darkeningEffect.SetActive(false);
            Invoke("TurnOnSleepUI", 3f);
            Invoke("TurnOffSleepUI", 8f);
        }

        else if (other.CompareTag("ToCabinTransition"))
        {
            transitionToCabin = true;
            darkeningEffect.SetActive(true);
            await Task.Delay(3000);
            hikeToCabinDialogue.SetActive(false);
            fpsController.enabled = false;
            await Task.Delay(4000);
            transitionToCabin = false;
            fpsController.enabled = true;
            playerCameraRoot.SetActive(true);
            darkeningEffect.SetActive(false);
            Invoke("ArrivedAtCabin", 5f);
            Invoke("TurnOffArriveDialogue", 10f);
        }

        else if (other.CompareTag("FromCabinToTowerPosition"))
        {
            batteryDialogue.SetActive(false);
            fromCabinToTower = true;
            darkeningEffect.SetActive(true);
            await Task.Delay(3000);
            fpsController.enabled = false;
            await Task.Delay(4000);
            fromCabinToTower = false;
            fpsController.enabled = true;
            playerCameraRoot.SetActive(true);
            darkeningEffect.SetActive(false);
        }
    }

    /// <summary>
    /// Disabling footsteps whenever the black screen fade effect is active
    /// </summary>
    private void DisableFootsteps()
    {
        if(darkeningEffect.activeSelf)
        {
            footSteps.SetActive(false);
        }

        else
        {
            footSteps.SetActive(true);
        }
    }

    /// <summary>
    /// These methods below are used for Invokes at some point when needed
    /// </summary>
    private void TurnOffUIPrompt()
    {
        turnOffFireUI.SetActive(false);
    }

    private void TurnOnSleepUI()
    {
        goToSleepUI.SetActive(true);
    }
    private void TurnOffSleepUI()
    {
        goToSleepUI.SetActive(false);
    }

    private void ArrivedAtCabin()
    {
        arrivedAtCabinDialogue.SetActive(true);
    }

    private void TurnOffArriveDialogue()
    {
        arrivedAtCabinDialogue.SetActive(false);
    }
}

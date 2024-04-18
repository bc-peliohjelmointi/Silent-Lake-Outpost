using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using StarterAssets;

public class CampTransition : MonoBehaviour
{
    [SerializeField] GameObject darkeningEffect;
    [SerializeField] Transform targetPosition;
    FirstPersonController fpsController;
    [SerializeField] GameObject playerCameraRoot;
    [SerializeField] GameObject turnOffFireUI;
    [SerializeField] GameObject campTransitionTrigger;

    [SerializeField] Transform towerTargetPosition;
    [SerializeField] GameObject goToSleepUI;
    [SerializeField] GameObject backToTowerUI;
    [SerializeField] GameObject NoLeavingBarrier;
    [SerializeField] GameObject WrongPathTrigger;

    [SerializeField] GameObject footSteps;

    private bool transitionToCamp = false;
    private bool transitionToTower = false;

    public bool canSleep = false;

    private void Start()
    {
        fpsController = GetComponent<FirstPersonController>();
    }

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
        }

        DisableFootsteps();
    }

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
    }

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
}

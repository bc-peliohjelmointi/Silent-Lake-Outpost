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

    private void Start()
    {
        fpsController = GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        if (!fpsController.enabled && darkeningEffect.activeSelf)
        {
            transform.position = targetPosition.position;
            transform.rotation = targetPosition.rotation;
            playerCameraRoot.SetActive(false);
        }
    }

    private async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ToCampTransition"))
        {
            darkeningEffect.SetActive(true);
            await Task.Delay(3000);
            fpsController.enabled = false;
            await Task.Delay(4000);
            fpsController.enabled = true;
            playerCameraRoot.SetActive(true);
            darkeningEffect.SetActive(false);
        }
    }
}

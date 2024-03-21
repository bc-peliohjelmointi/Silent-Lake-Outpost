using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShowItem : MonoBehaviour
{
    [SerializeField] GameObject flashLight;
    [SerializeField] Light flashLightSpotLight;
    [SerializeField] GameObject binocs;
    [SerializeField] GameObject propBinocs;
    [SerializeField] GameObject propFlashlight;

    [SerializeField] GameObject flashlightInstructionUI;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource flashLightSource;

    [SerializeField] AudioClip flashLightClip;
    [SerializeField] AudioClip photoClip;

    private bool isLightActive = false;
    private bool toggleFlashLightLight = false;

    Binoculars binocScript;

    private void Start()
    {
        flashLightSpotLight.enabled = false;
        binocScript = GetComponent<Binoculars>();
    }

    private void Update()
    {
        FlashLightOn();
        FlashLightShow();
        BinocularsShow();
    }


    private void DeactivateFlash()
    {
        isLightActive = false;
    }

    void FlashLightOn()
    {
        if (flashLight.activeSelf && Input.GetKeyDown(KeyCode.Mouse0))
        {
            flashLightSpotLight.enabled = true;
            toggleFlashLightLight = !toggleFlashLightLight;
            flashLightSource.PlayOneShot(flashLightClip);
        }

        else if (toggleFlashLightLight)
        {
            flashLightSpotLight.enabled = false;
        }
    }


    void FlashLightShow()
    {
        if (!propFlashlight.activeSelf)
        {
            if (!flashLight.activeSelf && !binocs.activeSelf && !binocScript.isZoomed && Input.GetKeyDown(KeyCode.F))
            {
                flashLight.SetActive(true);
            }

            else if (!flashLight.activeSelf && binocs.activeSelf && Input.GetKeyDown(KeyCode.F))
            {
                binocs.SetActive(false);
                flashLight.SetActive(true);
            }

            else if (flashLight.activeSelf && Input.GetKeyDown(KeyCode.F))
            {
                flashLight.SetActive(false);
                flashlightInstructionUI.SetActive(false);
            }
        }
    }

    void BinocularsShow()
    {
        if (!propBinocs.activeSelf)
        {
            if (!binocs.activeSelf && !flashLight.activeSelf && !binocScript.isZoomed && Input.GetKeyDown(KeyCode.B))
            {
                binocs.SetActive(true);
            }

            else if (!binocs.activeSelf && flashLight.activeSelf && Input.GetKeyDown(KeyCode.B))
            {
                flashLight.SetActive(false);
                binocs.SetActive(true);
            }

            else if (binocs.activeSelf && Input.GetKeyDown(KeyCode.B))
            {
                binocs.SetActive(false);
            }
        }
    }
}

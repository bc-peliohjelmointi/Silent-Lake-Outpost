using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShowItem : MonoBehaviour
{
    [SerializeField] GameObject propBinocs;
    [SerializeField] GameObject propFlashlight;

    [SerializeField] GameObject flashLight;
    [SerializeField] Light flashLightSpotLight;
    [SerializeField] GameObject binocs;


    [SerializeField] AudioSource flashLightSource;
    [SerializeField] AudioClip flashLightClip;

    [SerializeField] GameObject flashlightIcon;
    [SerializeField] GameObject binocularsIcon;

    [SerializeField] GameObject note;
    [SerializeField] GameObject darkening;



    Binoculars binocScript;

    private void Start()
    {
        binocScript = GetComponent<Binoculars>();
    }

    private void Update()
    {
        if(!darkening.activeSelf && !note.activeSelf)
        {
            FlashLightOn();
            FlashLightShow();
            BinocularsShow();
        }

        else if(darkening.activeSelf || note.activeSelf)
        {
            if (note.activeSelf)
            {
                flashLight.SetActive(false);
            }

            binocs.SetActive(false);
        }
    }


    void FlashLightOn()
    {
        if (flashLight.activeSelf && !flashLightSpotLight.enabled && Input.GetKeyDown(KeyCode.Mouse0))
        {
            flashLightSpotLight.enabled = true;
            flashLightSource.PlayOneShot(flashLightClip);

        }

        else if (flashLightSpotLight.enabled && Input.GetKeyDown(KeyCode.Mouse0))
        {
            flashLightSpotLight.enabled = false;
        }



        else if (binocs.activeSelf || !flashLight.activeSelf)
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
            }
        }

        if (flashLight.activeSelf)
        {
            flashlightIcon.SetActive(true);
        }

        else
        {
            flashlightIcon.SetActive(false);
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

        if (binocs.activeSelf)
        {
            binocularsIcon.SetActive(true);
        }

        else
        {
            binocularsIcon.SetActive(false);
        }
    }

}

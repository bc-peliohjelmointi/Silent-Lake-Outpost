using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// This class is made by Leevi. It's purpose if to 
/// </summary>
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


    /// <summary>
    /// Calling the different methods whenever needed.
    /// </summary>
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


    /// <summary>
    /// used to handle the flashlights lightsource and soundeffect whenever player turns on the flashlight
    /// </summary>
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


    /// <summary>
    /// Used for bringing up the flashlight and hiding it by pressing the key "F"
    /// </summary>
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


    /// <summary>
    /// Used for bringing up the binoculars and hiding the by the key "B"
    /// </summary>
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

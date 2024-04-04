using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] LayerMask mask1;
    [SerializeField] LayerMask mask2;

    Camera cam;

    [SerializeField] GameObject propBinocs;
    [SerializeField] GameObject propFlashlight;
    [SerializeField] GameObject pickupFlashUI;
    [SerializeField] GameObject pickupBinocsUI;
    [SerializeField] GameObject binocInstructionUI;
    [SerializeField] GameObject flashlightInstructionUI;
    [SerializeField] GameObject realFlashlight;
    [SerializeField] GameObject realBinocs;
    [SerializeField] Light binocLight;

    Tasks taskScript;

    private void Start()
    {
        cam = Camera.main;
        taskScript = GetComponent<Tasks>();
    }

    private void Update()
    {
        if(taskScript.canPickUpBinocs)
        {
            InteractWithObjects(mask1, cam, pickupBinocsUI, propBinocs);
        }
        InteractWithObjects(mask2, cam, pickupFlashUI, propFlashlight);
    }

    private void InteractWithObjects(LayerMask mask, Camera cam, GameObject UI, GameObject item)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2, mask))
        {
            if (hit.collider.gameObject == item)
            {
                UI.SetActive(true);
            }
        }

        else
        {
            UI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out hit, 2, mask))
            {
                if (hit.collider.gameObject == item)
                {
                    item.SetActive(false);

                    if (item.name == "PropBinocs")
                    {
                        binocInstructionUI.SetActive(true);
                        realBinocs.SetActive(true);
                        realFlashlight.SetActive(false);
                        flashlightInstructionUI.SetActive(false);
                        Invoke("TurnOffBinocInstructions", 8f);
                        binocLight.enabled = false;
                    }

                    else if (item.name == "PropFlashlight")
                    {
                        flashlightInstructionUI.SetActive(true);
                        realFlashlight.SetActive(true);
                        realBinocs.SetActive(false);
                        binocInstructionUI.SetActive(false);
                        Invoke("TurnOffFlashlightInstructions", 8f);
                    }
                }
            }
        }
    }

    private void TurnOffFlashlightInstructions()
    {
        flashlightInstructionUI.SetActive(false);
    }

    private void TurnOffBinocInstructions()
    {
        binocInstructionUI.SetActive(false);
    }
}
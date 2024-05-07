using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is made by Leevi. It's purpose is to handle item pickups. Example: Picking up flashlight and binoculars
/// </summary>
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

    [SerializeField] GameObject scoutingBarrier;

    // variables for picking up note
    [SerializeField] LayerMask noteLayer;
    [SerializeField] GameObject note;
    [SerializeField] GameObject noteUI;
    [SerializeField] GameObject noteImageUI;
    private bool holdingNote = false;

    // variables for battery pickup 
    [SerializeField] LayerMask batteryLayer;
    [SerializeField] GameObject batteryUI;
    [SerializeField] GameObject battery1;
    [SerializeField] GameObject battery2;
    public bool jumpscareReady = false;
    [SerializeField] GameObject backToTowerDialogue;
    [SerializeField] GameObject towerTransitionTrigger;


    Tasks taskScript;
    FirstPersonController fpsController;

    private void Start()
    {
        cam = Camera.main;
        taskScript = GetComponent<Tasks>();
        fpsController = GetComponent<FirstPersonController>();
    }

    /// <summary>
    /// Calling different methods
    /// </summary>
    private void Update()
    {
        if(taskScript.canPickUpBinocs)
        {
            InteractWithObjects(mask1, cam, pickupBinocsUI, propBinocs);
        }
        InteractWithObjects(mask2, cam, pickupFlashUI, propFlashlight);

        PickupNote(noteLayer, cam, noteUI, note, noteImageUI);

        PickupBatteries(batteryLayer, cam, batteryUI, battery1);
        PickupBatteries(batteryLayer, cam, batteryUI, battery2);

        if(!battery1.activeSelf && !battery2.activeSelf)
        {
            jumpscareReady = true;
            Invoke("BackToTowerDialogue", 1f);
            towerTransitionTrigger.SetActive(true);
        }
    }

    /// <summary>
    /// Used for picking up the 2 important items (Flashlight and binoculars);
    /// </summary>
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
                        scoutingBarrier.SetActive(true);
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

    /// <summary>
    /// Picking up the scary note left by the cannibal
    /// </summary>
    private void PickupNote (LayerMask mask, Camera cam, GameObject UI, GameObject item, GameObject pictureUI)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2, mask))
        {
            if (hit.collider.gameObject == item && !holdingNote)
            {
                UI.SetActive(true);
            }
        }

        else
        {
            UI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Physics.Raycast(ray, out hit, 2, mask))
            {
                if (hit.collider.gameObject == item)
                {
                    holdingNote = true;
                    UI.SetActive(false);
                    pictureUI.SetActive(true);
                    fpsController.enabled = false;
                }
            }
        }

        if (pictureUI.activeSelf && Input.GetKeyDown(KeyCode.C))
        {
            item.SetActive(false);
            pictureUI.SetActive(false);
            fpsController.enabled = true;
        }
    }

    private void PickupBatteries(LayerMask mask, Camera cam, GameObject UI, GameObject item)
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
                }
            }
        }
    }

    private void BackToTowerDialogue()
    {
        backToTowerDialogue.SetActive(true);
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
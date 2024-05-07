using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This script is made my Leevi. It is used for spotting unusual stuff such as human meat around the campfire.
/// </summary>
public class UnusualEvents : MonoBehaviour
{
    Camera cam;
    [SerializeField] GameObject turnOffFireUI;
    [SerializeField] GameObject questioningUI;
    [SerializeField] GameObject backToTowerUI;

    // variables for spotting meat in the camp area
    [SerializeField] GameObject spottedMeatUI;
    [SerializeField] LayerMask meatMask;
    [SerializeField] GameObject meat;

    // variables for dropping dead body jumpscare
    [SerializeField] GameObject deadbodyTrigger;
    [SerializeField] GameObject deadbody;
    [SerializeField] GameObject cabinBarrier;
    [SerializeField] AudioSource jumpscareSound;

    [SerializeField] GameObject backToTowerDialogue;

    private bool hasSeenMeat = false;

    ItemPickup itempickupScript;
    private void Start()
    {
        cam = Camera.main;
        itempickupScript = GetComponent<ItemPickup>();
    }
    private void Update()
    {
        if(!hasSeenMeat)
        {
            SpottingMeat();
        }

        if (itempickupScript.jumpscareReady)
        {
            deadbodyTrigger.SetActive(true);
        }
    }

    /// <summary>
    /// Used for when the player spots the human meat near the campfire
    /// </summary>
    public void SpottingMeat()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2, meatMask))
        {
            if (hit.collider.gameObject == meat)
            {
                turnOffFireUI.SetActive(false);
                questioningUI.SetActive(false);
                backToTowerUI.SetActive(false);
                spottedMeatUI.SetActive(true);
                Invoke("TurnOffSpotMeatUI", 3f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeadbodyTrigger"))
        {
            cabinBarrier.SetActive(true);
            deadbody.SetActive(true);
            backToTowerDialogue.SetActive(false);
            Invoke("JumpscareSound", 0.8f);
            Invoke("DisableCabinBarrier", 3f);
        }
    }

    private void TurnOffSpotMeatUI()
    {
        spottedMeatUI.SetActive(false);
        hasSeenMeat = true;
    }

    private void DisableCabinBarrier()
    {
        cabinBarrier.SetActive(false);
    }

    private void JumpscareSound()
    {
        jumpscareSound.enabled = true;
    }
}

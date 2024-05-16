using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    [SerializeField] GameObject seeingBodyUI;

    // variables for spotting meat in the camp area
    [SerializeField] GameObject spottedMeatUI;
    [SerializeField] LayerMask meatMask;
    [SerializeField] GameObject meat;

    // variables for dropping dead body jumpscare
    [SerializeField] GameObject deadbodyTrigger;
    [SerializeField] GameObject deadbody;
    [SerializeField] GameObject CabinTrigger;
    [SerializeField] Transform deadbodyTransform;
    [SerializeField] GameObject cabinBarrier;
    [SerializeField] AudioSource jumpscareSound;
    [SerializeField] AudioSource endBuildUp;



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
            CabinTrigger.SetActive(false);
            deadbodyTrigger.SetActive(false);
            cabinBarrier.SetActive(true);
            deadbody.SetActive(true);
            backToTowerDialogue.SetActive(false);
            Invoke("JumpscareSound", 0.8f);
            Invoke("CameraLock", 0.5f);
            Invoke("DisableCabinBarrier", 3f);
        }
    }

    private async void CameraLock()
    {
        Vector3 rot = Quaternion.LookRotation(deadbodyTransform.position - transform.position).eulerAngles;
        rot.x = rot.z = 0;
        transform.rotation = Quaternion.Euler(rot);
    }
    private void TurnOffSpotMeatUI()
    {
        spottedMeatUI.SetActive(false);
        hasSeenMeat = true;
    }

    private async void DisableCabinBarrier()
    {
        cabinBarrier.SetActive(false);
        seeingBodyUI.SetActive(true);
        await Task.Delay(7000);
        seeingBodyUI.SetActive(false);
    }

    private void JumpscareSound()
    {
        jumpscareSound.enabled = true;
        endBuildUp.enabled = true;
    }
}

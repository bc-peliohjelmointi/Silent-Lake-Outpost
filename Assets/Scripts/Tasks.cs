using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using StarterAssets;
using System.Security.Cryptography;
using UnityEngine.AI;

/// <summary>
/// This class is made by Leevi. It is usef for tasks that the player will have to do in order to advance in the story. Such as spotting the campfire for example.
/// Anton put the running sound of the cannibal and generator sound variables in this script
/// </summary>
public class Tasks : MonoBehaviour
{

    public NavMeshAgent agent;

    // variables for generator mission
    [SerializeField] GameObject generator;
    [SerializeField] GameObject selfDialogueUI;
    [SerializeField] GameObject ceilingLamp;
    [SerializeField] GameObject deskLamp;
    [SerializeField] GameObject generatorUI;
    [SerializeField] LayerMask maskGenerator;

    [SerializeField] AudioSource GeneratorStart;
    [SerializeField] AudioSource GeneratorLoop;

    [SerializeField] GameObject lookoutDialogueUI;
    [SerializeField] GameObject[] areaBarriers;

    // variables for scouting out the campfire
    [SerializeField] GameObject binocInstructions;
    [SerializeField] GameObject spotFire;
    [SerializeField] GameObject camp;
    [SerializeField] LayerMask maskCamp;
    [SerializeField] GameObject scoutingBarrier;

    [SerializeField] Light binocLight;

    // variables for sleeping task
    [SerializeField] GameObject bed;
    [SerializeField] LayerMask maskBed;
    [SerializeField] GameObject goToSleepUI;
    [SerializeField] GameObject flashlight;
    [SerializeField] GameObject binoculars;
    [SerializeField] GameObject darkeningEffect;
    [SerializeField] GameObject playerCameraRoot;
    [SerializeField] GameObject knockingDialogue;
    [SerializeField] GameObject closeDoorText;
    [SerializeField] GameObject runningSound;

    [SerializeField] AudioSource doorAudioSource;
    [SerializeField] AudioSource windSound;
    [SerializeField] AudioSource spookyMusic;

    // variables for the arm jumpscare after opening door
    [SerializeField] GameObject arm;
    [SerializeField] Transform armTransform;
    [SerializeField] Transform enemy;
    [SerializeField] LayerMask armLayer;
    [SerializeField] AudioSource armJumpscare;
    private bool hasSeenArm = false;
    [SerializeField] GameObject seenArmUI;
    [SerializeField] GameObject paper;
    [SerializeField] GameObject paperPin;

    //variables for radio contact task
    [SerializeField] GameObject radio;
    [SerializeField] GameObject radioDialogue;
    [SerializeField] LayerMask radioLayer;
    [SerializeField] AudioSource radioNoise;
    [SerializeField] GameObject radioNotWorkingDialogue;
    [SerializeField] GameObject useRadioUI;
    [SerializeField] GameObject hikeToCabinUI;
    [SerializeField] GameObject PlayerCamera;
    private bool canUseRadio = false;

    Camera cam;

    [SerializeField] GameObject cabinTransitionTriggers;

    private bool isTurnedOn = false;
    public bool canPickUpBinocs = false;
    public bool hasSeenCamp = false;
    private bool hasSlept = false;
    private bool isInside = false;

    // reference to other script for uses of their variables
    CampTransition campTransitionScript;
    FirstPersonController fpsController;
    DoorInteractable doorScript;


    private void Start()
    {
        doorScript = GetComponent<DoorInteractable>();
        campTransitionScript = GetComponent<CampTransition>();
        fpsController = GetComponent<FirstPersonController>();
        cam = Camera.main;
        Invoke("TurnOffLights", 6f);
        Invoke("DelayFirstDialogue", 7f);
    }

    private void Update()
    {
        if(!isTurnedOn)
        {
            TurnOnGenerator(maskGenerator, cam, generatorUI, generator);
        }

        if (campTransitionScript.canSleep && !hasSlept && isInside)
        {
            Sleeping(maskBed, cam, goToSleepUI, bed);
        }

        if (hasSlept && !hasSeenArm)
        {
            FallingHandJumpscare();
            LookAtArm(armLayer, cam, seenArmUI, arm);
        }

        if (canUseRadio)
        {
            RadioContact(radioLayer, cam, useRadioUI, radio);
        }
    }

    // raycasting method used for scouting out the campfire with binoculars
    public void SpotFire()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000, maskCamp))
        {
            if (hit.collider.gameObject == camp)
            {
                foreach (GameObject barrier in areaBarriers)
                {
                    barrier.SetActive(false);
                }
                scoutingBarrier.SetActive(false);
                lookoutDialogueUI.SetActive(false);
                spotFire.SetActive(true);
                hasSeenCamp = true;
                binocInstructions.SetActive(false);
                Invoke("DisableSpotFireText", 7f);
            }
        }
    }

    // This method is used when the player is turning on the generator downstairs
    private void TurnOnGenerator(LayerMask mask, Camera cam, GameObject UI, GameObject item)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2, mask))
        {
            if (hit.collider.gameObject == item && !isTurnedOn)
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
                    ceilingLamp.SetActive(true);
                    deskLamp.SetActive(true);
                    selfDialogueUI.SetActive(false);
                    isTurnedOn = true;
                    generatorUI.SetActive(false);
                    canPickUpBinocs = true;
                    Invoke("LookoutTaskDialogue", 3f);
                    canPickUpBinocs = true;
                    binocLight.enabled = true;
                    GeneratorStart.enabled = true;
                    Invoke("TurnOnLoop", 5f);
                }
            }
        }
    }


    // Method for sleeping used later on in the game
    private async void Sleeping(LayerMask mask, Camera cam, GameObject UI, GameObject item)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2, mask))
        {
            if (!hasSlept)
            {
                if (hit.collider.gameObject == item)
                {
                    UI.SetActive(true);
                }
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
                    if (!doorScript.DoorOpen.activeSelf)
                    {
                        scoutingBarrier.SetActive(true);
                        closeDoorText.SetActive(false);
                        hasSlept = true;
                        fpsController.enabled = false;
                        goToSleepUI.SetActive(false);
                        flashlight.SetActive(false);
                        darkeningEffect.SetActive(true);
                        await Task.Delay(3000);
                        windSound.enabled = false;
                        doorAudioSource.enabled = true;
                        await Task.Delay(2000);
                        runningSound.gameObject.SetActive(true);
                        await Task.Delay(2000);
                        spookyMusic.enabled = true;
                        knockingDialogue.SetActive(true);
                        Invoke("TurnOffKnockingDialogue", 3f);
                        fpsController.enabled = true;
                        playerCameraRoot.SetActive(true);
                        darkeningEffect.SetActive(false);
                        runningSound.gameObject.SetActive(false);
                    }

                    else
                    {
                        closeDoorText.SetActive(true);
                        Invoke("DisableCloseDoorText", 2f);
                    }
                }
            }
        }
    }


    // Used for interacting with the radio
    private void RadioContact(LayerMask mask, Camera cam, GameObject UI, GameObject item)
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
                    cabinTransitionTriggers.SetActive(true);
                    seenArmUI.SetActive(false);
                    scoutingBarrier.SetActive(false);
                    UI.SetActive(false);
                    radioNoise.enabled = true;
                    Invoke("TurnOnRadioNotWorking", 1f);
                    radioDialogue.SetActive(false);
                    Invoke("TurnOffRadioNotWorkingUI", 8f);
                    Invoke("HikeToCampDialogue", 10f);
                    canUseRadio = false;
                }
            }
        }
    }

    // Used for the jumpscare hanging arm left by the cannibal enemy infront of the tower door
    private void LookAtArm(LayerMask mask, Camera cam, GameObject UI, GameObject item)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2, mask))
        {
            if (hit.collider.gameObject == item)
            {
                hasSeenArm = true;
                UI.SetActive(true);
                Invoke("TurnOffArmUI", 5f);
                Invoke("RadioContactUI", 5.5f);
                canUseRadio = true;
            }
        }
    }

    // Jumpscare after waking up from sleep and opening the door
    private async void FallingHandJumpscare()
    {
        if(doorScript.isDoorOpening)
        {
            paper.SetActive(true);
            paperPin.SetActive(true);
            arm.SetActive(true);
            Invoke("ArmJumpscareSound", 0.5f);
            fpsController.enabled = false;
            Vector3 rot = Quaternion.LookRotation(armTransform.position - transform.position).eulerAngles;
            rot.x = rot.z = 0;
            transform.rotation = Quaternion.Euler(rot);
            await Task.Delay(1000);
            fpsController.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("InsideTrigger"))
        {
            isInside = true;
        }

        else
        {
            isInside = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            fpsController.enabled = false;
            Vector3 rot = Quaternion.LookRotation(enemy.position - transform.position).eulerAngles;
            rot.x = rot.z = 0;
            transform.rotation = Quaternion.Euler(rot);
        }
    }


    // These methods below are used for Invoking them at some point in need. 
    private void ArmJumpscareSound()
    {
        armJumpscare.enabled = true;
    }

    private void TurnOffArmUI()
    {
        seenArmUI.SetActive(false);
    }

    private void TurnOnRadioNotWorking()
    {
        radioNotWorkingDialogue.SetActive(true);
    }
    private void TurnOffRadioNotWorkingUI()
    {
        radioNotWorkingDialogue.SetActive(false);
    }

    private void RadioContactUI()
    {
        if(canUseRadio)
        {
            radioDialogue.SetActive(true);
        }
    }

    private void TurnOffKnockingDialogue()
    {
        knockingDialogue.SetActive(false);
    }

    private void TurnOnLoop()
    {
        GeneratorLoop.enabled = true;
        GeneratorStart.enabled = false;
    }

    private void DisableCloseDoorText()
    {
        closeDoorText.SetActive(false);
    }

    private void LookoutTaskDialogue()
    {
        lookoutDialogueUI.SetActive(true);
    }

    private void HikeToCampDialogue()
    {
        hikeToCabinUI.SetActive(true);
    }

    private void DelayFirstDialogue()
    {
        selfDialogueUI.SetActive(true);
    }

    private void DisableSpotFireText()
    {
        spotFire.SetActive(false);
    }

    private void TurnOffLights()
    {
        ceilingLamp.SetActive(false);
        deskLamp.SetActive(false);
    }
}
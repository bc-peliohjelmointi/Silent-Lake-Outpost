using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using System.Threading.Tasks;
using StarterAssets;
using System.Security.Cryptography;


public class Tasks : MonoBehaviour
{
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

    [SerializeField] AudioSource doorAudioSource;
    [SerializeField] AudioSource windSound;
    [SerializeField] AudioSource spookyMusic;

    // variables for the falling arm jumpscare after opening door
    [SerializeField] GameObject arm;
    [SerializeField] LayerMask armLayer;
    [SerializeField] AudioSource armJumpscare;
    private bool hasSeenArm = false;
    [SerializeField] GameObject seenArmUI;

    //variables for radio contact task
    [SerializeField] GameObject radio;
    [SerializeField] GameObject radioDialogue;
    [SerializeField] LayerMask radioLayer;
    [SerializeField] AudioSource radioNoise;
    [SerializeField] GameObject radioNotWorkingDialogue;
    [SerializeField] GameObject useRadioUI;
    private bool canUseRadio = false;

    Camera cam;

    private bool isTurnedOn = false;
    public bool canPickUpBinocs = false;
    public bool hasSeenCamp = false;
    private bool hasSlept = false;

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

        if (campTransitionScript.canSleep && !hasSlept)
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

    private void DelayFirstDialogue()
    {
        selfDialogueUI.SetActive(true);
    }

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
                        closeDoorText.SetActive(false);
                        hasSlept = true;
                        fpsController.enabled = false;
                        goToSleepUI.SetActive(false);
                        flashlight.SetActive(false);
                        binoculars.SetActive(false);
                        darkeningEffect.SetActive(true);
                        await Task.Delay(3000);
                        windSound.enabled = false;
                        doorAudioSource.enabled = true;
                        await Task.Delay(4000);
                        spookyMusic.enabled = true;
                        knockingDialogue.SetActive(true);
                        Invoke("TurnOffKnockingDialogue", 3f);
                        fpsController.enabled = true;
                        playerCameraRoot.SetActive(true);
                        darkeningEffect.SetActive(false);
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
                    radioNoise.enabled = true;
                    Invoke("TurnOnRadioNotWorking", 1f);
                    radioDialogue.SetActive(false);
                    Invoke("TurnOffRadioNotWorkingUI", 5f);
                    canUseRadio = false;
                }
            }
        }
    }

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
                Invoke("TurnOffArmUI", 4f);
                Invoke("RadioContactUI", 4.1f);
                Invoke("TurnOffContactUI", 6f);
                canUseRadio = true;
            }
        }
    }

    private void FallingHandJumpscare()
    {
        if(doorScript.isDoorOpening)
        {
            arm.SetActive(true);
            Invoke("ArmJumpscareSound", 0.5f);
        }
    }

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
        radioDialogue.SetActive(true);
    }

    private void TurnOffContactUI()
    {
        radioDialogue.SetActive(false);
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
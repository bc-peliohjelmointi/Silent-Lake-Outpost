using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using System.Threading.Tasks;
using StarterAssets;


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

    [SerializeField] AudioSource doorAudioSource;


    Camera cam;

    private bool isTurnedOn = false;
    public bool canPickUpBinocs = false;
    public bool hasSeenCamp = false;
    private bool hasSlept = false;

    CampTransition campTransitionScript;
    FirstPersonController fpsController;


    private void Start()
    {
        campTransitionScript = GetComponent<CampTransition>();
        fpsController = GetComponent<FirstPersonController>();
        cam = Camera.main;
        Invoke("TurnOffLights", 13f);
        Invoke("DelayFirstDialogue", 14f);
    }

    private void Update()
    {
        TurnOnGenerator(maskGenerator, cam, generatorUI, generator);

        if (campTransitionScript.canSleep && !hasSlept)
        {
            Sleeping(maskBed, cam, goToSleepUI, bed);
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
                    fpsController.enabled = false;
                    goToSleepUI.SetActive(false);
                    flashlight.SetActive(false);
                    binoculars.SetActive(false);
                    darkeningEffect.SetActive(true);
                    await Task.Delay(3000);
                    doorAudioSource.enabled = true;
                    await Task.Delay(4000);
                    knockingDialogue.SetActive(true);
                    fpsController.enabled = true;
                    playerCameraRoot.SetActive(true);
                    darkeningEffect.SetActive(false);
                    hasSlept = true;
                }
            }
        }
    }

    private void TurnOnLoop()
    {
        GeneratorLoop.enabled = true;
        GeneratorStart.enabled = false;
        
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
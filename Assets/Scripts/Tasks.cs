using UnityEngine;
using UnityEngine.UI;

public class Tasks : MonoBehaviour
{
    // variables for generator mission
    [SerializeField] GameObject generator;
    [SerializeField] GameObject selfDialogueUI;
    [SerializeField] GameObject ceilingLamp;
    [SerializeField] GameObject deskLamp;
    [SerializeField] GameObject generatorUI;
    [SerializeField] LayerMask maskGenerator;

    [SerializeField] GameObject lookoutDialogueUI;
    [SerializeField] GameObject[] areaBarriers;

    // variables for scouting out the campfire
    [SerializeField] GameObject spotFire;
    [SerializeField] GameObject camp;
    [SerializeField] LayerMask maskCamp;

    [SerializeField] Light binocLight;

    Camera cam;

    private bool isTurnedOn = false;
    public bool canPickUpBinocs = false;

    private void Start()
    {
        cam = Camera.main;
        Invoke("TurnOffLights", 13f);
        Invoke("DelayFirstDialogue", 14f);
    }

    private void Update()
    {
        TurnOnGenerator(maskGenerator, cam, generatorUI, generator);
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
                lookoutDialogueUI.SetActive(false);
                spotFire.SetActive(true);
                Invoke("DisableSpotFireText", 25f);
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
                    Invoke("LookoutTaskDialogue", 5f);
                    canPickUpBinocs = true;
                    binocLight.enabled = true;
                    // code here to turn on the generator sound 
                }
            }
        }
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
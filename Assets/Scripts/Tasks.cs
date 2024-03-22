using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasks : MonoBehaviour
{
    // variables for generator mission
    [SerializeField] GameObject generator;
    [SerializeField] GameObject selfDialogueUI;
    [SerializeField] GameObject ceilingLamp;
    [SerializeField] GameObject deskLamp;
    [SerializeField] GameObject generatorUI;
    [SerializeField] LayerMask maskGenerator;

    // variables for scouting out the campfire
    //[SerializeField] GameObject taskBarrier;
    //[SerializeField] GameObject camp;
    //[SerializeField] LayerMask mask;
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        TurnOnGenerator(maskGenerator, cam, generatorUI, generator);
    }
    /*
    public void SpotFire()
    {
        if (taskBarrier.activeSelf)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000, mask))
            {
                if (hit.collider.gameObject == camp)
                {
                    //taskBarrier.SetActive(false);
                    //taskList.SetActive(false);
                }
            }
        }
    }
    */

    private void TurnOnGenerator(LayerMask mask, Camera cam, GameObject UI, GameObject item)
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
                    ceilingLamp.SetActive(true);
                    deskLamp.SetActive(true);
                    selfDialogueUI.SetActive(false);
                    // code here to turn on the generator sound 
                }
            }
        }
    }
}
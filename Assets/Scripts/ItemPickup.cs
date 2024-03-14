using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    [SerializeField] GameObject pickUpUI;
    [SerializeField] GameObject item;

    Camera cam;

    [SerializeField] GameObject binocs;
    [SerializeField] GameObject flashlight;
    [SerializeField] GameObject flashlightUI;
    [SerializeField] GameObject binocsUI;
    [SerializeField] LayerMask mask2;
    [SerializeField] LayerMask mask3;

    [SerializeField] GameObject binocInstructionUI;
    [SerializeField] GameObject flashlightInstructionUI;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        InteractWithObjects(mask, cam, pickUpUI, item);
        InteractWithObjects(mask2, cam, binocsUI, binocs);
        InteractWithObjects(mask3, cam, flashlightUI, flashlight);
    }

    private void InteractWithObjects(LayerMask mask, Camera cam, GameObject UI, GameObject item)
    {

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 4, mask))
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
            if (Physics.Raycast(ray, out hit, 4, mask))
            {
                if (hit.collider.gameObject == item)
                {
                    item.SetActive(false);
                    if (item.name == "Binoculars")
                    {
                        binocInstructionUI.SetActive(true);
                    }

                    else if (item.name == "Flashlight")
                    {
                        flashlightInstructionUI.SetActive(true);
                    }
                }
            }
        }
    }
}
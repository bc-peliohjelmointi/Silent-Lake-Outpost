using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnusualEvents : MonoBehaviour
{
    Camera cam;
    [SerializeField] GameObject turnOffFireUI;
    [SerializeField] GameObject questioningUI;

    // variables for spotting meat in the camp area
    [SerializeField] GameObject spottedMeatUI;
    [SerializeField] LayerMask meatMask;
    [SerializeField] GameObject meat;

    private bool hasSeenMeat = false;


    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        if(!hasSeenMeat)
        {
            SpottingMeat();
        }
    }

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
                spottedMeatUI.SetActive(true);
                Invoke("TurnOffSpotMeatUI", 3f);
            }
        }
    }

    private void TurnOffSpotMeatUI()
    {
        spottedMeatUI.SetActive(false);
        hasSeenMeat = true;
    }
}

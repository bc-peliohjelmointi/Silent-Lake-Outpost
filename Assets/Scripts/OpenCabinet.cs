using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCabinet : MonoBehaviour
{
    [SerializeField] GameObject Cabinet;
    [SerializeField] GameObject cabinetUI;
    [SerializeField] LayerMask maskCabinet;

    public GameObject CabinetOpen;

    Camera cam;
    public bool isDoorOpening = false;
    bool isDoorMoving = false;
    float smoothTime = 4f;

    Vector3 targetPosition;

    [SerializeField] CapsuleCollider battery1Collider;
    [SerializeField] CapsuleCollider battery2Collider;



    private void Start()
    {
        cam = Camera.main;
        targetPosition = Cabinet.transform.position;
    }

    private void Update()
    {
        if (!isDoorMoving)
        {
            OpenDoor(maskCabinet, cam, cabinetUI, Cabinet);
        }
        if (isDoorOpening)
        {
            Cabinet.transform.position = Vector3.Lerp(Cabinet.transform.position, targetPosition, smoothTime * Time.deltaTime);
            if (Vector3.Distance(Cabinet.transform.position, targetPosition) < 0.1f)
            {
                Cabinet.transform.position = targetPosition;
                isDoorOpening = false;
                isDoorMoving = false;
            }
        }
    }

    private void OpenDoor(LayerMask mask, Camera cam, GameObject UI, GameObject item)
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

        if (Input.GetKeyDown(KeyCode.E) && !isDoorMoving)
        {
            if (Physics.Raycast(ray, out hit, 2, mask))
            {
                if (CabinetOpen.activeSelf)
                {
                    targetPosition = Cabinet.transform.position + new Vector3(-0.5f, 0, 0);
                    CabinetOpen.SetActive(false);
                    battery1Collider.enabled = false;
                    battery2Collider.enabled = false;
                }
                else
                {
                    targetPosition = Cabinet.transform.position + new Vector3(0.5f, 0, 0);
                    battery1Collider.enabled = true;
                    battery2Collider.enabled = true;
                    CabinetOpen.SetActive(true);
                }
                isDoorOpening = true;
                isDoorMoving = true;
            }
        }
    }


}

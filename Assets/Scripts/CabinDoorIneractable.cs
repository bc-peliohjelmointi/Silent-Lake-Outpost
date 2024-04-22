using UnityEngine;

//Script made by Anton.

public class CabinDoorInteractable : MonoBehaviour
{
    [SerializeField] GameObject CabinDoor;
    [SerializeField] GameObject doorUI;
    [SerializeField] LayerMask maskCabinDoor;

    public GameObject CabinDoorOpen;

    Camera cam;
    bool isDoorOpening = false;
    bool isDoorMoving = false;
    float smoothTime = 4f;

    Vector3 targetPosition; 

    private void Start()
    {
        cam = Camera.main;
        targetPosition = CabinDoor.transform.position;
    }

    private void Update()
    {
        if (!isDoorMoving) 
        {
            OpenDoor(maskCabinDoor, cam, doorUI, CabinDoor);
        }
        if (isDoorOpening)
        {
            CabinDoor.transform.position = Vector3.Lerp(CabinDoor.transform.position, targetPosition, smoothTime * Time.deltaTime);
            if (Vector3.Distance(CabinDoor.transform.position, targetPosition) < 0.1f)
            {
                CabinDoor.transform.position = targetPosition;
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
                if (CabinDoorOpen.activeSelf)
                {
                    targetPosition = CabinDoor.transform.position + new Vector3(0, 0, -1); 
                    CabinDoorOpen.SetActive(false);
                }
                else
                {
                    targetPosition = CabinDoor.transform.position + new Vector3(0, 0, 1); 
                    CabinDoorOpen.SetActive(true);
                }
                isDoorOpening = true;
                isDoorMoving = true; 
            }
        }
    }
}
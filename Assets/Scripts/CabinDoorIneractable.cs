using UnityEngine;

public class CabinDoorInteractable : MonoBehaviour
{
    [SerializeField] GameObject CabinDoor;
    [SerializeField] GameObject doorUI;
    [SerializeField] LayerMask maskCabinDoor;

    public GameObject CabinDoorOpen;

    Camera cam;
    bool isDoorOpening = false;
    bool isDoorMoving = false; // Added flag to track if the door is currently moving
    float smoothTime = 4f;

    Vector3 targetPosition; // Changed from Quaternion to Vector3 for position movement

    private void Start()
    {
        cam = Camera.main;
        targetPosition = CabinDoor.transform.position; // Initialize target position to current position
    }

    private void Update()
    {
        if (!isDoorMoving) // Check if the door is not currently moving
        {
            OpenDoor(maskCabinDoor, cam, doorUI, CabinDoor);
        }
        if (isDoorOpening)
        {
            CabinDoor.transform.position = Vector3.Lerp(CabinDoor.transform.position, targetPosition, smoothTime * Time.deltaTime); // Change rotation to position
            if (Vector3.Distance(CabinDoor.transform.position, targetPosition) < 0.1f) // Change from Quaternion.Angle to Vector3.Distance for position
            {
                CabinDoor.transform.position = targetPosition;
                isDoorOpening = false;
                isDoorMoving = false; // Reset the flag when the door movement is complete
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

        if (Input.GetKeyDown(KeyCode.E) && !isDoorMoving) // Check if the door is not currently moving
        {
            if (Physics.Raycast(ray, out hit, 2, mask))
            {
                if (CabinDoorOpen.activeSelf)
                {
                    targetPosition = CabinDoor.transform.position + new Vector3(0, 0, 1); // Move z position by 1
                    CabinDoorOpen.SetActive(false);
                }
                else
                {
                    targetPosition = CabinDoor.transform.position + new Vector3(0, 0, -1); // Move z position by -1
                    CabinDoorOpen.SetActive(true);
                }
                isDoorOpening = true;
                isDoorMoving = true; // Set the flag indicating that the door is now moving
            }
        }
    }
}
using UnityEngine;

public class DoorInteractable : MonoBehaviour
{
    [SerializeField] GameObject doorParent;
    [SerializeField] GameObject doorUI;
    [SerializeField] LayerMask maskDoor;

    public GameObject DoorOpen;

    Camera cam;
    bool isDoorOpening = false;
    bool isDoorMoving = false; // Added flag to track if the door is currently moving
    float smoothTime = 4f;

    Quaternion targetRotation;
    private void Start()
    {
        cam = Camera.main;
        targetRotation = doorParent.transform.rotation;
    }

    private void Update()
    {
        if (!isDoorMoving) // Check if the door is not currently moving
        {
            OpenDoor(maskDoor, cam, doorUI, doorParent);
        }
        if (isDoorOpening)
        {
            doorParent.transform.rotation = Quaternion.Lerp(doorParent.transform.rotation, targetRotation, smoothTime * Time.deltaTime);
            if (Quaternion.Angle(doorParent.transform.rotation, targetRotation) < 1f)
            {
                doorParent.transform.rotation = targetRotation;
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
                if (DoorOpen.activeSelf)
                {
                    targetRotation = Quaternion.Euler(0, -90, 0) * doorParent.transform.rotation;
                    DoorOpen.SetActive(false);
                }
                else
                {
                    targetRotation = Quaternion.Euler(0, 90, 0) * doorParent.transform.rotation;
                    DoorOpen.SetActive(true);
                }
                isDoorOpening = true;
                isDoorMoving = true; // Set the flag indicating that the door is now moving
            }
        }
    }
}


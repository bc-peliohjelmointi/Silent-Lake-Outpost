using UnityEngine;

public class DoorInteractable : MonoBehaviour
{
    [SerializeField] GameObject doorParent;
    [SerializeField] GameObject doorUI;
    [SerializeField] LayerMask maskDoor;

    public GameObject DoorOpen;

    Camera cam;
    public bool isDoorOpening = false;
    bool isDoorMoving = false; 
    float smoothTime = 4f;

    Quaternion targetRotation;

    // variable for lowering background sounds when player is inside with the door closed

    [SerializeField] AudioSource cricketSounds;
    private void Start()
    {
        cam = Camera.main;
        targetRotation = doorParent.transform.rotation;
    }

    private void Update()
    {
        if (!isDoorMoving)
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
                isDoorMoving = true; 
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("InsideTrigger") && !DoorOpen.activeSelf && !isDoorMoving)
        {
            cricketSounds.volume = 0.15f;
        }

        else
        {
            cricketSounds.volume = 0.5f;
        }
    }
}


using UnityEngine;
using System.Threading.Tasks;
using StarterAssets;

public class PuttingOutFire : MonoBehaviour
{
    [SerializeField] GameObject Fire;
    [SerializeField] GameObject CampFire;
    [SerializeField] GameObject fireUI;
    [SerializeField] GameObject IsFireOn;
    [SerializeField] GameObject Darkening;
    [SerializeField] LayerMask maskFire;

    [SerializeField] GameObject turnOffFireUI;
    [SerializeField] GameObject questionUI;
    [SerializeField] GameObject backToTowerUI;

    [SerializeField] GameObject transitionToTowerTrigger;

    Camera cam;

    FirstPersonController fpsController;
    private void Start()
    {
        cam = Camera.main;
        fpsController = GetComponent<FirstPersonController>();
    }
    private void Update()
    {
        PutOutFire(maskFire, cam, fireUI, CampFire, Fire);
    }

    private async void PutOutFire(LayerMask mask, Camera cam, GameObject UI, GameObject item, GameObject fire)
    {
        if (IsFireOn.activeSelf)
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
                    fpsController.enabled = false;
                    transitionToTowerTrigger.SetActive(true);
                    Darkening.SetActive(true);
                    IsFireOn.SetActive(false);
                    UI.SetActive(false);
                    await Task.Delay(3000);
                    Destroy(Fire);
                    await Task.Delay(4000);
                    fpsController.enabled = true;
                    Darkening.SetActive(false);
                    turnOffFireUI.SetActive(false);
                    questionUI.SetActive(true);
                    Invoke("TurnOffQuestionUI", 4f);
                    Invoke("BackToTowerUI", 5f);
                }
            }
        }
        
    }

    private void TurnOffQuestionUI()
    {
        questionUI.SetActive(false);
    }

    private void BackToTowerUI() 
    {
        backToTowerUI.SetActive(true);
    }
}
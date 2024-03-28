using Palmmedia.ReportGenerator.Core;
using UnityEngine;
using System.Threading.Tasks;

public class PuttingOutFire : MonoBehaviour
{
    [SerializeField] GameObject Fire;
    [SerializeField] GameObject CampFire;
    [SerializeField] GameObject fireUI;
    [SerializeField] GameObject IsFireOn;
    [SerializeField] GameObject Darkening;
    [SerializeField] LayerMask maskFire;


    Camera cam;
    private void Start()
    {
        cam = Camera.main;
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
                    Darkening.SetActive(true);
                    IsFireOn.SetActive(false);
                    UI.SetActive(false);
                    await Task.Delay(3000);
                    Destroy(Fire);
                    await Task.Delay(4000);
                    Darkening.SetActive(false);
                }
            }
        }
        
    }
}
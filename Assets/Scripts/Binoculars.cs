using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Binoculars : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _virtualCamera;

    [SerializeField] GameObject darkening;
    [SerializeField] GameObject note;

    [SerializeField] GameObject binoculars;
    [SerializeField] GameObject UI;
    [SerializeField] float minZoom = 2f;
    [SerializeField] float maxZoom = 50f;
    [SerializeField] float sensitivity = 15f;
    public float fov;

    public bool isZoomed;

    [SerializeField] Texture2D binocImage;
    Tasks taskScript;
    Bringup pauseScript;

    private void Start()
    {
        taskScript = GetComponent<Tasks>();
        pauseScript = GetComponent<Bringup>();
    }

    private void Update()
    {
        CheckZoom();

        if (isZoomed)
        {
            ScrollWheelZoom();
            if (fov <= 11 && !taskScript.hasSeenCamp)
            {
                taskScript.SpotFire();
            }
        }

        else
        {
            _virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(_virtualCamera.m_Lens.FieldOfView, maxZoom, Time.deltaTime * sensitivity);
        }
    }

    void CheckZoom()
    {
        if (binoculars.activeSelf && Input.GetKeyDown(KeyCode.Mouse0))
        {
            isZoomed = true;
        }

        else if (isZoomed && Input.GetKeyDown(KeyCode.Mouse0))
        {
            isZoomed = false;
            binoculars.SetActive(true);
        }

        else if(darkening.activeSelf || note.activeSelf)
        {
            isZoomed = false;
        }

        else if (!isZoomed)
        {
            isZoomed = false;
            if(pauseScript.isPaused == false)
            {
                UI.SetActive(true);
            }
            
            else
            {
                UI.SetActive(false);
            }
        }
    }

    void OnGUI()
    {
        if (isZoomed && pauseScript.isPaused == false)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), binocImage);
            binoculars.SetActive(false);
            UI.SetActive(false);
        }

    }

    void ScrollWheelZoom()
    {
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        scrollWheelInput *= -1;

        fov = _virtualCamera.m_Lens.FieldOfView;
        fov += scrollWheelInput * sensitivity;
        fov = Mathf.Clamp(fov, minZoom, maxZoom);
        _virtualCamera.m_Lens.FieldOfView = fov;
    }
}
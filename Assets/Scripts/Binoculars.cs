using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


/// <summary>
/// This class is made by Leevi. It handles everything to do with the binoculars. Zooming and drawing the zoom GUI image
/// </summary>
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

    /// <summary>
    /// Using this to check if player is spotting the campfire with the binoculars and unzooming when player isn't zoomed
    /// </summary>
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

    /// <summary>
    /// Checking if the playr is zooming
    /// </summary>
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

    /// <summary>
    /// Drawing the GUI image whenever the player is zooming
    /// </summary>
    void OnGUI()
    {
        if (isZoomed && pauseScript.isPaused == false)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), binocImage);
            binoculars.SetActive(false);
            UI.SetActive(false);
        }

    }

    /// <summary>
    /// Adjusting the FOV of the player when zooming via Scroll Wheel
    /// </summary>
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
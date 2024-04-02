using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class MouseLook : MonoBehaviour
{
    public Slider slider;
    public Transform playerBody;

    private FirstPersonController firstPersonController;
    private float mouseSensitivity;

    void Start()
    {
        firstPersonController = GetComponent<FirstPersonController>();
        mouseSensitivity = PlayerPrefs.GetFloat("currentSensitivity", 100f);
        slider.value = mouseSensitivity / 500f;
    }

    public void AdjustSpeed(float newSpeed)
    {
        mouseSensitivity = newSpeed * 500;
        PlayerPrefs.SetFloat("currentSensitivity", mouseSensitivity);
    }

    // Add a method to enable/disable looking
    public void SetLookingEnabled(bool enabled)
    {
        firstPersonController.enabled = enabled;
    }
}

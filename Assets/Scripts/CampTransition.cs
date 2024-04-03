using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class CampTransition : MonoBehaviour
{
    [SerializeField] GameObject darkeningEffect;
    [SerializeField] Transform targetPosition;
    private Quaternion targetRotation;

    private void Start()
    {
        targetRotation = transform.rotation;
    }

    
    private async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ToCampTransition"))
        {
            darkeningEffect.SetActive(true);
            await Task.Delay(3000);
            // vaiha player sijainti
            transform.position = targetPosition.position;
            targetRotation = Quaternion.Euler(0, -361, 0) * targetRotation;
            await Task.Delay(4000);
            darkeningEffect.SetActive(false);
        }
    }
}

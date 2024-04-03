using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampTransition : MonoBehaviour
{
    [SerializeField] GameObject darkeningEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ToCampTransition"))
        {
            darkeningEffect.SetActive(true);
        }
    }
}

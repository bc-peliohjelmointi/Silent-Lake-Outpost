using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCannibal : MonoBehaviour
{
    [SerializeField] GameObject cannibal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ActivateCannibal"))
        {
            cannibal.SetActive(true);
        }
    }
}

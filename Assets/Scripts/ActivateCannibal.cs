using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCannibal : MonoBehaviour
{
    [SerializeField] GameObject cannibal;
    [SerializeField] GameObject Barrier;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ActivateCannibal"))
        {
            Barrier.SetActive(true);
            cannibal.SetActive(true);
        }
    }
}

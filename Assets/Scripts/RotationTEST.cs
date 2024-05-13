using UnityEngine;

public class LookAtFire : MonoBehaviour
{
    public GameObject emptyObject;
    public GameObject fireObject; 

    void Update()
    {
        if (emptyObject.activeSelf)
        {
            Vector3 direction = fireObject.transform.position - transform.position;

            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}
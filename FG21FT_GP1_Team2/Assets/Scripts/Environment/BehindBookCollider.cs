using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindBookCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Potion"))
        {
            other.transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
        }
    }
}

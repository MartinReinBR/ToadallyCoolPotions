using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ingredient") || other.gameObject.CompareTag("Potion"))
        {
            Destroy(other.gameObject);
        }
    }
}

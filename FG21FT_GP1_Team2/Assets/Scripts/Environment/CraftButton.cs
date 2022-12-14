using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    public GameObject Cauldron;

    private void OnMouseDown()
    {
        Cauldron.GetComponent<Cauldron>().CraftPotion();
    }
}

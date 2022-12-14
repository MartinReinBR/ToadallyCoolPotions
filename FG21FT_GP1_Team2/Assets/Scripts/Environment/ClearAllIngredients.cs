using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearAllIngredients : MonoBehaviour
{
    public AudioClip buttonClick;

    private void OnMouseDown()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Ingredient");

        TempAudioManager.instance.PlaySoundEffect(buttonClick);

        for (int i = 0; i < objects.Length; i++)
        {
            Destroy(objects[i]);
            ObjectManager.allIngredients.Clear();
        }
    }
}

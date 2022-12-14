using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCauldron : MonoBehaviour
{
    public GameObject Cauldron;
    public AudioClip buttonClick;

    private void OnMouseDown()
    {
        Cauldron.GetComponent<Cauldron>().ResetCouldron();
        TempAudioManager.instance.PlaySoundEffect(buttonClick);
    }
}

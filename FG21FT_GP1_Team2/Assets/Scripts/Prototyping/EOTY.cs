using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EOTY : MonoBehaviour
{
    public int Index;
    public AudioClip[] HereComeThatBoy;
    public GameObject PauseMenu;

    public GameObject ESOTYpanel;
    public ESOTY Team;

    private void OnMouseDown()
    {
        if (Time.timeScale > 0 && Index == 0)
        {
            TempAudioManager.instance.PlaySoundEffectCustomVolume(HereComeThatBoy[UnityEngine.Random.Range(0, HereComeThatBoy.Length - 1)], 0.2f);
        }

        if(Index == 1)// and all is unlocked
        {

        }
    }

    [Serializable]
    public class ESOTY
    {
        public List<ESOTYindividual> Team;
    }

    [Serializable]
    public class ESOTYindividual
    {
        public string Name;
        public Sprite Image;
        public List<AudioClip> AudioClips;
        public List<string> Texts;
    }
} 

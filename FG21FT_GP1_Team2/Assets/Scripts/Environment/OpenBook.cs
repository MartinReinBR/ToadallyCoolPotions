using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made by Hadi
public class OpenBook : MonoBehaviour
{
    [SerializeField] private GameObject bookUI;
    [SerializeField] private ParticleSystem bookParticles;
    
    private void OnMouseDown()
    {
        if (!bookUI.activeSelf)
        {
            bookUI.SetActive(true);
            if (bookParticles.isPlaying)
            {
                bookParticles.Stop();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && bookUI.activeSelf)
        {
            bookUI.SetActive(false);
        }
    }
}

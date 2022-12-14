using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPages : MonoBehaviour
{
    public AudioClip flipPageSound;
    [SerializeField] private List<GameObject> pages = new List<GameObject>();
    [SerializeField] private int currentPage = 1;

    [SerializeField] private GameObject _rightArrow;
    [SerializeField] private GameObject _leftArrow;

    private void Start()
    {
        pages[0].SetActive(true);
        pages[1].SetActive(true);
    }

    public void OnSwapButtonRight()
    {
        if (currentPage < pages.Count - 1)
        {
            currentPage += 2;
            pages[currentPage - 3].SetActive(false);
            pages[currentPage - 2].SetActive(false);
            pages[currentPage - 1].SetActive(true);
            pages[currentPage].SetActive(true);
            TempAudioManager.instance.PlaySoundEffect(flipPageSound);
        }

        _leftArrow.SetActive(true);

        if (currentPage == pages.Count - 1)
        {
            _rightArrow.SetActive(false);
        }
        
    }
    
    public void OnSwapButtonLeft()
    {
        if (currentPage > 1)
        {
            currentPage -= 2;
            pages[currentPage + 2].SetActive(false);
            pages[currentPage + 1].SetActive(false);
            pages[currentPage - 1].SetActive(true);
            pages[currentPage].SetActive(true);
            TempAudioManager.instance.PlaySoundEffect(flipPageSound);
        }

        _rightArrow.SetActive(true);

        if (currentPage == 1)
        {
            _leftArrow.SetActive(false);
        }
    }
}

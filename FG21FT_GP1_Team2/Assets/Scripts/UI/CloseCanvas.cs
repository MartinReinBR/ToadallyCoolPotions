using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCanvas : MonoBehaviour
{
    private GameObject canvas;

    private void Start()
    {
        canvas = this.gameObject;
    }

    public void OnCloseButtonClick()
    {
        if (canvas.activeSelf)
        {
            canvas.SetActive(false);
        }
    }
}

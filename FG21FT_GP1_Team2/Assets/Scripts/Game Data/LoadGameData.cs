using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameData : MonoBehaviour
{
    private void Awake()
    {
        GameData.Load();
        Debug.Log("Loaded GameData..");
    }
}

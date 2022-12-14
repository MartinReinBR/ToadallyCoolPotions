using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    private float _startSize;

    private void OnMouseEnter()
    {
        //GetComponent<Renderer>().material.SetFloat("_Outline", 2f);
        _startSize = transform.localScale.x;
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f) * transform.localScale.x; //localScale multiplication was just to see the actual thing, remove it when needed
    }

    private void OnMouseExit()
    {
        //GetComponent<Renderer>().material.SetFloat("_Outline", 0f);
        transform.localScale = Vector3.one * _startSize; //localScale multiplication was just to see the actual thing, remove it when needed
    }

}

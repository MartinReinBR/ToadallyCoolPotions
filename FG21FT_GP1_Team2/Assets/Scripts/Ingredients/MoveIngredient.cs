using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveIngredient : MonoBehaviour
{
    private Rigidbody rig;

    #region 
    private float cauldronBounderyX1 = 18.7f;
    private float cauldronBounderyX2 = 19.2f;
    private float cauldronBounderyY1 = 11.25f;
    private float cauldronBounderyY2 = 12.7f;

    private float customerBounderyX1 = 19.4f;
    private float customerBounderyX2 = 20.5f;
    private float customerBounderyY1 = 11.5f;
    private float customerBounderyY2 = 12.4f;

    private float trashcanBounderyX1 = 16.8f;
    private float trashcanBounderyX2 = 17.35f;
    private float trashcanBounderyY1 = 10f;
    private float trashcanBounderyY2 = 12f;

    private float ropeBounderyX1 = 17f;
    private float ropeBounderyX2 = 18.3f;
    private float ropeBounderyY1 = 12.3f;
    private float ropeBounderyY2 = 12.9f;
    #endregion 

    private Vector3 _savedPos;
    private Vector3 _deltaThrowDir;
    private float _throwForce = 10f;
    public static event Action<bool> HighlightRopes;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _deltaThrowDir = transform.position - _savedPos;
        _savedPos = transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z  - 2f);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = objPosition;
        rig.isKinematic = true;
    }

    private void OnMouseUp()
    {
        if (gameObject.CompareTag("Ingredient"))
        {
            if ((transform.position.x > cauldronBounderyX1 && transform.position.x < cauldronBounderyX2)
            && (transform.position.y > cauldronBounderyY1 && transform.position.y < cauldronBounderyY2))
            {
                rig.isKinematic = false;
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f);
                
            }

            else if ((transform.position.x > trashcanBounderyX1 && transform.position.x < trashcanBounderyX2)
            && (transform.position.y > trashcanBounderyY1 && transform.position.y < trashcanBounderyY2))
            {
                rig.isKinematic = false;
                transform.position = new Vector3(17.05f, transform.position.y, -2f);
                TempAudioManager.instance.PlayDropIngredient();
            }

            else
            {
                rig.isKinematic = false;
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
                rig.AddForce(new Vector3(_deltaThrowDir.x, _deltaThrowDir.y, 0f) * 100, ForceMode.Impulse);
                Debug.Log((new Vector3(_deltaThrowDir.x, _deltaThrowDir.y, 0f) * _throwForce).x + "/" +  (new Vector3(_deltaThrowDir.x, _deltaThrowDir.y, 0f) * _throwForce).y);
            }
        }

        if (gameObject.CompareTag("Potion"))
        {
            if ((transform.position.x > customerBounderyX1 && transform.position.x < customerBounderyX2)
            && (transform.position.y > customerBounderyY1 && transform.position.y < customerBounderyY2))
            {
                rig.isKinematic = false;
                transform.position = new Vector3(21.8f, transform.position.y, 0.5f);
            }

            else if ((transform.position.x > trashcanBounderyX1 && transform.position.x < trashcanBounderyX2)
            && (transform.position.y > trashcanBounderyY1 && transform.position.y < trashcanBounderyY2))
            {
                rig.isKinematic = false;
                transform.position = new Vector3(17.05f, transform.position.y, -2f);
                TempAudioManager.instance.PlayDropIngredient();
            }

            else if ((transform.position.x > ropeBounderyX1 && transform.position.x < ropeBounderyX2)
            && (transform.position.y > ropeBounderyY1 && transform.position.y < ropeBounderyY2))
            {
                if(transform.position.x > 18)
                {
                    rig.isKinematic = false;
                    transform.position = new Vector3(17.75f, 13, -0.5f);
                }
                else
                {
                    rig.isKinematic = false;
                    transform.position = new Vector3(transform.position.x - 0.8f, 13, -0.5f);
                }
            }

            else
            {
                rig.isKinematic = false;
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);

                rig.AddForce(new Vector3(_deltaThrowDir.x, _deltaThrowDir.y, 0f) * 100, ForceMode.Impulse);
                Debug.Log((new Vector3(_deltaThrowDir.x, _deltaThrowDir.y, 0f) * _throwForce).x + "/" + (new Vector3(_deltaThrowDir.x, _deltaThrowDir.y, 0f) * _throwForce).y);
            }

            HighlightRopes?.Invoke(false);
        }
    }

    private void OnMouseDown()
    {
        if(gameObject.tag == "Potion")
        {
            HighlightRopes?.Invoke(true);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveIngredient : MonoBehaviour
{
    private Rigidbody rig;
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
        bool ShouldDropNormal = true;
        // Define a ray that starts at the camera's position and points towards the cursor
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Use the ray to perform a line trace, and store the result in a RaycastHit variable
        RaycastHit[] hits = Physics.RaycastAll(ray);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("TrashcanBoundary"))
            {
                rig.isKinematic = false;
                transform.position = new Vector3(17.05f, transform.position.y, -2f);
                TempAudioManager.instance.PlayDropIngredient();
                ShouldDropNormal = false;
                break;
            }

            else if (hit.collider.CompareTag("CauldronBoundary") && gameObject.CompareTag("Ingredient"))
            {
                rig.isKinematic = false;
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f);
                ShouldDropNormal = false;
                break;
            }

            else if (hit.collider.CompareTag("CustomerBoundary") && gameObject.CompareTag("Potion"))
            {
                rig.isKinematic = false;
                transform.position = new Vector3(21.8f, transform.position.y, 0.5f);
                ShouldDropNormal = false;
                break;
            }

            else if (hit.collider.CompareTag("RopeBoundary") && gameObject.CompareTag("Potion"))
            {
                if (transform.position.x > 18)
                {
                    rig.isKinematic = false;
                    transform.position = new Vector3(17.75f, 13, -0.5f);
                }
                else
                {
                    rig.isKinematic = false;
                    transform.position = new Vector3(transform.position.x - 0.8f, 13, -0.5f);
                }
                ShouldDropNormal = false;
                break;
            }

        }

        HighlightRopes?.Invoke(false);

        if (ShouldDropNormal)
        {
            rig.isKinematic = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);

            rig.AddForce(new Vector3(_deltaThrowDir.x, _deltaThrowDir.y, 0f) * 100, ForceMode.Impulse);
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


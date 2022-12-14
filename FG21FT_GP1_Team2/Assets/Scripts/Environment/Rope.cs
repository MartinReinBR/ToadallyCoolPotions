using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private bool _objectAttached = false;
    private Vector3 _startPos;

    private void Start()
    {
        _startPos = gameObject.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_objectAttached)
        {
            other.transform.position = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _objectAttached = false;
    }

    private void OnTriggerStay(Collider other)
    {
        _objectAttached = true;
    }

    private void HighliteRope(bool _state)
    {
        if(!_objectAttached && _state)
        {
            //_startSize = transform.localScale.x;
            //transform.localScale = new Vector3(1.1f, 1.1f, 1.1f) * transform.localScale.x;

            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        }
        else if(!_objectAttached && !_state)
        {
            //transform.localScale = new Vector3(_startSize, _startSize, _startSize);

            transform.position = _startPos;
        }
    }

    private void OnEnable()
    {
        MoveIngredient.HighlightRopes += HighliteRope;
    }

    private void OnDisable()
    {
        MoveIngredient.HighlightRopes -= HighliteRope;
    }
}

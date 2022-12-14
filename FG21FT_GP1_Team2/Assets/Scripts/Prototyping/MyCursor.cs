using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCursor : MonoBehaviour
{
    private TrailRenderer _tr;
    [SerializeField] private Color _trailColor;
    [SerializeField] [Range(0, 1)] private float _transparancy = 0.5f;
    private Vector3 _mgManagerPos;

    private void Start()
    {
        _tr = GetComponent<TrailRenderer>();
        _tr.startColor = new Color(_trailColor.r, _trailColor.g, _trailColor.b, _transparancy);
        _tr.startWidth = (0.25f * 2) * transform.parent.transform.localScale.x;
        _tr.enabled = false;
        _mgManagerPos = MinigameManager.instance.gameObject.transform.position;
    }

    private void Update()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit _hit;
        if (Physics.Raycast(_ray, out _hit, LayerMask.NameToLayer("Minigame")))
        {
            
            transform.position = new Vector3(_hit.point.x, _hit.point.y, _mgManagerPos.z - 0.05f);
        }
        else
        {
        }
    }

    private void EnableTrail()
    {
        _tr.enabled = true;
    }

    private void OnEnable()
    {
        Minigame.EnableCursorTrail += EnableTrail;
    }

    private void OnDisable()
    {
        Minigame.EnableCursorTrail -= EnableTrail;
    }
}

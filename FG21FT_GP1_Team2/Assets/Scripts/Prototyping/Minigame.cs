using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Minigame : MonoBehaviour
{
    [SerializeField] private bool _allowFailBeforeGameStart = false;
    [SerializeField] private bool _pointTimeIsGameTime = true;
    [SerializeField] private float _timeToDrawPoint;
    [SerializeField] private float _minigameTime;
    [SerializeField] private Transform _drawPointHolder;
    [SerializeField] private List<SphereCollider> _drawPoints;
    private int _currentDrawPoint;

    private bool _minigameState = false;
    private bool _minigameCompleted = false;
    private Coroutine _co;
    public static event Action<int> DrawPointReached;
    public static event Action EnableCursorTrail;
    public static event Action EnableAllBut0;

    public ParticleSystem FinishedParticle;

    private void Start()
    {
        if (_pointTimeIsGameTime)
        {
            _timeToDrawPoint = _minigameTime;
        }

        _drawPoints = new List<SphereCollider>();
        foreach (Transform _drawPoint in _drawPointHolder)
        {
            _drawPoints.Add(_drawPoint.gameObject.GetComponent<SphereCollider>());
        }

        _currentDrawPoint = 0;
         
        DisableAllDrawPoints();
        _drawPoints[_currentDrawPoint].enabled = true;
        DrawPointReached?.Invoke(_currentDrawPoint);
    }

    private void OnMouseEnter()
    {
        //Start minigame timer
        StartCoroutine(MinigameTimer(_minigameTime));
        _co = StartCoroutine(MinigameTimer(_timeToDrawPoint));
    }

    private void OnMouseExit()
    {
        FailedMinigame();
    }

    private void CompletedMinigame()
    {
        _minigameState = true;
        MinigameManager.instance.EndMiniGame(_minigameState, 0.5f);
        FinishedParticle.Play();
        _minigameCompleted = true;
        //Sound effect (?)
    }

    private void FailedMinigame()
    {
        if(!_minigameCompleted)
        {
            if (!_allowFailBeforeGameStart)
            {
                if (_minigameState)
                {
                    //End all coroutines
                    StopAllCoroutines();
                    _minigameState = false;
                    MinigameManager.instance.EndMiniGame(_minigameState, 0f);
                }
            }
            else
            {
                //End all coroutines
                StopAllCoroutines();
                _minigameState = false;
                MinigameManager.instance.EndMiniGame(_minigameState, 0f);
            }
        }
    }

    public void IterateDrawPoints(int _drawIndex)
    {
        StopCoroutine(_co);
        if(_drawIndex == _currentDrawPoint && _drawIndex == 0 && !_minigameState) //First drawpoint
        {
            EnableCursorTrail?.Invoke();
            _minigameState = true;
            _currentDrawPoint++;

            //Deactivate the previous point
            if (_currentDrawPoint > 0)
            {
                _drawPoints[_currentDrawPoint - 1].enabled = false;
            }

            //Activate the next point
            if (_currentDrawPoint <= _drawPoints.Count)
            {
                _drawPoints[_currentDrawPoint].enabled = true;
            }

            //Start timer in between points
            _co = StartCoroutine(MinigameTimer(_timeToDrawPoint));

            //Set drawn points color and opacity in the DrawPoint script
            DrawPointReached?.Invoke(_currentDrawPoint);
        }
        else if (_drawIndex == _currentDrawPoint && _drawIndex == 0 && _minigameState) //Allowing the player to loop the shape
        {
            CompletedMinigame();
            _currentDrawPoint++;
            DrawPointReached?.Invoke(_currentDrawPoint);
            StopAllCoroutines();
        }
        else if(_drawIndex == _currentDrawPoint && _drawIndex < _drawPoints.Count - 1)
        {
            _currentDrawPoint++;
            //Deactivate the previous point
            if (_currentDrawPoint > 0)
            {
                _drawPoints[_currentDrawPoint - 1].enabled = false;
            }

            //Activate the next point
            if (_currentDrawPoint <= _drawPoints.Count)
            {
                _drawPoints[_currentDrawPoint].enabled = true;
            }

            //Start timer in between points
            _co = StartCoroutine(MinigameTimer(_timeToDrawPoint));

            //Set drawn points color and opacity in the DrawPoint script
            DrawPointReached?.Invoke(_currentDrawPoint);
        }
        else if(_drawIndex == _currentDrawPoint && _drawIndex == _drawPoints.Count - 1)
        {
            _currentDrawPoint++;
            //Deactivate the previous point
            if (_currentDrawPoint > 0)
            {
                _drawPoints[_currentDrawPoint - 1].enabled = false;
            }
            _currentDrawPoint = 0;
            //Activate the next point
            if (_currentDrawPoint <= _drawPoints.Count)
            {
                _drawPoints[_currentDrawPoint].enabled = true;
            }
            _minigameState = true;

            DrawPointReached?.Invoke(0);
            EnableAllBut0?.Invoke();
        }
        else
        {
            FailedMinigame();
        }

    }

    private void DisableAllDrawPoints()
    {
        foreach (SphereCollider _point in _drawPoints)
        {
            _point.enabled = false;
        }
    }

    private IEnumerator MinigameTimer(float _seconds)
    {
        yield return new WaitForSeconds(_seconds);

        //End the minigame
        FailedMinigame();
    }

    private void OnEnable()
    {
        DrawPoint._DrawPointEntered += IterateDrawPoints;
    }

    private void OnDisable()
    {
        DrawPoint._DrawPointEntered -= IterateDrawPoints;
    }
}

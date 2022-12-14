using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DrawPoint : MonoBehaviour
{
    private int _index;
    public static event Action<int> _DrawPointEntered;

    private SpriteRenderer _sr;

    public Color TargetPointColor;
    public Color CompletedPointColor;
    private ParticleSystem TargetParticles;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _sr.enabled = false;
        _index = transform.GetSiblingIndex();
        TargetParticles = GetComponent<ParticleSystem>();
    }

    private void OnMouseEnter()
    {
        _DrawPointEntered?.Invoke(_index);
    }

    private void SetSprite(int _currentIndex)
    {
        if(_index == _currentIndex)
        {
            _sr.enabled = true;
            //Set opacity to 1, color to red
            _sr.color = new Color(TargetPointColor.r, TargetPointColor.g, TargetPointColor.b, 0.75f);
            TargetParticles.Play();
        }
        else if(_index < _currentIndex)
        {
            TargetParticles.Stop();
            _sr.enabled = true;
            //Set opacity between 0,5 and 1 based on the difference with color green
            float _diff = 0.5f + ((_index / _currentIndex) / 2);
            _sr.color = new Color(CompletedPointColor.r, CompletedPointColor.g, CompletedPointColor.b, _diff);
        }
        else
        {
            //Hide the sprite renderer in totality
            _sr.color = Color.white;
            _sr.enabled = false;
            TargetParticles.Stop();
        }
    }

    private void SetGreen()
    {
        if(_index > 0)
        {
            _sr.enabled = true;
            //Set opacity between 0,5 and 1 based on the difference with color green
            _sr.color = new Color(CompletedPointColor.r, CompletedPointColor.g, CompletedPointColor.b, 0.5f);
        }
    }

    private void OnEnable()
    {
        Minigame.DrawPointReached += SetSprite;
        Minigame.EnableAllBut0 += SetGreen;
    }

    private void OnDisable()
    {
        Minigame.DrawPointReached -= SetSprite;
        Minigame.EnableAllBut0 -= SetGreen;
    }
}

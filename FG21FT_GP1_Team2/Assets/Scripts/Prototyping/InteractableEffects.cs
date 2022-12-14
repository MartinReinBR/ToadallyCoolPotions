using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractableEffects : MonoBehaviour
{
    public bool InteractCursorOnOver = true;
    [SerializeField] private Texture2D _interactableCursor;
    public enum _mouseInteractions {MouseDown, MouseUp, MouseOver, MouseEnter, MouseExit}
    private _mouseInteractions _mouseInteraction;

    public List<Effect> OnMouseOverEffects;
    public List<Effect> OnMouseDownEffects;
    public List<Effect> OnMouseUpEffects;
    public List<Effect> OnMouseEnterEffects;
    public List<Effect> OnMouseExitEffects;

    [HideInInspector]public enum _effectType {Nothing, ScaleUp, ScaleDown, LightUp, LightOff, PlaySoundEffect, PlayParticleEffect }

    private void OnMouseDown()
    {
        foreach (Effect _effect in OnMouseDownEffects)
        {
            _effect.ExecuteEffect(_mouseInteractions.MouseDown, gameObject);
        }
    }

    private void OnMouseUp()
    {
        foreach (Effect _effect in OnMouseUpEffects)
        {
            _effect.ExecuteEffect(_mouseInteractions.MouseUp, gameObject);
        }
    }

    private void OnMouseOver()
    {
        foreach (Effect _effect in OnMouseOverEffects)
        {
            _effect.ExecuteEffect(_mouseInteractions.MouseOver, gameObject);
        }
    }

    private void OnMouseEnter()
    {
        foreach (Effect _effect in OnMouseEnterEffects)
        {
            _effect.ExecuteEffect(_mouseInteractions.MouseEnter, gameObject);
        }
    }

    private void OnMouseExit()
    {
        foreach (Effect _effect in OnMouseExitEffects)
        {
            _effect.ExecuteEffect(_mouseInteractions.MouseExit, gameObject);
        }

        
    }

    

    [System.Serializable]
    public class Effect
    {
        public _effectType _effect;

        [SerializeField] private string _OnMouseDownText;
        [SerializeField] private string _OnMouseUpText;
        [SerializeField] private string _OnMouseOverText;
        [SerializeField] private string _OnMouseEnterText;
        [SerializeField] private string _OnMouseExitText;

        //Execute selected item on x mouse action
        public void ExecuteEffect(_mouseInteractions _interaction, GameObject _gameObject)
        {
            switch (_interaction)
            {
                case _mouseInteractions.MouseDown:
                    switch (_effect)
                    {
                        default: Debug.Log(_OnMouseDownText); break;

                        case _effectType.ScaleUp:

                            break;
                        case _effectType.ScaleDown:

                            break;
                        case _effectType.LightUp:

                            break;
                        case _effectType.LightOff:

                            break;
                        case _effectType.PlaySoundEffect:

                            break;
                        case _effectType.PlayParticleEffect:

                            break;

                    }
                    break;

                case _mouseInteractions.MouseUp:
                    switch (_effect)
                    {
                        default: Debug.Log(_OnMouseUpText); break;

                        case _effectType.ScaleUp:

                            break;
                        case _effectType.ScaleDown:

                            break;
                        case _effectType.LightUp:

                            break;
                        case _effectType.LightOff:

                            break;
                        case _effectType.PlaySoundEffect:

                            break;
                        case _effectType.PlayParticleEffect:

                            break;

                    }
                    break;

                case _mouseInteractions.MouseOver:
                    switch (_effect)
                    {
                        default: Debug.Log(_OnMouseOverText); break;

                        case _effectType.ScaleUp:

                            break;
                        case _effectType.ScaleDown:

                            break;
                        case _effectType.LightUp:

                            break;
                        case _effectType.LightOff:

                            break;
                        case _effectType.PlaySoundEffect:

                            break;
                        case _effectType.PlayParticleEffect:

                            break;

                    }
                    break;

                case _mouseInteractions.MouseEnter:
                    switch (_effect)
                    {
                        default: Debug.Log(_OnMouseEnterText); break;

                        case _effectType.ScaleUp:
                            ScaleUp(_gameObject);
                            break;
                        case _effectType.ScaleDown:
                            ScaleDown(_gameObject);
                            break;
                        case _effectType.LightUp:

                            break;
                        case _effectType.LightOff:

                            break;
                        case _effectType.PlaySoundEffect:

                            break;
                        case _effectType.PlayParticleEffect:

                            break;

                    }
                    break;

                case _mouseInteractions.MouseExit:
                    switch (_effect)
                    {
                        default: Debug.Log(_OnMouseExitText);  break;

                        case _effectType.ScaleUp:
                            ScaleUp(_gameObject);
                            break;
                        case _effectType.ScaleDown:
                            ScaleDown(_gameObject);
                            break;
                        case _effectType.LightUp:

                            break;
                        case _effectType.LightOff:

                            break;
                        case _effectType.PlaySoundEffect:

                            break;
                        case _effectType.PlayParticleEffect:

                            break;

                    }
                    break;
            }
        }

        private void ScaleUp(GameObject _gameObject)
        {
            _gameObject.transform.localScale = _gameObject.transform.localScale * 1.2f;
        }

        private void ScaleDown(GameObject _gameObject)
        {
            _gameObject.transform.localScale = Vector3.one;
        }

        public void EnableLight()
        {

        }

        public void DisableLight()
        {

        }
    }



}

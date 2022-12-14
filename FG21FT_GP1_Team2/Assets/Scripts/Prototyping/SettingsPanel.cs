using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public GameObject PausePanel;

    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _effectsSlider;

    public void ClosePanel()
    {
        float _masterVolume = _masterSlider.value;
        float _musicVolume = _musicSlider.value;
        float _effectsVolume = _effectsSlider.value;

        TempAudioManager.instance.SetVolumes(_masterVolume, _musicVolume, _effectsVolume);

        PausePanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

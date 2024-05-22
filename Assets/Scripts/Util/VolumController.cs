using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] Slider MasterVolumeSlider;
    [SerializeField] Slider BgmVolumeSlider;
    [SerializeField] Slider EffectVolumeSlider;

    private string _masterVolumeKey = "MasterVolume";
    private string _bgmVolumeKey = "BGMVolume";
    private string _effectVolumeKey = "EffectVolume";

    void Start()
    {
        LoadVolume();
    }

    void LoadVolume()
    {
        if (PlayerPrefs.HasKey(_masterVolumeKey))
        {
            float savedMasterVolume = PlayerPrefs.GetFloat(_masterVolumeKey);
            MasterVolumeSlider.value = savedMasterVolume;
            AudioManager.Instance.SetMasterVolume(savedMasterVolume);
        }

        if (PlayerPrefs.HasKey(_bgmVolumeKey))
        {
            float savedBGMVolume = PlayerPrefs.GetFloat(_bgmVolumeKey);
            BgmVolumeSlider.value = savedBGMVolume;
            AudioManager.Instance.SetBGMVolume(savedBGMVolume);
        }

        if (PlayerPrefs.HasKey(_effectVolumeKey))
        {
            float savedEffectVolume = PlayerPrefs.GetFloat(_effectVolumeKey);
            EffectVolumeSlider.value = savedEffectVolume;
            AudioManager.Instance.SetEffectVolume(savedEffectVolume);
        }
    }

    public void OnMasterVolumeChanged()
    {
        float volume = MasterVolumeSlider.value;
        AudioManager.Instance.SetMasterVolume(volume);
        SaveVolume(_masterVolumeKey, volume);
    }

    public void OnBGMVolumeChanged()
    {
        float volume = BgmVolumeSlider.value;
        AudioManager.Instance.SetBGMVolume(volume);
        SaveVolume(_bgmVolumeKey, volume);
    }

    public void OnEffectVolumeChanged()
    {
        float volume = EffectVolumeSlider.value;
        AudioManager.Instance.SetEffectVolume(volume);
        SaveVolume(_effectVolumeKey, volume);
    }

    void SaveVolume(string key, float volume)
    {
        PlayerPrefs.SetFloat(key, volume);
        PlayerPrefs.Save();
    }
}

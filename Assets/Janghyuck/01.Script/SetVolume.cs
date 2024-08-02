using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volumeSlider;

    public void Awake()
    {
        volumeSlider.onValueChanged.AddListener(BGMAudioControl);
    }

    public void BGMAudioControl(float sliderVal)
    {
        mixer.SetFloat("BGM", Mathf.Log10(sliderVal) * 20);
    }
}

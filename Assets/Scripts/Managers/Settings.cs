using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer am;

    public void AudioVolume(float value) {
        am.SetFloat("masterVolume", value);
    }
}

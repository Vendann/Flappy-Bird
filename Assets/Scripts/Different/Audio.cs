using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClipArray;

    void Start() {
        audioSource.PlayOneShot(RandomClip());
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(RandomClip());
        }
    }

    AudioClip RandomClip() {
        return audioClipArray[Random.Range(0, audioClipArray.Length)];
    }
}

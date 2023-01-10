using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioContainer : MonoBehaviour {
    [SerializeField] AudioSource audioToPlay;
    bool isStartPlaying = false;

    void FixedUpdate() {
        CheckAudioStatusAndDestroyOnEndPlay();
    }

    void CheckAudioStatusAndDestroyOnEndPlay() {
        if (!audioToPlay.isPlaying && isStartPlaying) {
            DestroyAudioContainer();
        }
    }

    public void PlayAudioAndDestroyContainerAfter() {
        audioToPlay.Play();
        isStartPlaying = true;
    }

    public void PlayAudio() {
        audioToPlay.Play();
    }

    public void DestroyAudioContainer() {
        Destroy(gameObject);
    }

}

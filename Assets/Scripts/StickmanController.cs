using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanController : MonoBehaviour {
    public Slingshot.StickmanType stickmanType;
    public bool isShooted;
    bool isFlyAudioPlayed;
    public bool isStoped;
    bool isFlyAudioStoped;
    bool hitAudioPlayed;

    [SerializeField] AudioSource flyAudio;
    [SerializeField] AudioSource hitAudio;
    [SerializeField] AudioSource hulkAudio;

    private void Update() {
        if (isShooted && !isFlyAudioPlayed) {
            if (flyAudio != null) {
                flyAudio.Play();
                isFlyAudioPlayed = true;
            }
        }

        if (isShooted && isStoped && !isFlyAudioStoped) {
            flyAudio.Stop();
            isFlyAudioStoped = true;
        }

        if (isShooted && isStoped && !hitAudioPlayed) {
            hitAudio.Play();
            hitAudioPlayed = true;
        }
    }

    public void PlayHulkAudio() {
        hulkAudio.Play();
    }
}

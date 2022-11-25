using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour {
    
    [SerializeField] AudioSource buttonSoundPrefab;

    void PlayButtonSound() {
        buttonSoundPrefab.Play();
    }

    void Start() {
        gameObject.GetComponent<Button>().onClick.AddListener(PlayButtonSound);
    }
}

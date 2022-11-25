using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour {

    [SerializeField] Image soundBlockImage;

    void Start() {
        if (MyObj.Instance.GetSoundOffStatus()) {
            soundBlockImage.enabled = true;
        } else {
            soundBlockImage.enabled = false;
        }
    }

    public void ChangeSoundStatus() {
        if (MyObj.Instance.GetSoundOffStatus()) {
            MyObj.Instance.TurnOnSound();
            soundBlockImage.enabled = false;
        } else {
            MyObj.Instance.TurnOffSound();
            soundBlockImage.enabled = true;
        }
    }
    
}

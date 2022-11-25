using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChecker : MonoBehaviour {

    void Update() {
        if (MyObj.Instance.GetSoundOffStatus()) {
            AudioListener.volume = 0;
        } else {
            AudioListener.volume = 1;
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeMoneyBonusButton : MonoBehaviour {

    LevelState levelState;

    void Start() {
        levelState = FindObjectOfType<LevelState>();
    }

    
}

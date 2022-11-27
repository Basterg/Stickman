using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountMoneyOnLevel : MonoBehaviour {
    LevelState levelState;
    TextMeshProUGUI moneyOnLevelText;


    void Start() {
        levelState = FindObjectOfType<LevelState>();
        moneyOnLevelText = GetComponent<TextMeshProUGUI>();

        moneyOnLevelText.text = levelState.GetCountOfMoneyOnLevel().ToString();
    }
}

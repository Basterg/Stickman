using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountBonusMoneyOnLevel : MonoBehaviour {
    LevelState levelState;
    TextMeshProUGUI bounsMoneyOnLevelText;


    void Start() {
        levelState = FindObjectOfType<LevelState>();
        bounsMoneyOnLevelText = GetComponent<TextMeshProUGUI>();

        bounsMoneyOnLevelText.text = (levelState.GetCountOfMoneyOnLevel() * 3).ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonsController : MonoBehaviour {

    [SerializeField] List<ButtonLevelController> levelButtonControllers;

    int countOfUnlockedLevels;

    void Start() {
        UpdateLevelsView();
    }

    void UpdateLevelsView() {
        countOfUnlockedLevels = GlobalLevelsInfo.GetCountOfUnlockedLevels(); // мб тут чёто не так .. мб мб

        for (int i = 0; i < countOfUnlockedLevels; i++) {
            levelButtonControllers[i].UnblockButtonAndSetStars();
        }
    }

    void OnEnable() {
        MyObj.OnLoad += UpdateLevelsView;
    }

    void OnDisable() {
        MyObj.OnLoad -= UpdateLevelsView;
    }

    
}

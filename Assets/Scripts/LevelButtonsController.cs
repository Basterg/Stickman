using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonsController : MonoBehaviour {

    [SerializeField] List<ButtonLevelController> levelButtonControllers;

    int countOfUnlockedLevels;

    void Start() {
        countOfUnlockedLevels = GlobalLevelsInfo.GetCountOfUnlockedLevels(); // мб тут чёто не так

        for (int i = 0; i < countOfUnlockedLevels; i++) {
            levelButtonControllers[i].UnblockButtonAndSetStars();
        }
    }

    
}

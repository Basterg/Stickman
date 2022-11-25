using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour {
    int currentLevelNumber;
    LevelCanvasController levelCanvasController;

    void Start() {
        currentLevelNumber = SceneManager.GetActiveScene().buildIndex;
        levelCanvasController = FindObjectOfType<LevelCanvasController>();
    }
    public void LoadNextLevelFunction() {
        
        if (!levelCanvasController.GetLevelBlockStatus()) {
            if (currentLevelNumber < 21) {
                SceneManager.LoadScene("Level" + (currentLevelNumber + 1) + "Episode1");
            }
            // if (currentLevelNumber > 40 ) {
            //     SceneManager.LoadScene("Level" + (currentLevelNumber - 42 + 1) + "Episode2");
            // }
        
            if (currentLevelNumber == 21 ) {
                SceneManager.LoadScene("Main Menu");
            }
        } else {
            if (currentLevelNumber == 21 ) {
                SceneManager.LoadScene("Main Menu");
            } else {
                UnlockLevelForMoneyStars();
            }
        }
        
    }

    void UnlockLevelForMoneyStars() {
        if (GlobalLevelsInfo.GetStarsMoneyCount() >= 3) {
            GlobalLevelsInfo.SpendStarsMoney(3);
            levelCanvasController.UnlockNextLevel();
        } else {
            // не хватает звёзд
        }
        
    }
}

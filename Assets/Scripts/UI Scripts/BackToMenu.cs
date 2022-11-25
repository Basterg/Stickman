using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour {
    public void LoadMainMenuScene() {
        SceneManager.LoadScene("Main Menu");
        GlobalLevelsInfo.SaveData();
    }
}

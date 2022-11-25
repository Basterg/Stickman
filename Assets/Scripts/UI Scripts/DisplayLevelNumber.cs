using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DisplayLevelNumber : MonoBehaviour {

    TextMeshProUGUI levelNumberText;
    
    void Start() {
        levelNumberText = gameObject.GetComponent<TextMeshProUGUI>();
        levelNumberText.text = "1-" + SceneManager.GetActiveScene().buildIndex.ToString();
    }
}

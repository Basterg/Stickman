using UnityEngine;
using TMPro;

public class SumScoreDisplay : MonoBehaviour {
    [SerializeField] TextMeshProUGUI sumScoreText;

    void Update() {
        sumScoreText.text = GlobalLevelsInfo.GetSumScore().ToString();
    }
}


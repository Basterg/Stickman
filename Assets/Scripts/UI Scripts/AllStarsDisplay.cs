using UnityEngine;
using TMPro;

public class AllStarsDisplay : MonoBehaviour {
    [SerializeField] TextMeshProUGUI allStarsText;

    void Update() {
        allStarsText.text = GlobalLevelsInfo.GetAllStarsCount().ToString(); // разделить на эпизоды
    }
}

using UnityEngine;
using TMPro;

public class StarsMoneyDisplay : MonoBehaviour {
    [SerializeField] TextMeshProUGUI moneyStarsText;

    void Update() {
        moneyStarsText.text = GlobalLevelsInfo.GetStarsMoneyCount().ToString();
    }
}

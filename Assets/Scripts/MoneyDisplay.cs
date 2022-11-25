using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour {

    [SerializeField] TextMeshProUGUI moneyText;

    void Update() {
        moneyText.text = GlobalLevelsInfo.GetMoneyCount().ToString();
    }

    
}

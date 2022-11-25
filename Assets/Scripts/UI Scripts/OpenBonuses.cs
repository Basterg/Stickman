using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenBonuses : MonoBehaviour {
    [SerializeField] Image bonusesPanel;

    public void OpenOrCloseBonusesPanel() {
        if (bonusesPanel.isActiveAndEnabled) {
            bonusesPanel.gameObject.SetActive(false);
        } else {
            bonusesPanel.gameObject.SetActive(true);
        }
    }
}

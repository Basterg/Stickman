using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonLevelController : MonoBehaviour {
    [SerializeField] int numberOfLevel;
    [SerializeField] int numberOfEpisode;
    [SerializeField] Image blockImage;
    [SerializeField] TextMeshProUGUI buttonLevelNumberText;
    [SerializeField] StarsCountController starsCountController;
    [SerializeField] Button levelButton;

    [SerializeField] GameObject freePackAlert;
    [SerializeField] GameObject freeMoneyAlert;
    [SerializeField] GameObject bonusMoneyAlert;


    void OpenLevel() {
        // Если уровень пройден - проверить забран ли бонус за первое прохождение - если нет то открыть окно с вопросом
        // проверить разблокирован ли увроень и если да - то навесить блок имадж
        // возможно получить инфу по звёздам и отобразить их
        SceneManager.LoadScene("Level" + numberOfLevel + "Episode" + numberOfEpisode);
    }

    void Awake() {
        buttonLevelNumberText.text = numberOfLevel.ToString();
    }

    void Start() {
        levelButton.onClick.AddListener(OpenLevel);
        ChekBonusesStatusForActivateAlerts();

    }

    void ChekBonusesStatusForActivateAlerts() {
        if (GlobalLevelsInfo.GetCountOfUnlockedLevels() > 2) {
            if (!GlobalLevelsInfo.GetFreeMoneyTakenStatus(numberOfLevel)) {
                freeMoneyAlert.SetActive(true);
            }
            if (!GlobalLevelsInfo.GetFreePackTakenStatus(numberOfLevel)) {
                freePackAlert.SetActive(true);
            }
            if (!GlobalLevelsInfo.GetBonusMoneyTakenStatus(numberOfLevel)) {
                bonusMoneyAlert.SetActive(true);
            }
        }
    }

    void TakeOffBlockImage() {
        blockImage.gameObject.SetActive(false);
    }

    public void UnblockButtonAndSetStars() {
        TakeOffBlockImage();
        ActivateButton();
        SetStars();
    }

    void ActivateButton() {
        levelButton.enabled = true;
    }

    void SetStars() {
        var starsCount = GlobalLevelsInfo.GetLevelStars(numberOfLevel);
        starsCountController.ActivateDesiredNumberOfStars(starsCount);
    }
}

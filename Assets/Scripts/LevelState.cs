using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelState : MonoBehaviour {

    [SerializeField] int countOfMoneyBlocks;
    [SerializeField] int countOfMoneyOnLevel;
    [SerializeField] TextMeshProUGUI scoreNumberText;
    [SerializeField] TextMeshProUGUI recordNumberText;
    [SerializeField] GameObject recordTextGO;
    [SerializeField] Slingshot slingshot;
    public float threeStarCondition;
    public float twoStarCondition;
    LevelCanvasController levelCanvasController;
    bool isLevelEnded = false;
    bool isLevelCompleted;
    float score = 0;
    public bool isEndLevelUIIsOpen = false;
    public bool isStickmansScoreIsAdded = false;
    int levelNumber;
    bool levelIsLoose;

    public bool GetLevelStatus() {
        return isLevelEnded;
    }

    public int GetCountOfMoneyOnLevel() {
        return countOfMoneyOnLevel;
    }

    void Start() {
        levelCanvasController = FindObjectOfType<LevelCanvasController>();
        levelNumber = SceneManager.GetActiveScene().buildIndex;

        if (GlobalLevelsInfo.GetLevelCompletedStatus(levelNumber)) {
            recordTextGO.SetActive(true);
            recordNumberText.text = GlobalLevelsInfo.GetLevelRecord(levelNumber).ToString();
        }
    }

    void Update() {
        
        if (!isLevelEnded && countOfMoneyBlocks < 1) {
            isLevelEnded = true;
            StartCoroutine(ScoringPoints());
            if (!isLevelCompleted) {
                // SetNewRecord // НЕ ЗДЕСЬ
                // Сменить флаг
            } else {
                // Сравнить счет и рекорд // я вроде всю этуй хню сделал в ЛевелКанвасе
            }
        }

        if (slingshot.isLeftZeroStickmans && !isLevelEnded && !levelIsLoose) {
            levelIsLoose = true;
            StartCoroutine(OpenLooseScreen());
        }

        scoreNumberText.text = score.ToString();
    }

    IEnumerator ScoringPoints() { // похожу надо переделать // может и нет
        yield return new WaitForSeconds(4);
        levelCanvasController.DeactivateBaseUIExceptScoreAndRecord();
        if (!isEndLevelUIIsOpen) {
            // Destroy(slingshot.gameObject.GetComponent<Collider2D>()); // Блокируем рогатку или можно просто остановить время или вообще похуй // или не похуй - сделать блокировку
            // запускаем анимации очков +2к у стикменов
            AddStickMansPointsToScore();
            StartCoroutine(OpenEndLevelUI());
        }
    }

    IEnumerator OpenEndLevelUI() {
        yield return new WaitForSeconds(2);
        levelCanvasController.OPenWinLevelUI();
    }

    public void AddStickMansPointsToScore() {
        int countOfLeftStickMans = slingshot.countOfLeftStickmans;
        if (countOfLeftStickMans > 0) {
            score += countOfLeftStickMans * 2000;
        }
        isStickmansScoreIsAdded = true;
    }

    IEnumerator OpenLooseScreen() {
        yield return new WaitForSeconds(10);
        Debug.Log("YOU LOOSE");
    }
   
    void DepriveOneMoneyBlock(float bib) { // Переделать систему ивентов я хз
        countOfMoneyBlocks = countOfMoneyBlocks - 1;
    }

    void OnEnable() {
        MoneyBlockController.OnTakingMoney += DepriveOneMoneyBlock;
        MoneyBlockController.OnTakingMoney += IncreaseScore;

        BlocksController.OnBlockDestroy += IncreaseScore;
    }


    void OnDisable() {
        MoneyBlockController.OnTakingMoney -= DepriveOneMoneyBlock;
        MoneyBlockController.OnTakingMoney -= IncreaseScore;

        BlocksController.OnBlockDestroy -= IncreaseScore;
    }

    void IncreaseScore(float numberOfPoints) {
        score += numberOfPoints;
    }

    public float GetScore() {
        return score;
    }
}

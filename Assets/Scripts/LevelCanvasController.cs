using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Runtime.InteropServices;

public class LevelCanvasController : MonoBehaviour {
    [SerializeField] int powerUpsLevelNumberOpenCondition;
    [SerializeField] Button pauseButton;
    [SerializeField] GameObject pauseButtonGO;
    [SerializeField] Button resumeButton;
    [SerializeField] Button openEndLevelUIButton;
    [SerializeField] GameObject openEndLevelUIButtonGO;
    bool isOpenEndLevelUIButtonIsActive = false;
    [SerializeField] GameObject endLevelUI;
    [SerializeField] Button restartLevelButton;
    [SerializeField] GameObject restartLevelButtonGO;
    [SerializeField] GameObject powerUpsGO;
    [SerializeField] GameObject blackoutGO;
    [SerializeField] GameObject pauseGO;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] GameObject scoreGO;
    [SerializeField] GameObject recordGO;
    [SerializeField] GameObject goldStarTwoGO;
    [SerializeField] GameObject goldStarThreeGO;
    [SerializeField] TextMeshProUGUI recordNumberEndScreen;
    [SerializeField] Image recordStarTwo;
    [SerializeField] Image recordStarTrhee;
    [SerializeField] GameObject moneyGO;
    [SerializeField] GameObject starsGO;
    [SerializeField] GameObject moneyBonusGO;
    [SerializeField] Button takeMoneyBonusButton;
    [SerializeField] Button openShop;
    [SerializeField] GameObject shopGo;
    [SerializeField] Button openPowerUpsShop;
    [SerializeField] GameObject powerUpsShop;
    [SerializeField] Button backButton;
    [SerializeField] GameObject shopMoneyPanel;
    [SerializeField] GameObject blockForShop;
    [SerializeField] GameObject moneyShop;
    [SerializeField] Button openMoneyShop;
    Slingshot slingshot;

    [SerializeField] GameObject freePackGO;
    [SerializeField] GameObject freeMoneyGO;
    [SerializeField] GameObject alertForFreePackOnPowerUpButton;
    [SerializeField] GameObject alertForFreeMoneyOnPowerUpButton;
    [SerializeField] GameObject alertForFreeMoneyOnShopButton;
    [SerializeField] GameObject alertForFreePackOnShopButton;
    [SerializeField] GameObject alertOnFreePack;
    [SerializeField] GameObject alertOnFreeMoney;
    [SerializeField] GameObject alertInShopFreeMoney;
    [SerializeField] GameObject alertInShopFreePack;

    [SerializeField] Button buyFreePackButton;
    [SerializeField] Button buyFreeMoneyButton;

    [SerializeField] Button buyFirstMoneyPack;
    [SerializeField] Button buySecondMoneyPack;
    [SerializeField] Button buyThirdMoneyPack;

    [SerializeField] GameObject firstMoneyPackGO;
    [SerializeField] GameObject secondMoneyPackGO;
    [SerializeField] GameObject thirdMoneyPackGO;

    [SerializeField] GameObject giftGO;
    [SerializeField] Button giftButton;
    
    [SerializeField] GameObject blockUIForPause;

    

    
    LevelState levelState;
    int levelNumber;
    int countOfUnlockedLevels;

    [DllImport("__Internal")]
    private static extern void ShowAdvForBonusMoneyExtern(); // Скорее всего переделать в том числе для показа рекламы из мейн меню

    public void TakeMoneyBonusAfterWatchAD() {
        moneyBonusGO.SetActive(false);
        var bonusMoneyCount = levelState.GetCountOfMoneyOnLevel() * 3;
        GlobalLevelsInfo.AddMoney(bonusMoneyCount);
        GlobalLevelsInfo.SetMoneyTakenBonusStatusToTrue(levelNumber);
        GlobalLevelsInfo.SaveData();
    }

    void AddMoneyByAD() {
        ShowAdvForBonusMoneyExtern();
    }
    

    void Start() {
        if (MyObj.isUnauthMode) {
            firstMoneyPackGO.SetActive(false);
            secondMoneyPackGO.SetActive(false);
            thirdMoneyPackGO.SetActive(false);
        }
        

        levelState = FindObjectOfType<LevelState>();
        slingshot = FindObjectOfType<Slingshot>();

        

        pauseButton.onClick.AddListener(OpenPauseMenu);
        resumeButton.onClick.AddListener(ResumeGameAfterPause);
        openEndLevelUIButton.onClick.AddListener(OPenWinLevelUI);
        restartLevelButton.onClick.AddListener(RestartLevel);
        takeMoneyBonusButton.onClick.AddListener(AddMoneyByAD);
        openShop.onClick.AddListener(OpenShop);
        openPowerUpsShop.onClick.AddListener(OpenPowerUpsShop);
        openMoneyShop.onClick.AddListener(OpenMoneyShop);
        backButton.onClick.AddListener(BackActions);

        levelNumber = SceneManager.GetActiveScene().buildIndex;

        countOfUnlockedLevels = GlobalLevelsInfo.GetCountOfUnlockedLevels();

        if (GlobalLevelsInfo.GetCountOfUnlockedLevels() < powerUpsLevelNumberOpenCondition) {
            powerUpsGO.SetActive(false);

        }


        buyFreePackButton.onClick.AddListener(BuyFreePackWithConfirmation);
        buyFreeMoneyButton.onClick.AddListener(BuyFreeMoneyWithConfirmation);
        buyFirstMoneyPack.onClick.AddListener(BuyFirstMoneyPack);
        buySecondMoneyPack.onClick.AddListener(BuySecondMoneyPack);
        buyThirdMoneyPack.onClick.AddListener(BuyThirdMoneyPack);

        if (!GlobalLevelsInfo.GetFreePackTakenStatus(levelNumber)) {
            freePackGO.SetActive(true);
            alertForFreePackOnPowerUpButton.SetActive(true);
            alertForFreePackOnShopButton.SetActive(true);
            alertOnFreePack.SetActive(true);
            alertInShopFreePack.SetActive(true);
        }

        if (!GlobalLevelsInfo.GetFreeMoneyTakenStatus(levelNumber)) {
            freeMoneyGO.SetActive(true);
            alertForFreeMoneyOnPowerUpButton.SetActive(true);
            alertForFreeMoneyOnShopButton.SetActive(true);
            alertOnFreeMoney.SetActive(true);
            alertInShopFreeMoney.SetActive(true);
        }


        CheckGiftConditionsAndStatus();

        MyObj.Instance.ShowIntAdv();
    }

    void CheckGiftConditionsAndStatus() {
        if (levelNumber == 5) {
            if (!GlobalLevelsInfo.GetGiftStatuses().isLevelFiveGiftIsTaken) {
                giftGO.SetActive(true);
            }
        }

        if (levelNumber == 7) {
            if (!GlobalLevelsInfo.GetGiftStatuses().isLevelSevenGiftIsTaken) {
                giftGO.SetActive(true);
            }
        }

        giftButton?.onClick.AddListener(AddGiftMoneyByAdd);
    }

    [DllImport("__Internal")]
    private static extern void ShowAdvForGiftExtern();

    void AddGiftMoneyByAdd() {
        ShowAdvForGiftExtern();
    }

    void TakeGiftMoney() {
        giftGO.SetActive(false);
        GlobalLevelsInfo.AddMoney(10000); // !
        GlobalLevelsInfo.SetGiftStatusToTrue(levelNumber);
        GlobalLevelsInfo.SaveData();
    }


    [DllImport("__Internal")]
    private static extern void BuyFirstMoneyPackExtern();

    void BuyFirstMoneyPack() {
        BuyFirstMoneyPackExtern();
    }

    [DllImport("__Internal")]
    private static extern void BuySecondMoneyPackExtern();

    void BuySecondMoneyPack() {
        BuySecondMoneyPackExtern();
    }

    [DllImport("__Internal")]
    private static extern void BuyThirdMoneyPackExtern();

    void BuyThirdMoneyPack() {
        BuyThirdMoneyPackExtern();
    }



    [DllImport("__Internal")]
    private static extern void ShowAdvForFreePowerUpPackExtern();

    void BuyFreePackWithConfirmation() {
        ShowAdvForFreePowerUpPackExtern();
    }

    public void BuyFreePack() {
        powerUpsShop.GetComponent<PowerUpsShopController>().RandomizePowerUpsFromPackAndAdd(1); // 1
        GlobalLevelsInfo.SetFreePackTakenStatusToTrue(levelNumber);
        GlobalLevelsInfo.SaveData();
        freePackGO.SetActive(false);
        alertForFreePackOnPowerUpButton.SetActive(false);
        alertForFreePackOnShopButton.SetActive(false);
        alertOnFreePack.SetActive(false);
        alertInShopFreePack.SetActive(false);
    }

    [DllImport("__Internal")]
    private static extern void ShowAdvForFreeMoneyExtern(); 

    public void BuyFreeMoneyWithConfirmation() {
        ShowAdvForFreeMoneyExtern();
    }

    [SerializeField] int freeMoneyBonus;

    public void BuyFreeMoney() {
        GlobalLevelsInfo.AddMoney(freeMoneyBonus);
        GlobalLevelsInfo.SetFreeMoneyTakenStatusToTrue(levelNumber);
        GlobalLevelsInfo.SaveData();
        freeMoneyGO.SetActive(false);
        alertForFreeMoneyOnPowerUpButton.SetActive(false);
        alertForFreeMoneyOnShopButton.SetActive(false);
        alertOnFreeMoney.SetActive(false);
        alertInShopFreeMoney.SetActive(false);
    }

    void Update() {
        if (!isOpenEndLevelUIButtonIsActive && levelState.GetLevelStatus()) { // Сделать через ивент
            openEndLevelUIButtonGO.SetActive(true);
            isOpenEndLevelUIButtonIsActive = true;
        }

        // Добавить открытие окончания уровня по времени
    }

    void OpenShop() {
        shopGo.SetActive(true);
        blackoutGO.SetActive(true);
        backButton.gameObject.SetActive(true);
        blockForShop.SetActive(true);
        slingshot.DeactivateSlingshot();
        shopMoneyPanel.SetActive(true);
        
    }

    void OpenPowerUpsShop() {
        powerUpsShop.SetActive(true);
        shopGo.SetActive(false);
    }

    void BackActions() {
        if (shopGo.activeSelf) {
            CloseShop();
        } else if (powerUpsShop.activeSelf) {
            ClosePowerUpsShop();
        } else if (moneyShop.activeSelf) {
            CloseMoneyShop();
        }
    }

    void OpenMoneyShop() {
        moneyShop.SetActive(true);
        shopGo.SetActive(false);
    }

    void CloseMoneyShop() {
        moneyShop.SetActive(false);
        shopGo.SetActive(true);
    }

    void ClosePowerUpsShop() {
        powerUpsShop.SetActive(false);
        shopGo.SetActive(true);
    }

    void CloseShop() {
        shopGo.SetActive(false);
        blackoutGO.SetActive(false);
        backButton.gameObject.SetActive(false);
        shopMoneyPanel.SetActive(false);
        blockForShop.SetActive(false);
        slingshot.ActivateSlingshot();
    }

    void OpenPauseMenu() {
        blackoutGO.SetActive(true);
        pauseGO.SetActive(true);
        moneyGO.SetActive(true);
        starsGO.SetActive(true);
        blockUIForPause.SetActive(true);
        slingshot.DeactivateSlingshot();

        DeactivateBaseUIExceptScoreAndRecord();
    }

    void ResumeGameAfterPause() {
        blackoutGO.SetActive(false);
        pauseGO.SetActive(false);
        moneyGO.SetActive(false);
        starsGO.SetActive(false);
        blockUIForPause.SetActive(false);
        slingshot.ActivateSlingshot();

        ActivateBaseButtons();
    }

    void RestartLevel() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    void CheckUnlockLevelCondition() {
        if (countOfUnlockedLevels > 4) {
            if (levelNumber == countOfUnlockedLevels) {
                BlockNextLevel();
            }
        } else {
            GlobalLevelsInfo.AddLevelToUnlockedLevelsList();
        }
    }

    void BlockNextLevel() {
        isNextLevelBlocked = true;
        levelLock.SetActive(true);
    }

    public void UnlockNextLevel() {
        isNextLevelBlocked = false;
        levelLock.SetActive(false);
        GlobalLevelsInfo.AddLevelToUnlockedLevelsList();
        GlobalLevelsInfo.SaveData(); // Сохраняем что левел открыт, потому что игрок может не стартануть некст левел
    }

    public bool GetLevelBlockStatus() {
        return isNextLevelBlocked;
    }

    bool isNextLevelBlocked;
    [SerializeField] GameObject levelLock;

    public void OPenWinLevelUI() {
        if (levelNumber > 4) {
            MyObj.Instance.RateGame();
        }

        if (!levelState.isStickmansScoreIsAdded) {
            levelState.AddStickMansPointsToScore();
        }

        CheckUnlockLevelCondition();

        float finalScore = levelState.GetScore();

        finalScoreText.text = finalScore.ToString();
        var starsCount = ShowAppropriateNumberOfStars(finalScore);

        endLevelUI.SetActive(true);
        blackoutGO.SetActive(true);
        DeactivateAllBaseUI();
        levelState.isEndLevelUIIsOpen = true;

        moneyGO.SetActive(true);
        starsGO.SetActive(true);

        if (!GlobalLevelsInfo.GetBonusMoneyTakenStatus(levelNumber)) {
            moneyBonusGO.SetActive(true);
        }


        if (!GlobalLevelsInfo.GetLevelCompletedStatus(levelNumber)) {
            GlobalLevelsInfo.AddMoney(levelState.GetCountOfMoneyOnLevel());
            GlobalLevelsInfo.AddStarsMoney(starsCount);
            recordNumberEndScreen.text = finalScore.ToString();


            GlobalLevelsInfo.SetLevelCompletedStatusToTrue(levelNumber);
            ShowRecordStarsByCount(starsCount);
            GlobalLevelsInfo.SetLevelRecord(levelNumber, finalScore);
            GlobalLevelsInfo.SetLevelStars(levelNumber, starsCount);
        } else {
            var oldRecord = GlobalLevelsInfo.GetLevelRecord(levelNumber);
            if (finalScore > oldRecord) {
                GlobalLevelsInfo.SetLevelRecord(levelNumber, finalScore);
            }
            var oldStarsCount = GlobalLevelsInfo.GetLevelStars(levelNumber);
            if (starsCount > oldStarsCount) {
                GlobalLevelsInfo.SetLevelStars(levelNumber, starsCount);
                ShowAppropriateNumberOfStars(starsCount);
                GlobalLevelsInfo.AddStarsMoney(starsCount - oldStarsCount);
            }
            ShowRecordStarsByCount(GlobalLevelsInfo.GetLevelStars(levelNumber));
            recordNumberEndScreen.text = GlobalLevelsInfo.GetLevelRecord(levelNumber).ToString();
            // GlobalLevelsInfo.CheckIsNewScoreAndStarsIsBiggerThanOld(levelNumber, finalScore, starsCount);
            // сделать правильное отображение звёзд и ещё сделать так что бы сначала показылись старое количество а потом новое
            // Баг с новым рекордом - Вроде исправлен?
        }
        
        GlobalLevelsInfo.SaveData();
        MyObj.Instance.SetScoreToLeaderBoard();
    }

    void ShowRecordStarsByCount(int count) {
        if (count == 2) {
            recordStarTwo.enabled = true;
        }
        if (count == 3) {
            recordStarTwo.enabled = true;
            recordStarTrhee.enabled = true;
        }
    }

    IEnumerator Bib() {
        yield return new WaitForSeconds(1); // Это для звёзд?
        // короче доделать ёпты
    }

    int ShowAppropriateNumberOfStars(float score) {
        if (score >= levelState.threeStarCondition) {
            goldStarTwoGO.SetActive(true);
            goldStarThreeGO.SetActive(true);
            return 3;
        }
        if (score >= levelState.twoStarCondition) {
            goldStarTwoGO.SetActive(true);
            return 2;
        }

        return 1;
    }

    public void DeactivateBaseUIExceptScoreAndRecord() {
        pauseButtonGO.SetActive(false);
        restartLevelButtonGO.SetActive(false);
        powerUpsGO.SetActive(false);
        openEndLevelUIButtonGO.SetActive(false);
    }

    void DeactivateAllBaseUI() {
        DeactivateBaseUIExceptScoreAndRecord();
        scoreGO.SetActive(false);
        recordGO.SetActive(false);
    }

    void ActivateBaseButtons() {
        pauseButtonGO.SetActive(true);
        restartLevelButtonGO.SetActive(true);

        if (GlobalLevelsInfo.GetCountOfUnlockedLevels() > powerUpsLevelNumberOpenCondition) {
            powerUpsGO.SetActive(true);
        }
        if (isOpenEndLevelUIButtonIsActive) {
            openEndLevelUIButtonGO.SetActive(true);
        }
    }
}

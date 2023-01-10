using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class GiftRoad : MonoBehaviour {

    GiftRoadInfo giftRoadInfo;

    [SerializeField] GameObject takeFirstGiftButtonGO;
    [SerializeField] GameObject takeSecondGiftButtonGO;
    [SerializeField] GameObject takeThirdGiftButtonGO;

    [SerializeField] GameObject takenFirstGiftText;
    [SerializeField] GameObject takenSecondGiftText;
    [SerializeField] GameObject takenThirdGiftText;


    void Start() {
        takeFirstGiftButtonGO.GetComponent<Button>().onClick.AddListener(TakeFirstGift);
        takeSecondGiftButtonGO.GetComponent<Button>().onClick.AddListener(TakeSecondGift);
        takeThirdGiftButtonGO.GetComponent<Button>().onClick.AddListener(TakeThirdGift);
    }

    void TakeGift(int moneyAmount, GameObject takeGiftButton, GiftRoadState giftRoadState) {
        GlobalLevelsInfo.AddMoney(moneyAmount);
        takeGiftButton.SetActive(false);

        GiftRoadInfo newGiftRoadInfo = new GiftRoadInfo();
        newGiftRoadInfo.giftRoadState = giftRoadState; // Хм
        //giftRoadInfo.takingGiftDate = WorldTimeAPI.Instance.GetCurrentDateTime(); // Время взятия подарка
        newGiftRoadInfo.takingGiftDate = DateTime.UtcNow; // переделать
        GlobalLevelsInfo.SetGiftRoadInfo(newGiftRoadInfo);

        GlobalLevelsInfo.SaveData();
        UpdateGiftRoadState();
    }

    void TakeFirstGift() {
        TakeGift(5000, takeFirstGiftButtonGO, GiftRoadState.First);
    }

    void TakeSecondGift() {
        TakeGift(20000, takenSecondGiftText, GiftRoadState.Second);
    }

    void TakeThirdGift() {
        TakeGift(50000, takenThirdGiftText, GiftRoadState.Third);
    }

    public void UpdateGiftRoadState() {
        giftRoadInfo = GlobalLevelsInfo.GetGiftRoadInfo();
        Debug.Log(giftRoadInfo.takingGiftDate);

        switch (giftRoadInfo.giftRoadState) {
        case GiftRoadState.Zero:
            SetZeroGiftRoadState();
            break;
        case GiftRoadState.First:
            SetFirstGiftRoadState();
            break;
        case GiftRoadState.Second:
            SetSecondGiftRoadState();
            break;
        case GiftRoadState.Third:
            SetThirdGiftRoadState();
            break;
        default:
            Debug.Log("GiftRoad State Eror");
            break;
        }
    }

    void SetZeroGiftRoadState() {
        // не взяты подарки
        // первая кнопка взятия активна
        takeFirstGiftButtonGO.SetActive(true);
    }
    void SetFirstGiftRoadState() {
        Debug.Log("SetFirst");
        // взят первый подарок
        // либо показать таймер второго подарка либо активировать кнопку
        takenFirstGiftText.SetActive(true);

        var elapsedTimeInSeconds = CountOfSecondsFromTakingGift();
        if (elapsedTimeInSeconds > 3600) {
            takeSecondGiftButtonGO.SetActive(true);
            secondGiftTimer.SetActive(false);
        } else {
            secondGiftTimeLeft = 3600 - elapsedTimeInSeconds;
            secondGiftTimerOn = true;
            secondGiftTimer.SetActive(true);
        }
    }
    void SetSecondGiftRoadState() {
        Debug.Log("SetSecond");
        // взят второй подарок
        // либо показать таймер третьего подарка либо активировать кнопку
        takenFirstGiftText.SetActive(true);
        takenSecondGiftText.SetActive(true);

        var elapsedTimeInSeconds = CountOfSecondsFromTakingGift();
        if (elapsedTimeInSeconds > 14400) {
            takeThirdGiftButtonGO.SetActive(true);
            thirdGiftTimer.SetActive(false);
        } else {
            thirdGiftTimeLeft = 14400 - elapsedTimeInSeconds;
            thirdGiftTimerOn = true;
            thirdGiftTimer.SetActive(true);
        }
    }
    void SetThirdGiftRoadState() {
        // взят третий подарок
        // везде написано "получено" вместо кнопок
        takenFirstGiftText.SetActive(true);
        takenSecondGiftText.SetActive(true);
        takenThirdGiftText.SetActive(true);
    }

    
    float secondGiftTimeLeft;
    bool secondGiftTimerOn = false;
    float thirdGiftTimeLeft;
    bool thirdGiftTimerOn = false;
    [SerializeField] GameObject secondGiftTimer;
    [SerializeField] GameObject thirdGiftTimer;

    void Update() {
        if (secondGiftTimerOn) {
            if (secondGiftTimeLeft > 0) {
                secondGiftTimeLeft -= Time.deltaTime;
                updateSecondGIftTimer(secondGiftTimeLeft);
            } else {
                secondGiftTimeLeft = 0;
                secondGiftTimerOn = false;
                UpdateGiftRoadState();
            }
        }

        if (thirdGiftTimerOn) {
            if (secondGiftTimeLeft > 0) {
                thirdGiftTimeLeft -= Time.deltaTime;
                updateThirdGIftTimer(thirdGiftTimeLeft);
            } else {
                thirdGiftTimeLeft = 0;
                thirdGiftTimerOn = false;
                UpdateGiftRoadState();
            }
        }
    }

    void updateSecondGIftTimer(float currentTime) {
        currentTime += 1;
        
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        secondGiftTimer.GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void updateThirdGIftTimer(float currentTime) {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        thirdGiftTimer.GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    int CountOfSecondsFromTakingGift() {
        DateTime epochStart = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // var currentTime = WorldTimeAPI.Instance.GetCurrentDateTime();
        // int currentEpochTime = (int)(currentTime - epochStart).TotalSeconds; апишка лагает

        int currentEpochTime = Current();

        var takingTime = GlobalLevelsInfo.GetGiftRoadInfo().takingGiftDate;
        Debug.Log(takingTime);
        
        int takingEpochTime = (int)(takingTime - epochStart).TotalSeconds;


        return SecondsElapsedNoAbs(currentEpochTime, takingEpochTime);
    }

    public static int Current() {
        // DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); если будет баг то вернуть так
        DateTime epochStart = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        int currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
 
        return currentEpochTime;
    }
 
    public static int SecondsElapsed(int t1) {
        int difference = Current() - t1;
 
        return Mathf.Abs(difference);
    }
 
    public static int SecondsElapsed(int t1, int t2){
        int difference = t1 - t2;
 
        return Mathf.Abs(difference);
    }

    public static int SecondsElapsedNoAbs(int t1, int t2){
        int difference = t1 - t2;
 
        return difference;
    }

}

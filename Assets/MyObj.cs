using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;


public class MyObj : MonoBehaviour {

    [SerializeField] bool isOfflineMode;

    public static MyObj Instance;

    [SerializeField] GameObject AuthGO;

    [DllImport("__Internal")]
    private static extern void SetToLeaderboard(float data);

    [DllImport("__Internal")]
    private static extern void RateGameExtern();

    [DllImport("__Internal")]
    private static extern void ShowIntAdvExtern();

    [DllImport("__Internal")]
    private static extern void LoadDataExtern();

    [DllImport("__Internal")]
    private static extern bool AuthExtern();

    [DllImport("__Internal")]
    private static extern bool CheckPlayerModeExtern();


    bool isSoundOff;

    public static bool isUnauthMode { get; private set; }

    public void CheckPlayerMode() {
        isPlayerModeChecked = CheckPlayerModeExtern();
    }

    bool isPlayerModeChecked;


    public void RateGame() {
        RateGameExtern();
    }

    public void ShowIntAdv() {
        ShowIntAdvExtern();
    }

    // public void LoadData() {
    //     if (!isUnauthMode) {
    //         LoadDataExtern();
    //     } 
    // }

    public void SetUnauthMode() {
        isUnauthMode = true;
    }

    public void SetAuthMode() { // Используется методом из индекса после успешной авторизации - меняет флаг и загружает
        isUnauthMode = false;
        //LoadData(); // Возможно черз корутину // пока что вырубил, вынес в ЧекМод в индекс и убрал ТРУ в конце чекМода
    }

    public void SetPlayerInfo(string value) {
        GlobalLevelsInfo.InitGlobalLevelsInfoIfNotIsInit(); // инит первоначальный для авторизированных

        if (value != "") {
            GlobalLevelsInfo.SetGlobalInfoByLoadedInfo(JsonUtility.FromJson<SerializableList<GlobalLevelState>>(value));
        }

        if (GlobalLevelsInfo.GetCountOfUnlockedLevels() < 1) {
            GlobalLevelsInfo.AddLevelToUnlockedLevelsList();
        }

        OnLoad?.Invoke();
    }

    public void ResetData() {
        GlobalLevelsInfo.ResetData(); // 1
    }

    public void AddMoneyForFirstMoneyPack() {
        GlobalLevelsInfo.AddMoney(20000);
        GlobalLevelsInfo.SaveData();
    }
    public void AddMoneyForSecondMoneyPack() {
        GlobalLevelsInfo.AddMoney(50000);
        GlobalLevelsInfo.SaveData();
    }
    public void AddMoneyForThirdMoneyPack() {
        GlobalLevelsInfo.AddMoney(150000);
        GlobalLevelsInfo.SaveData();
    }

    public bool GetSoundOffStatus() {
        return isSoundOff;
    }

    public void TurnOffSound() {
        isSoundOff = true;
    }

    public void TurnOnSound() {
        isSoundOff = false;
    }

    public void SetScoreToLeaderBoard() {
        if (!isUnauthMode) {
            SetToLeaderboard(GlobalLevelsInfo.GetSumScore());
        }
    }


    private void Awake() {
        if (Instance == null) {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;

            StartCoroutine(CheckPlayerModeAndLoadData());

            GlobalLevelsInfo.InitGlobalLevelsInfoIfNotIsInit();
            if (GlobalLevelsInfo.GetCountOfUnlockedLevels() < 1) {
                GlobalLevelsInfo.AddLevelToUnlockedLevelsList();
            } 
        } else {
            Destroy(gameObject);
        }
    }

    public static bool isReadyToCheckAuth { get; private set; }

    public IEnumerator CheckPlayerModeAndLoadData() { // Может редуцировать
        isChekingAuth = true;
        CheckPlayerMode();
        yield return new WaitUntil(() => isPlayerModeChecked);
        isReadyToCheckAuth = true;
        isChekingAuth = false;
        isPlayerModeChecked = false;
    }

    public static bool isChekingAuth { get; private set; }

    void Start() {

    }

    void Update() {
        
    }

    public delegate void LoadEvent();
    public static event LoadEvent OnLoad;

    public void Auth() { // Используется кнопкой ауфа
        StartCoroutine(AuthAndUpdate());
    }

    IEnumerator AuthAndUpdate() {
        yield return new WaitUntil(AuthExtern);
        if (!isUnauthMode) {
            OnLoad?.Invoke();
        }
    }
}

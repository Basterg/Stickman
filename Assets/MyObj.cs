using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


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
    private static extern void AuthExtern();

    [DllImport("__Internal")]
    private static extern bool CheckPlayerModeExtern();


    bool isSoundOff;

    public static bool isUnauthMode { get; private set; }

    public bool CheckPlayerMode() {
        return CheckPlayerModeExtern();
    }


    public void RateGame() {
        RateGameExtern();
    }

    public void ShowIntAdv() {
        ShowIntAdvExtern();
    }

    public void LoadData() {
        if (!isUnauthMode) {
            LoadDataExtern();
        } 
    }

    public void SetUnauthMode() {
        isUnauthMode = true;
    }

    public void SetAuthMode() { // Используется методом из индекса после успешной авторизации - меняет флаг и загружает
        isUnauthMode = false;
        LoadData(); // Возможно черз корутину
    }

    public void SetPlayerInfo(string value) {
        GlobalLevelsInfo.InitGlobalLevelsInfoIfNotIsInit();

        if (value != null) {
            GlobalLevelsInfo.SetGlobalInfoByLoadedInfo(JsonUtility.FromJson<SerializableList<GlobalLevelState>>(value));
        }
    }

    public void AddMoneyForFirstMoneyPack() {
        GlobalLevelsInfo.AddMoney(20000);
    }
    public void AddMoneyForSecondMoneyPack() {
        GlobalLevelsInfo.AddMoney(50000);
    }
    public void AddMoneyForThirdMoneyPack() {
        GlobalLevelsInfo.AddMoney(150000);
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
        SetToLeaderboard(GlobalLevelsInfo.GetSumScore());
    }


    private void Awake() {
        if (Instance == null) {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
            GlobalLevelsInfo.InitGlobalLevelsInfoIfNotIsInit(); // инит первоначальный
            StartCoroutine(CheckPlayerModeAndLoadData()); 
            
        } else {
            Destroy(gameObject);
        }
    }

    IEnumerator CheckPlayerModeAndLoadData() {
        yield return new WaitUntil(CheckPlayerMode);
        LoadData();
    }

    public void ResetData() {
        GlobalLevelsInfo.ResetData();
    }

    void Start() {
        
    }

    void Update() {
        
    }

    public void Auth() { // Используется кнопкой ауфа
        AuthExtern();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;


public class MyObj : MonoBehaviour {

    public static MyObj Instance;

    [DllImport("__Internal")]
    private static extern void SetToLeaderboard(float data);

    [DllImport("__Internal")]
    private static extern void RateGameExtern();

    [DllImport("__Internal")]
    private static extern void ShowIntAdvExtern();


    bool isSoundOff;

    public void RateGame() {
        RateGameExtern();
    }

    public void ShowIntAdv() {
        ShowIntAdvExtern();
    }

    public void SetPlayerInfo(string value) {
        GlobalLevelsInfo.InitGlobalLevelsInfoIfNotIsInit();

        if (value != null) {
            GlobalLevelsInfo.SetGlobalInfoByLoadedInfo(JsonUtility.FromJson<SerializableList<GlobalLevelState>>(value));
        }

        if (GlobalLevelsInfo.GetCountOfUnlockedLevels() < 1) { // для не авториззованных
            GlobalLevelsInfo.AddLevelToUnlockedLevelsList();
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


    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
            
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetData() {
        GlobalLevelsInfo.ResetData();
        
    }
}

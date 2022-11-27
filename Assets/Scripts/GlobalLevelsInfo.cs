using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public static class GlobalLevelsInfo {

    static SerializableList<GlobalLevelState> globalInfo;
    static bool isInit;

    [DllImport("__Internal")]
    private static extern void SaveDataExtern(string data);


    // // void Awake() {
    // //     GlobalLevelsInfo.LoadExtern();
    // // }

    // public static void Load() {
    //     LoadExtern();
    // }

    public static PowerUpsCount GetPowerUpsCount() {
        return globalInfo.powerUpsCount;
    }
    
    public static void SaveData() {
        if (!MyObj.isUnauthMode) {
            string jsonStringLevels = JsonUtility.ToJson(globalInfo);
            SaveDataExtern(jsonStringLevels);
        }
    }

    public static float GetSumScore() {
        float sumScore = 0;
        foreach (var levelState in globalInfo.listOfLevelStates) {
            sumScore += levelState.record;
        }
        return sumScore;
    }

    public static int GetMoneyCount() {
        return globalInfo.money;
    }
    public static int GetStarsMoneyCount() {
        return globalInfo.starsMoney;
    }

    public static void SetMoneyCount(int count) {
        globalInfo.money = count;
    }

    public static void AddMoney(int count) {
        globalInfo.money += count;
    }

    public static void SpendMoney(int count) {
        globalInfo.money -= count;
    }

    public static void SetStarsMoneyCount(int count) {
        globalInfo.starsMoney = count;
    }

    public static void AddStarsMoney(int count) {
        globalInfo.starsMoney += count;
    }

    public static void SpendStarsMoney(int count) {
        globalInfo.starsMoney -= count;
    }

    public static void AddDesiredCountOfHulkPowerUp(int count) {
        globalInfo.powerUpsCount.hulkPowerUpCount += count;
    }
    public static void AddDesiredCountOfBoxerPowerUp(int count) {
        globalInfo.powerUpsCount.boxerPowerUpCount += count;
    }
    public static void AddDesiredCountOfEarthquakePowerUp(int count) {
        globalInfo.powerUpsCount.earthquakePowerUpCount += count;
    }
    public static void AddDesiredCountOfSlingshotPowerUp(int count) {
        globalInfo.powerUpsCount.slingshotPowerUpCount += count;
    }
    public static void AddDesiredCountOfBombermanPowerUp(int count) {
        globalInfo.powerUpsCount.bombermanPowerUpCount += count;
    }


    public static void SpendHulkPowerUp() {
        globalInfo.powerUpsCount.hulkPowerUpCount--;
    }

    public static void SpendBoxerPowerUp() {
        globalInfo.powerUpsCount.boxerPowerUpCount--;
    }

    public static void SpendEarthquakePowerUp() {
        globalInfo.powerUpsCount.earthquakePowerUpCount--;
    }

    public static void SpendSlingshotPowerUp() {
        globalInfo.powerUpsCount.slingshotPowerUpCount--;
    }

    public static void SpendBombermanPowerUp() {
        globalInfo.powerUpsCount.bombermanPowerUpCount--;
    }
        

    public static int GetAllStarsCount() {
        int allStarsCount = 0;
        foreach (var levelState in globalInfo.listOfLevelStates) {
            allStarsCount += levelState.starsCount;
        }

        return allStarsCount;
    }


    public static List<GlobalLevelState> GetLevelsList() {
        return globalInfo.listOfLevelStates;
    }

    public static SerializableList<GlobalLevelState> GetInfo() {
        return globalInfo;
    }

    public static void InitGlobalLevelsInfoIfNotIsInit() {
        if (!isInit) {
            globalInfo = new SerializableList<GlobalLevelState>();
            globalInfo.listOfLevelStates = new List<GlobalLevelState>();
            globalInfo.powerUpsCount = new PowerUpsCount();
            AddLevelToUnlockedLevelsList();
            isInit = true;
        }
    }

    public static void ResetData() {
        isInit = false;
        InitGlobalLevelsInfoIfNotIsInit();
        SaveData();
    }

    static bool GetInitStatus() {
        return isInit;
    }

    public static void SetGlobalInfoByLoadedInfo(SerializableList<GlobalLevelState> loadedSerializableList) {
        globalInfo = loadedSerializableList;
    }

    public static int GetCountOfUnlockedLevels() {
        return globalInfo.listOfLevelStates.Count;
    }

    public static GlobalLevelState GetDesiredLevelGlobalState(int levelNumber) {
        return globalInfo.listOfLevelStates[levelNumber - 1];
    }

    public static void AddLevelToUnlockedLevelsList() { // Возможно не нужно
        globalInfo.listOfLevelStates.Add(new GlobalLevelState());
    }

    public static void SetLevelCompletedStatusToTrue(int levelNumber) {
        globalInfo.listOfLevelStates[levelNumber - 1].isLevelCompleted = true;
    }

    public static void SetLevelRecord(int levelNumber, float newRecord) {
        globalInfo.listOfLevelStates[levelNumber - 1].record = newRecord;
    }

    public static void SetLevelStars(int levelNumber, int starsCount) {
        globalInfo.listOfLevelStates[levelNumber - 1].starsCount = starsCount;
    }

    public static bool GetLevelCompletedStatus(int levelNumber) {
        return globalInfo.listOfLevelStates[levelNumber - 1].isLevelCompleted;
    }

    public static float GetLevelRecord(int levelNumber) {
        return globalInfo.listOfLevelStates[levelNumber - 1].record;
    }

    public static int GetLevelStars(int levelNumber) {
        return globalInfo.listOfLevelStates[levelNumber - 1].starsCount;
    }

    public static bool GetBonusMoneyTakenStatus(int levelNumber) {
        return globalInfo.listOfLevelStates[levelNumber -  1].isBonusMoneyTaken;
    }

    public static void SetMoneyTakenBonusStatusToTrue(int levelNumber) {
        globalInfo.listOfLevelStates[levelNumber - 1].isBonusMoneyTaken = true;
    }

    public static bool GetFreePackTakenStatus(int levelNumber) {
        return globalInfo.listOfLevelStates[levelNumber -  1].isFreePackTaken;
    }

    public static void SetFreePackTakenStatusToTrue(int levelNumber) {
        globalInfo.listOfLevelStates[levelNumber - 1].isFreePackTaken = true;
    }

    public static bool GetFreeMoneyTakenStatus(int levelNumber) {
        return globalInfo.listOfLevelStates[levelNumber -  1].isFreeMoneyTaken;
    }

    public static void SetFreeMoneyTakenStatusToTrue(int levelNumber) {
        globalInfo.listOfLevelStates[levelNumber - 1].isFreeMoneyTaken = true;
    }

    // public static void CheckIsNewScoreAndStarsIsBiggerThanOld(int levelNumber, float score, int stars) { // Разделить для отображения звёзд
    //     if (nestedListOfUnlockedLevels.list[levelNumber - 1].record < score) {
    //         SetLevelRecord(levelNumber, score);
    //     }

    //     if (nestedListOfUnlockedLevels.list[levelNumber - 1].starsCount < stars) {
    //         SetLevelRecord(levelNumber, stars);
    //     }
    // }
}

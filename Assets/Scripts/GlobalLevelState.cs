using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GlobalLevelState {
    public bool isLevelCompleted;
    public float record;
    public int starsCount;
    public bool isBonusMoneyTaken;
    public bool isFreePackTaken;
    public bool isFreeMoneyTaken;

}

 [System.Serializable]
 public class SerializableList<T> {
    public List<T> listOfLevelStates;
    public int money;
    public int starsMoney;
    public PowerUpsCount powerUpsCount;
    public GiftStatuses giftStatuses;
    public GiftRoadInfo giftRoadInfo;
}

[System.Serializable]
public class PowerUpsCount {
    public int hulkPowerUpCount = 0;
    public int boxerPowerUpCount = 0;
    public int earthquakePowerUpCount = 0;
    public int slingshotPowerUpCount = 0;
    public int bombermanPowerUpCount = 0;
}


[System.Serializable]
public class GiftStatuses {
    public bool isLevelFiveGiftIsTaken = false;
    public bool isBomberGiftIsTaken = false;
    public bool isLevelSevenGiftIsTaken = false;
}

[System.Serializable]
public class GiftRoadInfo {
    public GiftRoadState giftRoadState = GiftRoadState.Zero;
    public DateTime takingGiftDate = new DateTime(2019, 1, 1, 0, 0, 0, DateTimeKind.Utc);
}

[System.Serializable]
public enum GiftRoadState {
    Zero,
    First,
    Second,
    Third
}
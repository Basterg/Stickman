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
}

[System.Serializable]
public class PowerUpsCount {
    public int hulkPowerUpCount = 0;
    public int boxerPowerUpCount = 0;
    public int earthquakePowerUpCount = 0;
    public int slingshotPowerUpCount = 0;
    public int bombermanPowerUpCount = 0;
}

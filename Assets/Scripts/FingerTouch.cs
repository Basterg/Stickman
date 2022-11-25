using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerTouch : MonoBehaviour {
    [SerializeField] GameObject fingerUp;
    [SerializeField] GameObject fingerDown;

    bool isFingerUpIsActive;
    bool isFingerDownIsActive;
    [SerializeField] float time;
    [SerializeField] bool flag = true;

    int countOfChanges = 0;

    public void StartFingerTouchAnimation() {
        StartCoroutine(changeFingerWaiter());
    }

    void Update() {
        if (countOfChanges == 4) {
            flag = false;
        }
    }

    IEnumerator changeFingerWaiter() {

    var wait = new WaitForSeconds(time);

        while(flag) {
            ChangeActiveFinger();
            yield return wait;
        }

        if (!flag) {
            fingerDown.gameObject.SetActive(false);
            isFingerDownIsActive = false;
            fingerUp.gameObject.SetActive(false);
            isFingerUpIsActive = false;
        }

    }

    
    void ChangeActiveFinger() {
        if (isFingerDownIsActive) {
            fingerDown.gameObject.SetActive(false);
            isFingerDownIsActive = false;

            fingerUp.gameObject.SetActive(true);
            isFingerUpIsActive = true;

            countOfChanges++;
            return;
        }

        if (isFingerUpIsActive) {
            fingerUp.gameObject.SetActive(false);
            isFingerUpIsActive = false;

            fingerDown.gameObject.SetActive(true);
            isFingerDownIsActive = true;

            countOfChanges++;
            return;
        }

        if (!isFingerDownIsActive && !isFingerUpIsActive) {
            fingerUp.gameObject.SetActive(true);
            isFingerUpIsActive = true;
        }
        
    }
}

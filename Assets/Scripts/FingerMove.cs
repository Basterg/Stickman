using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerMove : MonoBehaviour {
    [SerializeField] GameObject finger;
    [SerializeField] float step;
    Vector3 targetToMove;
    Vector3 fingerStartPosition;
    bool isTimeToStart = false;

    int countOfCycles;

    IEnumerator Start() {
        fingerStartPosition = finger.transform.position;
        targetToMove = new Vector3(fingerStartPosition.x - 4, fingerStartPosition.y - 1, 0);

        yield return new WaitForSeconds(1);
        finger.SetActive(true);
        isTimeToStart = true;

    }


    private void FixedUpdate() {
        if (isTimeToStart) {
            if (countOfCycles < 3) {
            finger.transform.position = Vector3.MoveTowards(finger.transform.position, targetToMove, step);

            if (finger.transform.position == targetToMove) {
                finger.transform.position = fingerStartPosition;
                countOfCycles++;
            }
        } else {
            Destroy(gameObject);
            }
        }
    }
}

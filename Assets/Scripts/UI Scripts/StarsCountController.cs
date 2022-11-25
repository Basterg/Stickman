using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsCountController : MonoBehaviour {
    [SerializeField] Image goldStartOne;
    [SerializeField] Image goldStartTwo;
    [SerializeField] Image goldStartThree;

    void ActivateFirstStar() {
        goldStartOne.gameObject.SetActive(true);
    }
    void ActivateSecondStar() {
        goldStartTwo.gameObject.SetActive(true);
    }
    void ActivateThirdStar() {
        goldStartThree.gameObject.SetActive(true);
    }

    void ActivateAllStars() {
        ActivateFirstStar();
        ActivateSecondStar();
        ActivateThirdStar();
    }

    void ActivateTwoStars() {
        ActivateFirstStar();
        ActivateSecondStar();
    }

    public void ActivateDesiredNumberOfStars(int starsCount) {
        if (starsCount == 1) {
            ActivateFirstStar();
        }
        if (starsCount == 2) {
            ActivateTwoStars();
        }
        if (starsCount == 3) {
            ActivateAllStars();
        }
    }
}

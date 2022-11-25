using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasheFollow : MonoBehaviour {

    float xPosition;
    float yPosition;
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;
    


    void Start() {
        xPosition = gameObject.transform.position.x;
        xPosition = gameObject.transform.position.y;
    }
    void FixedUpdate() {
        xPosition = xPosition + xOffset;
        yPosition = yPosition + yOffset;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPunchAnimation : MonoBehaviour {
    [SerializeField] GameObject blackSmokePS;

    void OnCollisionEnter2D(Collision2D other) {
        Instantiate(blackSmokePS, transform.position, Quaternion.identity);
    }
}

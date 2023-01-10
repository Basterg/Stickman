using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSContainer : MonoBehaviour {
    bool isStarted;
    ParticleSystem _particleSystem;

    void Awake() {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    void Start() {
        isStarted = true;
    }

    
    void FixedUpdate() {
        if (!_particleSystem.IsAlive() && isStarted) {
            Destroy(gameObject);
        }
    }
}

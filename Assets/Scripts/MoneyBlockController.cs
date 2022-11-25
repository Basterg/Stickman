using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBlockController : MonoBehaviour {

    [SerializeField] int amountOfCash;

    [SerializeField] GameObject blockDestroyingParticleSystem;
    [SerializeField] GameObject destroyingPointsParticleSystem;

    [SerializeField] AudioSource breakAudio;

    public delegate void MoneyTakeEvent(float amountOfCash);
    public static event MoneyTakeEvent OnTakingMoney;


    bool isMoneyTouchedByStickman = false;

    void OnCollisionEnter2D(Collision2D collision) {
        if (!isMoneyTouchedByStickman && collision.gameObject.tag == "Stickman") {
            isMoneyTouchedByStickman = true;
            StartDestroyMoneyBlockActions();
        }
    }

    public void StartDestroyMoneyBlockActions() {
        if (breakAudio != null) {
            breakAudio.Play();
        }
        if (blockDestroyingParticleSystem != null) {
            Instantiate(blockDestroyingParticleSystem, transform.position, Quaternion.identity);
        }

        if (destroyingPointsParticleSystem != null) {
            Instantiate(destroyingPointsParticleSystem, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
        }

        gameObject.transform.DetachChildren();

        OnTakingMoney?.Invoke(1000);

        Destroy(gameObject);
    }
}

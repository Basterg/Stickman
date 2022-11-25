using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    [SerializeField] float fieldOfImpact;
    [SerializeField] float force;

    [SerializeField] LayerMask layerToHit;

    [SerializeField] GameObject explosionPS;
    [SerializeField] AudioSource explosionAudio;

    void Update() {
        
    }

    public void Explode() {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, layerToHit);
        gameObject.GetComponent<StickmanPartController>().stickmanType = Slingshot.StickmanType.Bomb;

        foreach (Collider2D obj in objects) {
            if (obj.gameObject.GetComponent<MoneyBlockController>()) {
                obj.gameObject.GetComponent<MoneyBlockController>().StartDestroyMoneyBlockActions();
            }

            if (obj.gameObject.GetComponent<BlocksController>()) {
                obj.GetComponent<BlocksController>().SubtractHP(200); // ы
            }
            Vector2 direction = obj.transform.position -transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
        }

        GameObject ExplosionsEffectIns = Instantiate(explosionPS, transform.position, Quaternion.identity);
        explosionAudio.Play();
        
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, fieldOfImpact);
    }
}

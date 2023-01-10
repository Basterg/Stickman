using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksController : MonoBehaviour {

    Collider2D blockCollider;
    
    [SerializeField] float blockHP = 100;
    float startBlockHP;
    [SerializeField] GameObject blockDestroyingParticleSystem;
    [SerializeField] GameObject innerSmokeBigParticleSystem;
    [SerializeField] GameObject smokeSmallParticleSystem;
    [SerializeField] GameObject destroyingPointsParticleSystem;

    [SerializeField] AudioContainer breakAudioContainer;
    [SerializeField] AudioContainer hitAudioContainer;

    public enum BlockType {
        Wood,
        Glass,
        Stone,
        Tablet
    }

    public BlockType blockType;

    bool isDestroyed = false;

     public delegate void BlockDestroyEvent(float numberOfPoints);
    public static event BlockDestroyEvent OnBlockDestroy;

    void Awake() {
        blockCollider = gameObject.GetComponent<Collider2D>();
        startBlockHP = blockHP;
    }

    void Start() {
        
    }
    
    void Update() {
        if (blockHP < 0 && !isDestroyed) { // Возможно убрать
                isDestroyed = true;
                StartDestroyBlockActions();
            }
    }

    public void SubtractHP(int damage) {
        blockHP -= damage;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (!isDestroyed) { // Хз как обработать - пока так
            float takenDamage = 0;
            takenDamage += collision.relativeVelocity.magnitude;

            if (collision.gameObject.tag == "Stickman") {
                takenDamage = takenDamage * 2;

                if (collision.gameObject.GetComponent<StickmanPartController>().stickmanType == Slingshot.StickmanType.Hulk) {
                    takenDamage = takenDamage * 10; 
                }

                if (blockType == BlockType.Stone && collision.gameObject.GetComponent<StickmanPartController>().stickmanType == Slingshot.StickmanType.StoneBreaker) {
                    takenDamage = takenDamage * 20;
                }
            } else {
                if (collision.relativeVelocity.magnitude > 10) {
                    if (hitAudioContainer != null) {
                        hitAudioContainer.PlayAudio();
                    }
                }
            }

            blockHP -= takenDamage;

            if (blockHP < 0 && !isDestroyed) {
                isDestroyed = true;
                StartDestroyBlockActions();

            }

            if (takenDamage > 40 && !isDestroyed) {
                if (smokeSmallParticleSystem != null) {
                    Instantiate(smokeSmallParticleSystem, transform.position, Quaternion.identity);
                }
            }

            // if (collision.gameObject.tag == "Stickman") {
            //     Debug.Log("Magnitude with Stickman: " + collision.relativeVelocity.magnitude);
            // }

            // if (collision.gameObject.tag == "Floor") {
            //     Debug.Log("Magnitude with Floor: " + collision.relativeVelocity.magnitude);
            // }

            // if (collision.gameObject.tag == "Block") {
            //     Debug.Log("Magnitude with Block: " + collision.relativeVelocity.magnitude);
            // }
        }
    }

    void StartDestroyBlockActions() {
        if (breakAudioContainer != null) {
            breakAudioContainer.PlayAudioAndDestroyContainerAfter();
            hitAudioContainer.DestroyAudioContainer();
        }
        if (blockDestroyingParticleSystem != null) {
            Instantiate(blockDestroyingParticleSystem, transform.position, Quaternion.identity);
        }

        if (destroyingPointsParticleSystem != null) {
            Instantiate(destroyingPointsParticleSystem, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
        }

        if (innerSmokeBigParticleSystem != null) {
            //Instantiate(smokeBigParticleSystem, transform.position, Quaternion.identity);
            innerSmokeBigParticleSystem.SetActive(true);
            gameObject.transform.DetachChildren();
        }

        OnBlockDestroy?.Invoke(startBlockHP);

        Destroy(gameObject);
    }
}

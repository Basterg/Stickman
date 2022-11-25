using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanPartController : MonoBehaviour {

    public Slingshot.StickmanType stickmanType;
    [SerializeField] Sprite greenSkin;
    [SerializeField] StickmanController stickmanController;
    [SerializeField] AudioSource boxGlovesHit;
    

    public void BecomeGreen() {
        if (greenSkin != null) {
            gameObject.GetComponent<SpriteRenderer>().sprite = greenSkin;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (stickmanController.isShooted && !stickmanController.isStoped) {
            if (collision.gameObject.tag == "Block" || collision.gameObject.tag == "Floor") {
                stickmanController.isStoped = true;
            }
        }

        if (stickmanController.isShooted && stickmanType == Slingshot.StickmanType.Boxer) {
            if (boxGlovesHit != null) {
                boxGlovesHit.Play();
            }
        }
    }
}

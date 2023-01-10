using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class SpriteFromAtlas : MonoBehaviour {

    [SerializeField] SpriteAtlas spriteAtlas;
    [SerializeField] string spriteName;
    [SerializeField] bool isForUI;
    
    void Start() {
        if (isForUI) {
            GetComponent<Image>().sprite = spriteAtlas.GetSprite(spriteName);
        } else {
            GetComponent<SpriteRenderer>().sprite = spriteAtlas.GetSprite(spriteName);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class PowerUpController : MonoBehaviour {
    [SerializeField] Image blockImage;
    [SerializeField] Image powerUpImage;
    [SerializeField] Button powerUpButton;
    bool isBlocked;
    
    void Start() {
        
    }

    
    void Update() {
        
    }

    public void CheckBlockStatusByCount(int powerUpCount) {
        if (powerUpCount < 1 && !isBlocked) {
            BlockPowerUp(); 
        }

        if (powerUpCount > 0 && isBlocked) {
            UnblockPoweUp();
        }
    }

    void BlockPowerUp() {
        blockImage.enabled = true;
        powerUpImage.enabled = false;
        powerUpButton.enabled = false;
        isBlocked = true;
    }

    void UnblockPoweUp() {
        blockImage.enabled = false;
        powerUpImage.enabled = true;
        powerUpButton.enabled = true;
        isBlocked = false;
    }
}

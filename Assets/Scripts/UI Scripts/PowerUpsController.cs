using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpsController : MonoBehaviour {
    Slingshot slingshot;
    [SerializeField] TextMeshProUGUI hulkPowerUpCount;
    [SerializeField] TextMeshProUGUI boxerPowerUpCount;
    [SerializeField] TextMeshProUGUI earthquakePowerUpCount;
    [SerializeField] TextMeshProUGUI slingshotPowerUpCount;
    [SerializeField] TextMeshProUGUI bombermanPowerUpCount;

    [SerializeField] PowerUpController hulkPoweUpController;
    [SerializeField] PowerUpController boxerPoweUpController;
    [SerializeField] PowerUpController earthquakePoweUpController;
    [SerializeField] PowerUpController slingshotPoweUpController;
    [SerializeField] PowerUpController bombermanPoweUpController;

    [SerializeField] Button hulkPowerUpButton;
    [SerializeField] Button boxerPowerUpButton;
    [SerializeField] Button earthquakePowerUpButton;
    [SerializeField] Button slingshotPowerUpButton;
    [SerializeField] Button bombermanPowerUpButton;

    PowerUpsCount powerUpsCount;
    FloorShake floorShake;
    
    void Start() {
        floorShake = FindObjectOfType<FloorShake>();
        slingshot = FindObjectOfType<Slingshot>();

        hulkPowerUpButton.onClick.AddListener(ACtivateHulkPowerUp);
        boxerPowerUpButton.onClick.AddListener(ActivateBoxerPowerUp);
        earthquakePowerUpButton.onClick.AddListener(ACtivateEarthquakePowerUp);
        slingshotPowerUpButton.onClick.AddListener(ACtivateSlingshotPowerUp);
        bombermanPowerUpButton.onClick.AddListener(ActivateBombermanPowerUp);
    }

    void Update() {
        // получать инфу о количество бонуса и отображать в панели текста
        // Скорее всего нужно сделать через ивенты, но посмотрим на производительность
        powerUpsCount = GlobalLevelsInfo.GetPowerUpsCount();

        hulkPoweUpController.CheckBlockStatusByCount(powerUpsCount.hulkPowerUpCount);
        boxerPoweUpController.CheckBlockStatusByCount(powerUpsCount.boxerPowerUpCount);
        earthquakePoweUpController.CheckBlockStatusByCount(powerUpsCount.earthquakePowerUpCount);
        slingshotPoweUpController.CheckBlockStatusByCount(powerUpsCount.slingshotPowerUpCount);
        bombermanPoweUpController.CheckBlockStatusByCount(powerUpsCount.bombermanPowerUpCount);

        hulkPowerUpCount.text = powerUpsCount.hulkPowerUpCount.ToString();
        boxerPowerUpCount.text = powerUpsCount.boxerPowerUpCount.ToString();
        earthquakePowerUpCount.text = powerUpsCount.earthquakePowerUpCount.ToString();
        slingshotPowerUpCount.text = powerUpsCount.slingshotPowerUpCount.ToString();
        bombermanPowerUpCount.text = powerUpsCount.bombermanPowerUpCount.ToString();
    }

    void ACtivateHulkPowerUp() {
        // Активация скорее всего через ивент
        if (powerUpsCount.hulkPowerUpCount > 0) {
            if (slingshot.ActivateHulkPowerUp()) {
                GlobalLevelsInfo.SpendHulkPowerUp();
            }
        }
        
    }

    void ActivateBoxerPowerUp() {
        if (powerUpsCount.boxerPowerUpCount > 0) {
            if (slingshot.InstantiateAndPlaceBoxerInSlingshot()) {
                GlobalLevelsInfo.SpendBoxerPowerUp();
            }
        }
    }

    void ACtivateEarthquakePowerUp() {
        if (powerUpsCount.earthquakePowerUpCount > 0) {
            StartCoroutine(floorShake.ActivateEarthquakePowerUp());
            GlobalLevelsInfo.SpendEarthquakePowerUp();
        }
    }

    void ACtivateSlingshotPowerUp() {
        if (powerUpsCount.slingshotPowerUpCount > 0) {
            if (slingshot.ActivateSlingshotPowerUp()) {
                GlobalLevelsInfo.SpendSlingshotPowerUp();
            }
        }
    }

    void ActivateBombermanPowerUp() {
        if (powerUpsCount.bombermanPowerUpCount > 0) {
            if (slingshot.InstantiateAndPlaceBombermanInSlingshot()) {
                GlobalLevelsInfo.SpendBombermanPowerUp();
            }
        }
    }
}

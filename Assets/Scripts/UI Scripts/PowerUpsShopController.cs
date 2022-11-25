using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class PowerUpsShopController : MonoBehaviour {
    [SerializeField] int secondPackCost;
    [SerializeField] int thirdPackCost;
    [SerializeField] int fourthPackCost;
    [SerializeField] int commonPackCost;

    [SerializeField] int secondPackNumberOfPoweUps;
    [SerializeField] int thirdPackNumberOfPoweUps;
    [SerializeField] int fourthPackNumberOfPoweUps;
    [SerializeField] int hulkPackNumberOfPoweUps;
    [SerializeField] int boxerPackNumberOfPoweUps;
    [SerializeField] int earthquakePackNumberOfPoweUps;
    [SerializeField] int slingshotPackNumberOfPoweUps;
    [SerializeField] int bombermanPackNumberOfPoweUps;
    

    [SerializeField] Button chestTabButton;
    [SerializeField] Button hulkTabButton;
    [SerializeField] Button boxerTabButton;
    [SerializeField] Button earthquakeTabButton;
    [SerializeField] Button slingshotTabButton;
    [SerializeField] Button bombermanTabButton;

    [SerializeField] GameObject chestTab;
    [SerializeField] GameObject hulkTab;
    [SerializeField] GameObject boxerTab;
    [SerializeField] GameObject earthquakeTab;
    [SerializeField] GameObject slingshotTab;
    [SerializeField] GameObject bombermanTab;

    [SerializeField] Image chestBackground;
    [SerializeField] Image hulkBackground;
    [SerializeField] Image boxerBackground;
    [SerializeField] Image earthquakeBackground;
    [SerializeField] Image slingshotBackground;
    [SerializeField] Image bombermanBackground;

    [SerializeField] GameObject chestTabletText;
    [SerializeField] GameObject hulkTabletText;
    [SerializeField] GameObject boxerTabletText;
    [SerializeField] GameObject earthquakeTabletText;
    [SerializeField] GameObject slingshotTabletText;
    [SerializeField] GameObject bombermanTabletText;

    
    [SerializeField] Button buySecondPackButton;
    [SerializeField] Button buyThirdPackButton;
    [SerializeField] Button buyFourthPackButton;
    [SerializeField] Button buyHulkPackButton;
    [SerializeField] Button buyBoxerPackButton;
    [SerializeField] Button buyEathquakePackButton;
    [SerializeField] Button buySlingshotPackButton;
    [SerializeField] Button buyBombermanPackButton;

    [SerializeField] GameObject confirmationWindow;
    [SerializeField] Button yesButtonOfConfirmationWindow;
    [SerializeField] Button noButtonOfConfirmationWindow;
    [SerializeField] GameObject blockerUIForConfirmationWindow;
    [SerializeField] GameObject moneyWarningWindow;
    [SerializeField] Button okButtonOfMoneyWarningWindow;

    PowerUpsCount powerUpsCount;
    [SerializeField] TextMeshProUGUI hulkPowerUpCount;
    [SerializeField] TextMeshProUGUI boxerPowerUpCount;
    [SerializeField] TextMeshProUGUI earthquakePowerUpCount;
    [SerializeField] TextMeshProUGUI slingshotPowerUpCount;
    [SerializeField] TextMeshProUGUI bombermanPowerUpCount;

    

    int levelNumber;


    
    void Start() {
        levelNumber = SceneManager.GetActiveScene().buildIndex;

        chestTabButton.onClick.AddListener(OpenChestTab);
        hulkTabButton.onClick.AddListener(OpenHulkTab);
        boxerTabButton.onClick.AddListener(OpenBoxerTab);
        earthquakeTabButton.onClick.AddListener(OpenEarthquakeTab);
        slingshotTabButton.onClick.AddListener(OpenSlingshotTab);
        bombermanTabButton.onClick.AddListener(OpenBombermanTab);

        noButtonOfConfirmationWindow.onClick.AddListener(CloseConfirmationWindow);
        okButtonOfMoneyWarningWindow.onClick.AddListener(CloseMoneyWarningWindow);


        buySecondPackButton.onClick.AddListener(BuySecondPackWithConfirmation);
        buyThirdPackButton.onClick.AddListener(BuyThirdPackkWithConfirmation);
        buyFourthPackButton.onClick.AddListener(BuyFourthPackkWithConfirmation);
        buyHulkPackButton.onClick.AddListener(BuyHulkPackkWithConfirmation);
        buyBoxerPackButton.onClick.AddListener(BuyBoxerPackkWithConfirmation);
        buyEathquakePackButton.onClick.AddListener(BuyEarthquakePackkWithConfirmation);
        buySlingshotPackButton.onClick.AddListener(BuySlingshotPackkWithConfirmation);
        buyBombermanPackButton.onClick.AddListener(BuyBombermanPackkWithConfirmation);

        OpenChestTab();
    }

    void Update() {
        powerUpsCount = GlobalLevelsInfo.GetPowerUpsCount();

        hulkPowerUpCount.text = powerUpsCount.hulkPowerUpCount.ToString();
        boxerPowerUpCount.text = powerUpsCount.boxerPowerUpCount.ToString();
        earthquakePowerUpCount.text = powerUpsCount.earthquakePowerUpCount.ToString();
        slingshotPowerUpCount.text = powerUpsCount.slingshotPowerUpCount.ToString();
        bombermanPowerUpCount.text = powerUpsCount.bombermanPowerUpCount.ToString();
    }

    void BuyBombermanPackkWithConfirmation() {
        if (GlobalLevelsInfo.GetMoneyCount() < commonPackCost) {
            OpenMoneyWarningWindow();
        } else {
            OpenConfirmationWindow();
            yesButtonOfConfirmationWindow.onClick.AddListener(BuyBombermanPack);
        }
    }

    void BuySlingshotPackkWithConfirmation() {
        if (GlobalLevelsInfo.GetMoneyCount() < commonPackCost) {
            OpenMoneyWarningWindow();
        } else {
            OpenConfirmationWindow();
            yesButtonOfConfirmationWindow.onClick.AddListener(BuySlinghshotPack);
        }
    }

    void BuyEarthquakePackkWithConfirmation() {
        if (GlobalLevelsInfo.GetMoneyCount() < commonPackCost) {
            OpenMoneyWarningWindow();
        } else {
            OpenConfirmationWindow();
            yesButtonOfConfirmationWindow.onClick.AddListener(BuyEarthquakePack);
        }
    }

    void BuyBoxerPackkWithConfirmation() {
        if (GlobalLevelsInfo.GetMoneyCount() < commonPackCost) {
            OpenMoneyWarningWindow();
        } else {
            OpenConfirmationWindow();
            yesButtonOfConfirmationWindow.onClick.AddListener(BuyBoxerPack);
        }
    }

    void BuyHulkPackkWithConfirmation() {
        if (GlobalLevelsInfo.GetMoneyCount() < commonPackCost) {
            OpenMoneyWarningWindow();
        } else {
            OpenConfirmationWindow();
            yesButtonOfConfirmationWindow.onClick.AddListener(BuyHulkPack);
        }
    }


    void BuyFourthPackkWithConfirmation() {
        if (GlobalLevelsInfo.GetMoneyCount() < fourthPackCost) {
            OpenMoneyWarningWindow();
        } else {
            OpenConfirmationWindow();
            yesButtonOfConfirmationWindow.onClick.AddListener(BuyFourthPack);
        }
    }

    void BuyThirdPackkWithConfirmation() {
        if (GlobalLevelsInfo.GetMoneyCount() < thirdPackCost) {
            OpenMoneyWarningWindow();
        } else {
            OpenConfirmationWindow();
            yesButtonOfConfirmationWindow.onClick.AddListener(() => BuyPack(thirdPackCost, thirdPackNumberOfPoweUps));
        }
    }

    void BuySecondPackWithConfirmation() {
        if (GlobalLevelsInfo.GetMoneyCount() < secondPackCost) {
            OpenMoneyWarningWindow();
        } else {
            OpenConfirmationWindow();
            yesButtonOfConfirmationWindow.onClick.AddListener(() => BuyPack(secondPackCost, secondPackNumberOfPoweUps));
        }
    }

    void BuyFourthPack() {
        GlobalLevelsInfo.SpendMoney(fourthPackCost);
        RandomizePowerUpsFromPackAndAdd(fourthPackNumberOfPoweUps); 
        GlobalLevelsInfo.AddDesiredCountOfBombermanPowerUp(1);
        CloseConfirmationWindow();
        GlobalLevelsInfo.SaveData(); 
    }

    void BuyPack(int cost, int numberOfPowerUps) {
        GlobalLevelsInfo.SpendMoney(cost);
        RandomizePowerUpsFromPackAndAdd(numberOfPowerUps);
        CloseConfirmationWindow();
        GlobalLevelsInfo.SaveData();
    }
    

    void BuyHulkPack() {
        GlobalLevelsInfo.SpendMoney(commonPackCost);
        GlobalLevelsInfo.AddDesiredCountOfHulkPowerUp(hulkPackNumberOfPoweUps);
        CloseConfirmationWindow();
        GlobalLevelsInfo.SaveData();
    }

    void BuyBoxerPack() {
        GlobalLevelsInfo.SpendMoney(commonPackCost);
        GlobalLevelsInfo.AddDesiredCountOfBoxerPowerUp(boxerPackNumberOfPoweUps);
        CloseConfirmationWindow();
        GlobalLevelsInfo.SaveData();
    }

    void BuyEarthquakePack() {
        GlobalLevelsInfo.SpendMoney(commonPackCost);
        GlobalLevelsInfo.AddDesiredCountOfEarthquakePowerUp(earthquakePackNumberOfPoweUps);
        CloseConfirmationWindow();
        GlobalLevelsInfo.SaveData();
    }

    void BuySlinghshotPack() {
        GlobalLevelsInfo.SpendMoney(commonPackCost);
        GlobalLevelsInfo.AddDesiredCountOfSlingshotPowerUp(slingshotPackNumberOfPoweUps);
        CloseConfirmationWindow();
        GlobalLevelsInfo.SaveData();
    }

    void BuyBombermanPack() {
        GlobalLevelsInfo.SpendMoney(commonPackCost);
        GlobalLevelsInfo.AddDesiredCountOfBombermanPowerUp(bombermanPackNumberOfPoweUps);
        CloseConfirmationWindow();
        GlobalLevelsInfo.SaveData();
    }


    void OpenMoneyWarningWindow() {
        moneyWarningWindow.SetActive(true);
        blockerUIForConfirmationWindow.SetActive(true);
    }

    void CloseMoneyWarningWindow() {
        moneyWarningWindow.SetActive(false);
        blockerUIForConfirmationWindow.SetActive(false);
    }

    void CloseConfirmationWindow() {
        confirmationWindow.SetActive(false);
        yesButtonOfConfirmationWindow.onClick.RemoveAllListeners();
        blockerUIForConfirmationWindow.SetActive(false);
    }

    void OpenConfirmationWindow() {
        confirmationWindow.SetActive(true);
        blockerUIForConfirmationWindow.SetActive(true);
    }


    public void RandomizePowerUpsFromPackAndAdd(int countOfPowerUpsInPack) {
        
		for (int i = 0; i < countOfPowerUpsInPack; i++) { // Переделать как-то добавление всех сразу а не по одному
			int powerUpOrderNumber = Random.Range(1, 5);

            if (powerUpOrderNumber == 1) {
                GlobalLevelsInfo.AddDesiredCountOfHulkPowerUp(1);
            }
            if (powerUpOrderNumber == 2) {
                GlobalLevelsInfo.AddDesiredCountOfBoxerPowerUp(1);
            }
            if (powerUpOrderNumber == 3) {
                GlobalLevelsInfo.AddDesiredCountOfEarthquakePowerUp(1);
            }
            if (powerUpOrderNumber == 4) {
                GlobalLevelsInfo.AddDesiredCountOfSlingshotPowerUp(1);
            }
		}
    }
    

    void CloseActiveTab() {
        if (chestTab.activeSelf) {
            chestTab.SetActive(false);
            chestBackground.gameObject.SetActive(false);
            chestTabletText.SetActive(false);
        }
        if (hulkTab.activeSelf) {
            hulkTab.SetActive(false);
            hulkBackground.gameObject.SetActive(false);
            hulkTabletText.SetActive(false);
        }
        if (boxerTab.activeSelf) {
            boxerTab.SetActive(false);
            boxerBackground.gameObject.SetActive(false);
            boxerTabletText.SetActive(false);
        }
        if (earthquakeTab.activeSelf) {
            earthquakeTab.SetActive(false);
            earthquakeBackground.gameObject.SetActive(false);
            earthquakeTabletText.SetActive(false);
        }
        if (slingshotTab.activeSelf) {
            slingshotTab.SetActive(false);
            slingshotBackground.gameObject.SetActive(false);
            slingshotTabletText.SetActive(false);
        }
        if (bombermanTab.activeSelf) {
            bombermanTab.SetActive(false);
            bombermanBackground.gameObject.SetActive(false);
            bombermanTabletText.SetActive(false);
        }
    }

    void OpenChestTab() {
        CloseActiveTab();
        chestBackground.gameObject.SetActive(true);
        chestTab.SetActive(true);
        chestTabletText.SetActive(true);
    }

    void OpenHulkTab() {
        CloseActiveTab();
        hulkBackground.gameObject.SetActive(true);
        hulkTab.SetActive(true);
        hulkTabletText.SetActive(true);
    }
    void OpenBoxerTab() {
        CloseActiveTab();
        boxerBackground.gameObject.SetActive(true);
        boxerTab.SetActive(true);
        boxerTabletText.SetActive(true);
    }
    void OpenEarthquakeTab() {
        CloseActiveTab();
        earthquakeBackground.gameObject.SetActive(true);
        earthquakeTab.SetActive(true);
        earthquakeTabletText.SetActive(true);
    }
    void OpenSlingshotTab() {
        CloseActiveTab();
        slingshotBackground.gameObject.SetActive(true);
        slingshotTab.SetActive(true);
        slingshotTabletText.SetActive(true);
    }
    void OpenBombermanTab() {
        CloseActiveTab();
        bombermanBackground.gameObject.SetActive(true);
        bombermanTab.SetActive(true);
        bombermanTabletText.SetActive(true);
    }
    
}

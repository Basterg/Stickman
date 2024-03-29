using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;

public class MainMenuCanvasController : MonoBehaviour {

    [SerializeField] Button playButton;
    [SerializeField] GameObject playButtonGO;
    [SerializeField] GameObject episodesUIGO;
    [SerializeField] GameObject missionsOfEpisodeOneUIGO;
    [SerializeField] Button episodeOneButton;
    [SerializeField] Button backButton;
    [SerializeField] GameObject gameNameTextGo;

    [SerializeField] GameObject AuthGo;
    [SerializeField] Button authButton;

    [SerializeField] GameObject giftRoad;
    [SerializeField] GameObject giftRoadButtonGO;
    [SerializeField] Button openGiftRoadButton;
    [SerializeField] Button closeGiftRoadButton;

    [SerializeField] GameObject uiBlocker;

    void Start() {
        playButton.onClick.AddListener(OpenEpisodes);
        episodeOneButton.onClick.AddListener(OpenEpisodeOneMissions);
        backButton.onClick.AddListener(Back);
        gameNameTextGo.SetActive(true);
        authButton.onClick.AddListener(MyObj.Instance.Auth);

        openGiftRoadButton.onClick.AddListener(OpenGiftRoad);
        closeGiftRoadButton.onClick.AddListener(CloseGiftRoad);
    }

    [DllImport("__Internal")]
    private static extern void CheckPaymentsExtern(); // Возможно переделать

    void OpenGiftRoad() {
        giftRoad.GetComponent<GiftRoad>().UpdateGiftRoadState();
        giftRoad.SetActive(true);
        uiBlocker.SetActive(true);
    }

    void CloseGiftRoad() {
        giftRoad.SetActive(false);
        uiBlocker.SetActive(false);
    }

    void OpenEpisodes() {
        episodesUIGO.SetActive(true);
        playButtonGO.SetActive(false);
        gameNameTextGo.SetActive(false);
        if (!MyObj.isUnauthMode) {
            CheckPaymentsExtern();
        }


    }

    void OpenEpisodeOneMissions() {
        backButton.gameObject.SetActive(true);
        missionsOfEpisodeOneUIGO.SetActive(true);
        episodesUIGO.SetActive(false);

        giftRoadButtonGO.SetActive(false);
    }

    void Back() {
        episodesUIGO.SetActive(true);
        missionsOfEpisodeOneUIGO.SetActive(false);
        backButton.gameObject.SetActive(false);

        giftRoadButtonGO.SetActive(true);
    }

    void Update() {
        // if (MyObj.isUnauthMode && !AuthGo.activeSelf) {
        //     // Сделать активным кнопку с открытием уведомления и кнопкой для вызова авторизации
        //     AuthGo.SetActive(true);
        // } else if(AuthGo.activeSelf && !MyObj.isUnauthMode) {
        //     AuthGo.SetActive(false);
        // }

        if (MyObj.isReadyToCheckAuth) {
            if (!MyObj.isChekingAuth && MyObj.isUnauthMode) {
                StartCoroutine(MyObj.Instance.CheckPlayerModeAndLoadData());
            }
        }
    }
}

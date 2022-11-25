using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuCanvasController : MonoBehaviour {

    [SerializeField] Button playButton;
    [SerializeField] GameObject playButtonGO;
    [SerializeField] GameObject episodesUIGO;
    [SerializeField] GameObject missionsOfEpisodeOneUIGO;
    [SerializeField] Button episodeOneButton;
    [SerializeField] Button backButton;
    [SerializeField] GameObject gameNameTextGo;

    void Start() {
        playButton.onClick.AddListener(OpenEpisodes);
        episodeOneButton.onClick.AddListener(OpenEpisodeOneMissions);
        backButton.onClick.AddListener(Back);
        gameNameTextGo.SetActive(true);
    }

    void OpenEpisodes() {
        episodesUIGO.SetActive(true);
        playButtonGO.SetActive(false);
        gameNameTextGo.SetActive(false);
    }

    void OpenEpisodeOneMissions() {
        backButton.gameObject.SetActive(true);
        missionsOfEpisodeOneUIGO.SetActive(true);
        episodesUIGO.SetActive(false);
    }

    void Back() {
        episodesUIGO.SetActive(true);
        missionsOfEpisodeOneUIGO.SetActive(false);
        backButton.gameObject.SetActive(false);
    }



    
}

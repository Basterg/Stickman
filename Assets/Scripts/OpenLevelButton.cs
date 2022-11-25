using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenLevelButton : MonoBehaviour {
    [SerializeField] int numberOfLevelToOpen;

    public void OpenLevel() {
        // Если уровень пройден - проверить забран ли бонус за первое прохождение - если нет то открыть окно с вопросом
        // проверить разблокирован ли увроень и если да - то навесить блок имадж
        // возможно получить инфу по звёздам и отобразить их
        SceneManager.LoadScene("Level" + numberOfLevelToOpen);
    }

    
}

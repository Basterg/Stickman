using UnityEngine;

public class AuthButton : MonoBehaviour {
    
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (MyObj.isUnauthMode && !gameObject.activeSelf) {
            // Сделать активным кнопку с открытием уведомления и кнопкой для вызова авторизации
            gameObject.SetActive(true);
        } else if(gameObject.activeSelf && !MyObj.isUnauthMode) {
            gameObject.SetActive(false);
        }
    }
}

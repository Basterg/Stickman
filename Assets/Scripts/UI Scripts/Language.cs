using System.Runtime.InteropServices;
using UnityEngine;

public class Language : MonoBehaviour {
    
    [DllImport("__Internal")]
    private static extern string GetLang();

    public static Language Instance;

    public string CurrentLanguage;

    void Awake() {

        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            CurrentLanguage = GetLang();
        } else {
            Destroy(gameObject);
        }
    }
}

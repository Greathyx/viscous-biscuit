using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {
    public string sceneName;
    public void LoadScene(string sceneName) {
        GameStateController.instance.lastScene = SceneManager.GetActiveScene().name;
        if(sceneName == "SpawningRoom")
        {
            PlayerPrefs.SetString("isMenuActive", "false");
        }
        
        SceneManager.LoadScene(sceneName);
    }
}

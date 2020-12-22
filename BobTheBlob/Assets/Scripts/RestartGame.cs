using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // if press any key on the keyboard
        if(Input.anyKeyDown)
        {
        	PlayerPrefs.SetString("ifRestartGame", "true");
            SceneManager.LoadScene("SpawningRoom");
            GameStateController.instance.liquidLevelDone = false;
            GameStateController.instance.gasLevelDone = false;
        }
    }
}
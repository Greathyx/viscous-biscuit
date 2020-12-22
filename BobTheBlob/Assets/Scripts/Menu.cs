using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public GameObject pauseMenu;

    public void PlayGame()
    {
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }

    public void QuitGame()
    {
    	Application.Quit();
    }

    public void pauseGame()
    {
    	pauseMenu.SetActive(true);
    	Time.timeScale = 0f;
    }

    public void resumeGame()
    {
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("SpawningRoom");
    }
}

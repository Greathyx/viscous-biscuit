using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterNextLevel : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
    	// if press 'E' on the keyboard
        if(Input.GetKeyDown(KeyCode.E))
        {
        	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

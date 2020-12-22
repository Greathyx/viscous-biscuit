using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

    public GameObject title;
    public GameObject text;
    public GameObject Menu;
    public GameObject pauseButton;

    private Image title_img;
    private Text text_text;
    private Image pauseButton_img;
    private string ifRestartGame;

    private AudioSource bgmAudio;
    public AudioClip bgmClip;

    // Start is called before the first frame update
    void Start()
    {
        bgmAudio = gameObject.AddComponent<AudioSource>();
        bgmAudio.playOnAwake = true;
        bgmAudio.volume = 0.2f;
        bgmAudio.clip = bgmClip;
        bgmAudio.Play();

        title_img = title.GetComponent<Image>();
        text_text = text.GetComponent<Text>();
        pauseButton_img = pauseButton.GetComponent<Image>();

        string isMenuActive =  PlayerPrefs.GetString("isMenuActive");
        ifRestartGame =  PlayerPrefs.GetString("ifRestartGame");
        Debug.Log(isMenuActive);
        if (isMenuActive == "false") 
        {
            Menu.SetActive(false);
            PlayerPrefs.DeleteKey("isMenuActive");
        } else {
            Time.timeScale = 0f;
            pauseButton_img.canvasRenderer.SetAlpha(0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(title_img.canvasRenderer.GetAlpha() == 0)
        {
            Menu.SetActive(false);
        }
        
        // if press any key on the keyboard
        if((Menu.activeSelf && Input.anyKeyDown) || ifRestartGame == "true")
        {
            bgmAudio.Stop();
            title_img.CrossFadeAlpha(0, 1.2f, false);
            text_text.CrossFadeAlpha(0, 1.2f, false);
            pauseButton_img.CrossFadeAlpha(1, 1.2f, false);
            Time.timeScale = 1f;
            if (ifRestartGame == "true")
            {
                // Menu.SetActive(false);
                PlayerPrefs.DeleteKey("ifRestartGame");
            }
        }
    }
}
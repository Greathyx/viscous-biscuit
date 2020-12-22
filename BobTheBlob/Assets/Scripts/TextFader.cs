using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextFader : MonoBehaviour
{

    public Controller playerMovement;
    public GameObject title;
    public GameObject text;
    public GameObject restartText;
    public GameObject fader;
    public GameObject menu;
    public GameObject blackOutSquare;
    CircleCollider2D collider;

    private Text toBeContinued;
    private Text restartGame;
    private Image VisousBiscout;
    private Image background;

    void Start(){
        collider = this.GetComponent<CircleCollider2D>();
        toBeContinued = text.GetComponent<Text>();
        restartGame = restartText.GetComponent<Text>();
        restartGame.canvasRenderer.SetAlpha(0.0f);
        restartText.SetActive(false);
        VisousBiscout = title.GetComponent<Image>();
        background = blackOutSquare.GetComponent<Image>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        menu.GetComponent<AudioSource>().enabled = true;
        //if (!toBeContinued.enabled)
         //{
        StartCoroutine(FadeBlackOutSquare());
        //StartCoroutine(FadeImage(true, 1, PlayBtn.GetComponent<Image>()));
         //}
    }

 

 public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 1)
 {
     Color objectColor = blackOutSquare.GetComponent<Image>().color;
     float fadeAmount;

     if (fadeToBlack)
     {
         while (blackOutSquare.GetComponent<Image>().color.a < 1)
         {
             fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

             objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
             blackOutSquare.GetComponent<Image>().color = objectColor;
             yield return null;
         }
     } 
    StartCoroutine(FadeTextToFullAlpha(20f, toBeContinued));
 }
 
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.enabled = true;
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        StartCoroutine(FadeImage(true, 1, VisousBiscout));
        
    }
 
    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        
    }

    IEnumerator FadeImage(bool fadeAway, int fadeSpeed, Image img)
    {

        Color objectColor = img.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeAway)
        {
            while (img.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                img.GetComponent<Image>().color = objectColor;
                yield return null;
            }

            restartText.SetActive(true);
            restartGame.canvasRenderer.SetAlpha(0.0f);
            restartGame.CrossFadeAlpha(1, 1.2f, false);
          //StartCoroutine(FadeTextToFullAlpha(20f, PlayText.GetComponent<Text>()));

        } 
        
        //VisousBiscout.enabled = true;
        // fade from opaque to transparent
        // if (fadeAway)
        // {
        //     // loop over 1 second backwards
        //     for (float i = 1; i >= 0; i -= Time.deltaTime)
        //     {
        //         // set color with i as alpha
        //         VisousBiscout.color = new Color(1, 1, 1, i);
        //         yield return null;
        //     }
        // }
        // // fade from transparent to opaque
        // else
        // {
            
        //     // loop over 1 second
        //     for (float i = 0; i <= 1; i += Time.deltaTime)
        //     {
        //         // set color with i as alpha
        //         VisousBiscout.color = new Color(1, 1, 1, i);
        //         yield return null;
        //     }
        // }
    }
}
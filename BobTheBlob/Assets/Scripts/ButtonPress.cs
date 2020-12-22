using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    private float buttonWidth; // For collision detection
    private GameObject player;   // Player Object
    private Transform buttonTop; // Position of the object to be moved

    public bool pressed;

    private AudioSource presseBtnAudio;
    private bool audioPlayed; // only want the audio to be played once when the button are pressed
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        buttonTop = transform.GetChild(0);
        buttonWidth = gameObject.GetComponentInParent<BoxCollider2D>().size.x;

        presseBtnAudio = gameObject.AddComponent<AudioSource>();
        presseBtnAudio.playOnAwake = false;
        presseBtnAudio.clip = clip;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerTouches()) {
            if(!audioPlayed)
            {
                presseBtnAudio.Play();
                audioPlayed = true;
            }
            Depress();
            pressed = true;
        } else {
            Rise();
            audioPlayed = false;
            pressed = false;
        }
    }

    bool PlayerTouches() {
        return player.transform.position.x > this.transform.position.x - buttonWidth/2 && player.transform.position.x < this.transform.position.x + buttonWidth/2 
        && player.transform.position.y > this.transform.position.y && player.transform.position.y < this.transform.position.y + 4.0f;
    }

    void Depress() {    // Lower button top
        buttonTop.localPosition = new Vector2(0.0f, buttonWidth/5 * 0.3f);  // Some arithmetic to make button scaleable
    }

    void Rise() {     // Raise button top
        buttonTop.localPosition = new Vector2(0.0f, buttonWidth/5 * 0.6f);
    }
}

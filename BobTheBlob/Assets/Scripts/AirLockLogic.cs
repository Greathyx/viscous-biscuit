using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirLockLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip steamClip;
    private AudioSource steamNoise;
    private GameObject left;
    private GameObject right;
    private bool rightOpen;
    //public bool playerInside;

    void Start()
    {
        rightOpen = true;
        steamNoise = gameObject.AddComponent<AudioSource>();
        steamNoise.playOnAwake = false;
        steamNoise.clip = steamClip;

        left = GameObject.FindWithTag("LeftAirLock");
        right = GameObject.FindWithTag("RightAirLock");
        right.GetComponent<AirLock>().OpenClose(); //Close the left door
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            if(rightOpen) {
                other.gameObject.GetComponent<Controller>().highPressure = true;
            } else {
                other.gameObject.GetComponent<Controller>().highPressure = false; // blob cannot be liquid during high pressure
            }
            StartCoroutine(AirLockSwitch());
            /*left.GetComponent<AirLock>().OpenClose();
            right.GetComponent<AirLock>().OpenClose();*/
        }
    }

    private IEnumerator AirLockSwitch() {
        steamNoise.Play();
        if(rightOpen) {
            right.GetComponent<AirLock>().OpenClose();
        } else {
            left.GetComponent<AirLock>().OpenClose();
        }
        yield return new WaitForSeconds(5);
        if(rightOpen) {
            left.GetComponent<AirLock>().OpenClose();
            rightOpen = false;
        } else {
            right.GetComponent<AirLock>().OpenClose();
            rightOpen = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

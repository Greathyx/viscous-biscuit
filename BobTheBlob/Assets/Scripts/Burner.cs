using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burner : MonoBehaviour
{

    public ButtonPress trigger;    // TODO: Generalize...
    public Controller playerControls;
    public Transform playerTransform;
    public float minBurningDistance = 4.5f;

    public bool gotToggled;

    // Start is called before the first frame update
    void Start(){   
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger.pressed & !gotToggled) {
            gotToggled = true;
            toggleFlame();
        }

        if(Vector2.Distance(playerTransform.position, this.transform.position) <= minBurningDistance){
            playerControls.isBurning = true;
        }
    }

    void toggleFlame(){
        gameObject.SetActive(false);
    }

}

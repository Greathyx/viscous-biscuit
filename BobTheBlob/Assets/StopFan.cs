using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopFan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateController.instance.liquidLevel && GameStateController.instance.gasLevel)
        {
            //Stop fan animation
             this.gameObject.GetComponent<Animator>().enabled = false;
             this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
             this.gameObject.GetComponent<AreaEffector2D>().enabled = false;

        }
        
    }
}

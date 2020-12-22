using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Rotates children from a given angle to a given angle. Currently configured only for buttons, but the button can have any tag name, specified in a field from the inspector.*/
public class RotateHinge : MonoBehaviour
{
    public float fromAngle;
    public float toAngle;
    public float turningRate = 30f;
    [TextArea]
    public string TriggerTagName; 
    private ButtonPress trigger;    // TODO: Generalize...
    public bool persists;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Trigger tag name: " + TriggerTagName);
        trigger = GameObject.FindGameObjectWithTag(TriggerTagName).GetComponent<ButtonPress>(); // TODO: Generalize this
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger.pressed) {
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.Euler(0, 0, toAngle), turningRate * Time.deltaTime);//Quaternion.Euler(0, 0, toAngle);
        } else if (!persists) {
            this.transform.rotation = Quaternion.Euler(0, 0, fromAngle);
        }
    }
}

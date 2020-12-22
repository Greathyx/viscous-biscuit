using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanControl : MonoBehaviour
{
    [TextArea]
    public string TriggerTagName; 
    private ButtonPress trigger;    // TODO: Generalize...
    public bool persists;
    // Start is called before the first frame update
    void Start()
    {
        trigger = GameObject.FindGameObjectWithTag(TriggerTagName).GetComponent<ButtonPress>(); // TODO: Generalize this
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger.pressed) {
            this.GetComponentInParent<AreaEffector2D>().enabled = true;
        } else if (!persists) {
            this.GetComponentInParent<AreaEffector2D>().enabled = false;
        }
    }
}

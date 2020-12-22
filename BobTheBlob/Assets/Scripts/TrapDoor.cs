using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    [TextArea]
    public string connectedObject;

    public Vector3 displacement; 
    public bool toggled = false;

    private ButtonPress button;
    private Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.Find(connectedObject).GetComponent<ButtonPress>();
        transform = this.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(button.pressed & !toggled){
            toggled = true;
            displace();
        }
    }

    void displace(){
        transform.Translate(displacement);
    }
}

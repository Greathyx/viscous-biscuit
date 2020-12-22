using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class togglingDebugger : MonoBehaviour
{
    private Canvas canvasComponent;
    // Start is called before the first frame update
    void Awake()
    {
        canvasComponent = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            canvasComponent.enabled ^= true;
        }   
    }
}

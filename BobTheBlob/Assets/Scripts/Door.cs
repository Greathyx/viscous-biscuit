using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    //public int TargetScene;
    [TextArea]
    public string TargetScene;
    CircleCollider2D collider;
    private SwitchScene s;
    // Start is called before the first frame update
    void Start()
    {
        collider = this.GetComponent<CircleCollider2D>();
        s = GameObject.FindGameObjectWithTag("SceneSwitcher").GetComponent<SwitchScene>();
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        //SceneManager.LoadScene(sceneName: TargetScene);
        //SwitchScene[] s = FindObjectsOfType<SwitchScene>();
        //LoadScene(TargetScene);
       s.LoadScene(TargetScene);
    }
}

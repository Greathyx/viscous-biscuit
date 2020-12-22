using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirLock : MonoBehaviour{

    /*public*/ bool open;
    bool animationRunning;
    float t;
    float deltaT;
    [Range(10, 500)]
    public int numSteps;
    Vector3 startPos;
    Vector3 endPos;
    GameObject door;

    void Start(){
        open = true;
        door = transform.Find("Door").gameObject;
        startPos = door.transform.localPosition;
        endPos = startPos - (transform.Find("Fixture").localPosition - startPos).normalized * 3f;
        deltaT = 1f / numSteps;
        animationRunning = false;
    }

    // Update is called once per frame
    void Update(){

    }

    public void OpenClose(){
        open = !open;
        animationRunning = true;
        StartCoroutine(StartAnimation());
    }
    IEnumerator StartAnimation(){
        t = 0;
        Vector3 tempPos = door.transform.localPosition;
        for(int i = 0; i < numSteps; i++){
            t += deltaT;
            door.transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            yield return new WaitForSeconds(deltaT);
        }   
        endPos.Set(tempPos.x, tempPos.y, tempPos.z);
        startPos = door.transform.localPosition;
        animationRunning = false;
    }
}

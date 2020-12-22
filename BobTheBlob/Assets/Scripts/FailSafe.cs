using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailSafe : MonoBehaviour
{   
    Blob blob;
    Vector3[] vertices;
    Vector3 avgPos;
    int divider;
    int index;
    [Range(0, 1f)]
    public float stepSize = 0.1f;

    void Start(){
        blob = GetComponent<Blob>();
        divider = 4;
        index = (int) Mathf.Round(blob.numJoints / divider);
        vertices = new Vector3[divider]; 
    }

    // Update is called once per frame
    void Update(){
        for(int i = 0; i < divider; i++){
            vertices[i] = blob.joints[i*index].transform.position;
        }
        avgPos = GetMeanPosition(vertices);
        
        // if center is away from avg then realign it
        if((blob.centerBody.position - (Vector2) avgPos).magnitude > blob.radius * 0.5f){
            blob.centerBody.position = Vector3.MoveTowards(blob.centerBody.position, avgPos, stepSize);
            Debug.Log("centerAdjusted");
        }
        
    }

    Vector3 GetMeanPosition(Vector3[] vertices){
        Vector3 meanPos = Vector3.zero;
        for(int i = 0; i < vertices.Length; i++){
            meanPos += vertices[i];
        }
        return meanPos / vertices.Length;
    }

    /*
    private bool CheckOutOfBounds(Vector3[] vertices, Vector3 centerPos){
        Vector3 v1, v2;
        int intersectionsY, intersectionsX = 0;
        for(int i = 0; i < vertices.Length; i++){  
            v1 = vertices[i];
            v2 = vertices[(i + 1 > vertices.Length - 1 ? 0 : i + 1)];
            // if y-value is between v1 and v2
            if((centerPos.y < v1.y && centerPos.y > v2.y) || (centerPos.y > v1.y && centerPos.y < v2.y)){
                intersectionsY += 1;
            }
            // check x values 
            if((centerPos.x < v1.x && centerPos.x > v2.x) || (centerPos.x > v1.x && centerPos.x < v2.x)){
                intersectionsX += 1;
            }
        }
        if(intersectionsX == 2 && intersectionsY == 2){
            return true;
        }
        return false;
    }
    */
}

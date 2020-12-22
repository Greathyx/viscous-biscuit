using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControl : MonoBehaviour
{
    public Controller playerMovement;
    public Transform playerTransform;
    private Transform elevatorTransform;
    public Vector3 translationSpeed;
    public float minTriggerDistance;
    public float maxTravelDistance;
 
    private Vector3 origin;
    private float playerDistance;
    private float distanceTraveled;


    private bool startedMoving = false;

    private

    // Start is called before the first frame update
    void Start(){
        elevatorTransform = this.transform;
        origin = elevatorTransform.position;
    }

    // Update is called once per frame
    void Update(){
        playerDistance = Vector3.Distance(playerTransform.position, elevatorTransform.position);
        if(playerDistance <= minTriggerDistance){
            startedMoving = true;
        }         

        if(startedMoving){ElevatorMotion(translationSpeed);}
    }

    void ElevatorMotion(Vector3 t){
        distanceTraveled = Vector3.Distance(elevatorTransform.position, origin);
        if(distanceTraveled >= maxTravelDistance){
            playerMovement.enabled = false;
            playerTransform.parent = elevatorTransform;
            this.enabled = false;
        } else {
            elevatorTransform.Translate(t);
        }
    }
}

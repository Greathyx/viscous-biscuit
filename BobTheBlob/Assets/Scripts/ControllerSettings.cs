using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ControllerSettings : ScriptableObject {
   // movement variables
    [Range(1, 10)]
    public float forceMultiplier;
    [Range(1, 100)]
    public float force;
    [Range(1, 30)]
    public float maxVelocityX;
    [Range(1, 30)]
    public float maxVelocityY;
    public Vector2 stoppingSpeed;
    public Vector2 velocityVectorY;
    public Vector2 velocityVectorX;

    // jumping variables
    [Range(0, 10)]
    public float jumpingForce;
    [Range(0,20)]
    public float maxJumpingVelocity = 10f;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BlobSettings : ScriptableObject{
    [Range(5, 20)]
    public int numJoints = 5;
    [Range(1, 5)]
    public float radius = 1f;
    [Range(0.5f, 5.0f)]
    public float stiffnes = 1f; // controlls how stiff the blob is
    [Range(0.1f, 5)]
    public float colliderRadius;
    [Range(0.1f, 5)]
    public float centerColliderRadius;
    [Range(0.1f, 1f)]
    public float springMinDist;
}

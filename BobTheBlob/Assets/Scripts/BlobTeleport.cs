using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlobTeleport : MonoBehaviour
{
    public float TargetX;
    public float TargetY;
    CircleCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = this.GetComponent<CircleCollider2D>();
    }


    void OnTriggerEnter2D(Collider2D other) {
        other.transform.parent.gameObject.transform.position = new Vector2(TargetX, TargetY);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleState : MonoBehaviour
{
    private CircleCollider2D playerColliderSolid;
    private CapsuleCollider2D playerColliderLiquid;
    // Start is called before the first frame update
    void Awake()
    {
        playerColliderSolid = GetComponent<CircleCollider2D>();
        playerColliderLiquid = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            playerColliderSolid.enabled = playerColliderSolid.enabled^true; // Beatiful but won't work with more than 2 states
            playerColliderLiquid.enabled = playerColliderLiquid.enabled^true;
        }
    }
}

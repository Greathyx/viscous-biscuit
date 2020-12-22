using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D player;
    [SerializeField]
    private float speedMultiplier;
    [SerializeField]
    private float jumpForce;
    private bool canJump;
    
    void Awake()
    {
        canJump=true;
        player = GetComponent<Rigidbody2D>();
    }       
   
   void Update()
    {
        player.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * speedMultiplier, 0.8f), player.velocity.y);

        if(player.velocity.y == 0f){
            canJump = true;
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && canJump){
            canJump = false;
            //Debug.Log("hit space");   
            Vector2 up = transform.TransformDirection(Vector2.up);

            //player.velocity += up * 0.8f * speedMultiplier;
            //new Vector2(0,Mathf.Lerp(0, Input.GetAxis("Vertical") * speedMultiplier, 0.8f))
            player.AddForce(up * jumpForce, ForceMode2D.Impulse);
            //Debug.Log(up * 5f * speedMultiplier);
            }
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}

using System.Collections;
using System.Collections.Generic;
using  UnityEngine.SceneManagement;
using UnityEngine;

public class Controller : MonoBehaviour
{
    /******************************
        Variable declarations
    ******************************/
    // state stiffnes values
    public enum States{
        Default, 
        Solid, 
        Liquid, 
        Gaseous
    };

    public bool highPressure;

    public int currentState = (int)States.Default;

    public float defaultStiffnes = 1.8f;
    public float liquidStiffnes = 0.9f;
    public float gaseousStiffnes = 5.0f;

    public float defaultForce = 50f;
    public float liquidForce = 15f;
    public float gaseousForce = 30f;

    public float d_MaxVelocityX = 25f;
    public float l_MaxVelocityX = 10f;
    public float g_MaxVelocityX = 15f;

    public float soaringVecolityMax = 6f;
    public float terminalVelocityMax = 40f;

    public float d_JumpingImpulseForce = 10f;
    public float l_JumpingImpulseForce = 8f;
    public float d_CenterBodyImpulseRatio = 0.8f; // percentage of the force applied to the center that is also applied to the joints when jumping
    public float l_CenterBodyImpulseRatio = 0.4f; // percentage of the force applied to the center that is also applied to the joints when jumping

    public Vector2 centerInertia;
    public Vector2 surfaceInertia;

    public ControllerSettings controllerSettings;

    // basic object and directions
    Blob blob;
    Vector2 up;
    Vector2 left;
    Vector2 right;

    //status effect variables
    public bool isBurning = false;
    
    // movement variables
    float angle;
    float forceMultiplier;
    private float force;
    private float maxVelocityX;
    private float maxVelocityY;

    // jumping variables
    private float jumpingForce;
    private float impulseRatio;
    private bool midAir = false;

    // Ground check objects
    private CircleCollider2D boxCollider2d;
    [SerializeField] private LayerMask platformLayerMask;

    // movement audios source
    private AudioSource toSolidAudio;
    private AudioSource toLiquidAudio;
    private AudioSource toGasAudio;
    private AudioSource jumpAudio;

    public AudioClip toSolidClip;
    public AudioClip toLiquidClip;
    public AudioClip toGasClip;
    public AudioClip jumpClip;

    void Start(){
        highPressure = false; //Spagheeetti
        toSolidAudio = gameObject.AddComponent<AudioSource>();
        toSolidAudio.playOnAwake = false;
        toSolidAudio.clip = toSolidClip;

        toLiquidAudio = gameObject.AddComponent<AudioSource>();
        toLiquidAudio.playOnAwake = false;
        toLiquidAudio.clip = toLiquidClip;

        toGasAudio = gameObject.AddComponent<AudioSource>();
        toGasAudio.playOnAwake = false;
        toGasAudio.clip = toGasClip;

        jumpAudio = gameObject.AddComponent<AudioSource>();
        jumpAudio.playOnAwake = false;
        jumpAudio.clip = jumpClip;

        up = new Vector2(0f, 1f);
        right = new Vector2(1, 0);
        left = new Vector2(-1, 0);
        blob = gameObject.GetComponent<Blob>();
        centerInertia = new Vector2(0.1f, 1);
        surfaceInertia = new Vector2(0.95f, 1);
        jumpingForce = 1f;

        // ChangeState((int)States.Default);
        blob.stiffnes = defaultStiffnes;
        currentState = (int)States.Default;
        
        force = defaultForce;
        jumpingForce = d_JumpingImpulseForce;
        impulseRatio = d_CenterBodyImpulseRatio;
        
        maxVelocityX = d_MaxVelocityX;
        maxVelocityY = soaringVecolityMax;
       }

    /******************************
        Variable declarations
    ******************************/
    void Update(){ // Update is called once per frame
    	// move audio for only key pressed but not hold
    	// if(!Input.GetKey(KeyCode.LeftShift) && 
     //        (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))) {
            
     //    }

        // vertical movement
        if(Input.GetKey(KeyCode.RightArrow)){
            Move(right*force);
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            Move(left*force);
        }
        else{
            Inertia();
        }

        // clamp falling speed
        if(Mathf.Abs(blob.centerBody.velocity.y) >= terminalVelocityMax){
            TerminalFallingCap(terminalVelocityMax);
        }

        // change of states
        if(Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))){
            if (currentState != (int)States.Default){
            	toSolidAudio.Play();
                ChangeState((int)States.Default);
            }
        }

        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.DownArrow)){
            if (currentState != (int)States.Liquid){
            	toLiquidAudio.Play();
                ChangeState((int)States.Liquid);
            }
        }

        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.UpArrow)){
            if (currentState != (int)States.Gaseous && !highPressure){
            	toGasAudio.Play();
                ChangeState((int)States.Gaseous);
            }
        }

        // hobo solution for gas state floating
        if((currentState == (int)States.Gaseous) && (blob.centerBody.velocity.y <= maxVelocityY)){
            Move(up*force);
        }
        
        // Jumping mechanic
        if(Input.GetKeyDown(KeyCode.Space) && !midAir && IsGrounded()) {
            midAir = true;
            Jump();
            jumpAudio.Play();// play jump audio source
        }
        if(midAir && Input.GetKeyUp(KeyCode.Space)){
            midAir = false;
        }

        //status effect reactions
        if(isBurning){
            if (currentState != (int)States.Gaseous){
            	toGasAudio.Play();
                Jump();
                ChangeState((int)States.Gaseous);
            }
            isBurning = false;
        }

        // restart
        if(Input.GetKey(KeyCode.R)) {
            Restart();
        }
    }

    /****************************
        Method calls
    ****************************/
    void ChangeState(int endState){
        switch (endState)
        {
            case (int)States.Default:
                blob.stiffnes = defaultStiffnes;
                currentState = (int)States.Default;
                
                force = defaultForce;
                jumpingForce = d_JumpingImpulseForce;
                impulseRatio = d_CenterBodyImpulseRatio;
                
                maxVelocityX = d_MaxVelocityX;
                maxVelocityY = soaringVecolityMax;
                break;
            case (int)States.Solid:
                Debug.Log("Change State called with solid state as argument");
                break;
            case (int)States.Liquid:
                blob.stiffnes = liquidStiffnes;
                currentState = (int)States.Liquid;

                force = liquidForce;
                jumpingForce = l_JumpingImpulseForce;
                impulseRatio = l_CenterBodyImpulseRatio;

                maxVelocityX = l_MaxVelocityX;
                maxVelocityY = soaringVecolityMax;
                break;
            case (int)States.Gaseous:
                blob.stiffnes = gaseousStiffnes;
                currentState = (int)States.Gaseous;

                force = gaseousForce;
                jumpingForce = 0f;
                impulseRatio = 0f;;
                
                maxVelocityX = g_MaxVelocityX;
                maxVelocityY = soaringVecolityMax;
                break;
            default:
                Debug.Log("change state called with invalid argument");
                break;
        }
        // stop start particle effect 
        blob.StopStartParticles(endState == (int)States.Gaseous);
        // enable disable hingeJoints
        blob.EnableDisableHingeJoints(endState == (int)States.Default);
    }

    void Jump(){
        blob.centerBody.AddForce(up*jumpingForce, ForceMode2D.Impulse);
        for(int i = 0; i < blob.numJoints; i++){
            blob.joints[i].GetComponent<Rigidbody2D>().AddForce(up * jumpingForce * impulseRatio, ForceMode2D.Impulse);
        }
    }

    void TerminalFallingCap(float cap){
        Vector2 velocityVectorY;
        Vector2 velocityVectorX;

        Vector2 axisX = new Vector2(1f,0f);
        Vector2 axisY = new Vector2(0f,1f);

        velocityVectorX = Vector2.Scale(blob.centerBody.velocity, axisX);
        velocityVectorY = Vector2.Scale(blob.centerBody.velocity, axisY);

        velocityVectorY = Vector2.ClampMagnitude(velocityVectorY, cap);
        blob.centerBody.velocity = velocityVectorX + velocityVectorY;
    }

    void Move(Vector2 dir){ 
        Vector2 velocityVectorY;
        Vector2 velocityVectorX;

        blob.centerBody.AddForce(dir);
        for(int i = 0; i < blob.numJoints; i++){
            // angle between up and current joint
            angle = Vector2.Angle((blob.joints[i].transform.position - blob.joints[blob.numJoints].transform.position), up);
            // only apply force if angle is between certain range
            if(angle < 90f){ 
                //apply force
                forceMultiplier = (180f - angle)/180f;
                blob.joints[i].GetComponent<Rigidbody2D>().AddForce(dir*forceMultiplier);
                
                //throttle velocity along x-axis only
                velocityVectorX = Vector2.Scale(blob.joints[i].GetComponent<Rigidbody2D>().velocity, right);
                velocityVectorY = Vector2.Scale(blob.joints[i].GetComponent<Rigidbody2D>().velocity, up);
                
                velocityVectorX = Vector2.ClampMagnitude(velocityVectorX, maxVelocityX);
                blob.joints[i].GetComponent<Rigidbody2D>().velocity = velocityVectorX + velocityVectorY;
            } 
        }
    }

    void Inertia(){
        blob.centerBody.velocity = Vector2.Scale(blob.centerBody.velocity, centerInertia);
        for(int i = 0; i < blob.numJoints; i++){
            blob.joints[i].GetComponent<Rigidbody2D>().velocity = Vector2.Scale(blob.joints[i].GetComponent<Rigidbody2D>().velocity, surfaceInertia);
        }
    }

    private bool IsGrounded(){
        bool onGround = false;
        //Check for every joint
        for (int i=0; i<blob.numJoints; i++) {
            boxCollider2d = blob.joints[i].GetComponent<CircleCollider2D>();
            float extraHeightText = .5f;
            //Raycasthit from circle collider and down to see if we stand on a platform
            RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.down, boxCollider2d.bounds.extents.y + extraHeightText, platformLayerMask);
            Color rayColor;
            if (raycastHit.collider != null){
                //If the ray hit a platform we set the color to green and set onGround to true
                rayColor = Color.green;
                onGround = true;
            }
            else {
                rayColor = Color.red;
            }
            //Draw the line which we see in unity
            Debug.DrawRay(boxCollider2d.bounds.center, Vector2.down * (boxCollider2d.bounds.extents.y+ extraHeightText), rayColor);
            //Debug.Log(raycastHit.collider);
        }    
        return onGround;    
    }

    void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateSettings(ControllerSettings settings){
        // movementVariables
        forceMultiplier = settings.forceMultiplier;
        force = settings.force;
        maxVelocityX = settings.maxVelocityX;
        maxVelocityY = settings.maxVelocityY;
        // stoppingSpeed = settings.stoppingSpeed;
        // velocityVectorY = settings.velocityVectorY;
        // velocityVectorX = settings.velocityVectorX;

        // jumping variables
        jumpingForce = settings.jumpingForce;
        // maxJumpingVelocity = settings.maxJumpingVelocity;
    }
}

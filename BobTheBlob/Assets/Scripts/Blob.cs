using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class Blob : MonoBehaviour{ 
    // number of joints in blob and the initial radius of the blob
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

    public BlobSettings blobSettings;

    // object that holds all the joint information of the blob
    GameObject blobJoint;
    
    // comonents of the blobObject
    public GameObject[] joints;
    public SpringJoint2D[][] springJoints;
    public DistanceJoint2D[][] distanceJoints;
    public HingeJoint2D[][] hingeJoints;
    public Rigidbody2D centerBody;

    // holds the mesh
    MeshFilter meshFilter;
    BlobMesh blobMesh;
    GameObject meshObj;
    [Range(1, 10)]
    public int meshDetail = 1;

    // particleSystem stuff
    ParticleSystem particleSystem;
    BlobParticles blobParticles;

    void Start(){
        this.transform.position = GameObject.FindGameObjectWithTag("SceneState").GetComponent<GameStateController>().currentTransition();
        // + 1 is for accomodation of the centerJoint
        joints = new GameObject[numJoints + 1];
        springJoints = new SpringJoint2D[numJoints + 1][];
        distanceJoints = new DistanceJoint2D[numJoints + 1][];
        hingeJoints = new HingeJoint2D[numJoints][];
        springMinDist = 0.1f;
        
        // add joints to the blob
        for(int i = 0; i < numJoints; i++){
            joints[i] = AddJoint(i);
        }

        // set the Joints to correct position
        PositionJoints();
        
        // add a center joint that every other joint is connected to
        gameObject.name = "Blob";
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        centerBody = gameObject.GetComponent<Rigidbody2D>(); //NOTE: Modification - save reference to center joint
        gameObject.AddComponent<CircleCollider2D>();
        joints[numJoints] = gameObject;
        
        // connect joints
        ConnectJoints();
        centerColliderRadius = joints[0].GetComponent<CircleCollider2D>().radius * 1.5f;
        gameObject.GetComponent<CircleCollider2D>().radius = centerColliderRadius;

        // initialize the mesh for the blob
        InitMesh();

        // initiliazise particle system used for gas state
        InitParticleSystem();
    }

    void UpdateSettings(BlobSettings settings){
        numJoints = settings.numJoints;
        radius = settings.radius;
        stiffnes = settings.stiffnes; // controlls how stiff the blob is
    }

    void Update(){
        meshObj.transform.position = Vector3.zero;
        blobMesh.UpdateMesh();
        UpdateStiffnes();

        // update proprteis if they've changed
        if(colliderRadius != joints[0].GetComponent<CircleCollider2D>().radius){
            UpdateColliderRadius();
        }
        if(centerColliderRadius != gameObject.GetComponent<CircleCollider2D>().radius){
            gameObject.GetComponent<CircleCollider2D>().radius = centerColliderRadius;
        }

        // enable and disble mesh depending on state
        if(particleSystem.isPlaying){
            meshObj.GetComponent<MeshRenderer>().enabled = false;
        }
        else if (!meshObj.GetComponent<MeshRenderer>().enabled){
            meshObj.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    void ConnectJoints(){  // connects the joints with spring and distance-Joints
        float springDistance = (joints[0].transform.position - joints[2].transform.position).magnitude; 
        colliderRadius = (joints[0].transform.position - joints[1].transform.position).magnitude/2f;
        for(int i = 0; i < numJoints; i++){
            // connect springJoints to neighbour one over and to the center joint
            springJoints[i][0].connectedBody = joints[(i + 2 > (numJoints-1) ? (i+2) % numJoints: i + 2)].GetComponent<Rigidbody2D>();
            springJoints[i][1].connectedBody = joints[i - 2 < 0 ? numJoints + (i-2) :  i - 2].GetComponent<Rigidbody2D>();
            springJoints[i][2].connectedBody = gameObject.GetComponent<Rigidbody2D>();

            for(int j = 0; j < 3; j++){
                springJoints[i][j].autoConfigureDistance = false;
                springJoints[i][j].distance = springDistance;
            }

            // connect distance Joints to closesest neighbour
            distanceJoints[i][0].connectedBody = joints[(i + 1 > (numJoints-1) ? 0 : i + 1)].GetComponent<Rigidbody2D>();
            
            // set distance joint to centerBody
            distanceJoints[i][1].connectedBody = centerBody;
            distanceJoints[i][1].maxDistanceOnly = true;
            distanceJoints[i][1].autoConfigureDistance = false;
            distanceJoints[i][1].distance = radius * 1.5f;

            // set hingeJoints
            hingeJoints[i][0].connectedBody = joints[(i - 1 < 0 ? numJoints - 1 : i - 1)].GetComponent<Rigidbody2D>();
            hingeJoints[i][1].connectedBody = joints[(i + 1 > (numJoints-1) ? 0 : i + 1)].GetComponent<Rigidbody2D>();
            hingeJoints[i][1].enabled = false;
            //hingeJoints[i][0].enabled = false;
            for(int j = 0; j < 2; j++){
                JointAngleLimits2D limits = hingeJoints[i][j].limits;
                limits.min = -10;
                limits.max = 10;
                hingeJoints[i][j].limits = limits;
                hingeJoints[i][j].useLimits = true;
                //hingeJoints[i][j].enabled = false;
            }
            
            joints[i].GetComponent<CircleCollider2D>().radius = colliderRadius;
        }

    }

    void PositionJoints(){ // position the joints in circle depending on radius and num joints
        float theta = 360f/numJoints;
        float angle;

        // ofset for the position of the center joint
        float xOffset = transform.position.x;
        float yOffset = transform.position.y;
        for(int i = 0; i < numJoints; i++){
            angle = (Mathf.PI / 180) * theta * i;
            joints[i].transform.position = new Vector2(radius * Mathf.Sin(angle) + xOffset, 
                radius * Mathf.Cos(angle) + yOffset);
        }
    }

    GameObject AddJoint(int index){ // add new joint with all the appropriate components
        blobJoint = new GameObject();
        blobJoint.name = "Joint_" + index.ToString();
        blobJoint.transform.parent = this.transform;
        blobJoint.AddComponent<Rigidbody2D>();
        blobJoint.AddComponent<CircleCollider2D>();

        // add springJoints
        springJoints[index] = new SpringJoint2D[3];
        for(int i = 0; i < 3; i++){
            springJoints[index][i] = blobJoint.AddComponent<SpringJoint2D>();
        }

        // add distanceJoints
        distanceJoints[index] = new DistanceJoint2D[2];
        for(int i = 0; i < 2; i++){
            distanceJoints[index][i] = blobJoint.AddComponent<DistanceJoint2D>();
        }

        // add hingeJoints
        hingeJoints[index] = new HingeJoint2D[2];
        for(int i = 0; i < 2; i++){
            hingeJoints[index][i] = blobJoint.AddComponent<HingeJoint2D>();
        }

        return blobJoint;
    }

    void InitMesh(){
        meshObj = new GameObject("meshObj");
        meshObj.transform.parent = transform;
        meshObj.AddComponent<MeshRenderer>();
        meshFilter = meshObj.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = new Mesh();
        blobMesh = new BlobMesh(this, meshFilter.sharedMesh);
        meshObj.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/TestingMat");

        // set the render queue to be after the sprites render queue
        meshObj.GetComponent<MeshRenderer>().material.renderQueue = this.GetComponent<SpriteRenderer>().material.renderQueue + 1;        
    }

    void InitParticleSystem(){
        GameObject particleObj = new GameObject("particleObj");

        // make sure the particle system is att correct position
        particleObj.transform.parent = transform;
        particleObj.transform.localPosition = Vector3.zero;
        
        // add renderer and other stuff
        particleSystem = particleObj.AddComponent<ParticleSystem>();
        ParticleSystemRenderer renderer = particleObj.GetComponent<ParticleSystemRenderer>();
        renderer.material = Resources.Load<Material>("Materials/gasMat");
        renderer.material.renderQueue = meshObj.GetComponent<MeshRenderer>().material.renderQueue;
        blobParticles = new BlobParticles(particleSystem);
        
        // itnitililize it at stop
        particleSystem.Stop();
    }

    public void StopStartParticles(bool start){
        if(start){
            particleSystem.Play();  
        }
        else{
            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            Debug.Log(particleSystem.isPlaying);        
        }
        /*
        for(int i = 0; i < numJoints; i++){
                joints[i].GetComponent<CircleCollider2D>().enabled = !start;
        }
        */
        
    }

    public void EnableDisableHingeJoints(bool enable){
        for(int i = 0; i < numJoints; i++){
            hingeJoints[i][0].useLimits = enable; 
        }
    }

    void UpdateStiffnes(){
        for(int i = 0; i < numJoints; i++){
            springJoints[i][2].frequency = stiffnes;
        }
    }

    void UpdateColliderRadius(){
        for(int i = 0; i < numJoints; i++){
            joints[i].GetComponent<CircleCollider2D>().radius = colliderRadius;
        }
    }

    void UpdateSprings(){
        for(int i = 0; i < springJoints.Length; i++){
            springJoints[i][2].dampingRatio = springMinDist;
        }
    }
}

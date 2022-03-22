using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Modules
    public MovementModule movementModule;
    public CollisionModule collisionModule;
    public InputModule inputModule;

    //Current Room Player is in
    public RoomModule currentRoom;

    //Game Controller
    public GameController gameController;

    //RigidBody
    public Rigidbody2D rb;

    //Speed
    public float moveSpeed;

    //Key Controls
    public KeyCode staircaseIntKey = KeyCode.E;
    public KeyCode lightIntKey = KeyCode.Q;
    public KeyCode fixFuseKey = KeyCode.Z;
    public KeyCode genIntKey = KeyCode.S;
    public KeyCode fullLevelViewKey = KeyCode.Space;

    //Trigger Bools
    public bool isPlayerInStaircase;
    public bool isPlayerInLightswitch;
    public bool isPlayerInFusebox;
    public bool isPlayerInGenerator;

    //Bool: Checks Player Walking
    public bool isPlayerWalking;

    //Bool: Check if Player is Viewing Entire level
    public bool isViewingFullLevel;

    //Disabled
    public bool isFlashLightEnabled;

    //Float: Input Deadzone
    float deadZone = 0.001f;

    //Animator: Player Animations
    public Animator animator;

    //Camera: Main Camera
    public Camera cam;

    //Transform: Starting Position of Camera
    public Vector3 startCamPosition;

    //Float: Move Speed of Camera
    public float camMoveSpeed;

    //Float: Zoom Out Speed
    public float camMoveOutSpeed;
    
    private void Awake()
    {
        //Assign RigidBody Component
        rb = GetComponent<Rigidbody2D>();

        //Get GameController
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        //Assign Main Camera
        cam = Camera.main;

        //Set Camera Starting Position
        startCamPosition = cam.transform.position;
         
    }

    private void Update()
    {
        //Set the Animator Bool in Animator
        animator.SetBool("isPlayerWalking", isPlayerWalking);
    }

    private void FixedUpdate()
    {
        //Player is NOT viewing full level
        if (!isViewingFullLevel)
        {

            //Check if Player is Moving
            if (Input.GetAxis("Horizontal") > deadZone && Input.GetAxis("Horizontal") > 0 ||
                Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Horizontal") < deadZone)
            {
                //Player is Walking
                isPlayerWalking = true;
            }
            else
            {
                //Playerr is Not Walking
                isPlayerWalking = false;
            }

            //Call Player Movement
            movementModule.Movement(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed, 0));
        }
    }

}

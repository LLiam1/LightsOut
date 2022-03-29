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
    public KeyCode elevatorIntKey = KeyCode.F;
    public KeyCode genIntKey = KeyCode.S;
    public KeyCode fullLevelViewKey = KeyCode.Space;

    //Trigger Bools
    public bool isPlayerInStaircase;
    public bool isPlayerInLightswitch;
    public bool isPlayerInFusebox;
    public bool isPlayerInGenerator;
    public bool isPlayerInElevator;

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

    //Enum: Player Facing Direction
    private enum PlayerDirection { Left, Right }

    //PlayerDirection: Facing Dir
    private PlayerDirection dir;

    
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

        //Set Default Direction
        dir = PlayerDirection.Right;
    }

    private void Update()
    {
        //Set the Animator Bool in Animator
        animator.SetBool("isPlayerWalking", isPlayerWalking);

        //Check to make sure Player is not Viewing Full Level
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
        }

        //Flip Sprite Based on player walk direction
        switch (dir)
        {
            //Right
            case PlayerDirection.Right:
                //Facing Right
                transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            //Left
            case PlayerDirection.Left:
                //Facing Left
                transform.eulerAngles = new Vector3(0, 180, 0);
                break;
        }

        //Set up Player Facing Direction
        if(Input.GetAxis("Horizontal") > 0)
        {
            //Player Facing Right
            dir = PlayerDirection.Right;
        } else if(Input.GetAxis("Horizontal") < 0)
        {
            //Player Facing Left
            dir = PlayerDirection.Left;
        } 

    }

    private void FixedUpdate()
    {
        //Player is NOT viewing full level
        if (!isViewingFullLevel) { 
        
            //Call Player Movement
            movementModule.Movement(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed, 0));
        }
    }

    public void SetCameraPosition(RoomModule spawnRoom)
    {
        //Set Camera to current Room position
        cam.transform.position = spawnRoom.pos.transform.position;
    }
}

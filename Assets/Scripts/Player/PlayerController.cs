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

    //Trigger Bools
    public bool isPlayerInStaircase;
    public bool isPlayerInLightswitch;
    public bool isPlayerInFusebox;
    public bool isPlayerInGenerator;

    //Bool: Checks Player Walking
    public bool isPlayerWalking;

    //Disabled
    public bool isFlashLightEnabled;

    //Float: Input Deadzone
    float deadZone = 0.001f;

    //Animator
    public Animator animator;

    private void Awake()
    {
        //Assign RigidBody Component
        rb = GetComponent<Rigidbody2D>();

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        animator.SetBool("isPlayerWalking", isPlayerWalking);
    }

    private void FixedUpdate()
    {
        //Call Player Movement

        if(Input.GetAxis("Horizontal") > deadZone && Input.GetAxis("Horizontal") > 0 ||
            Input.GetAxis("Horizontal")  < 0 && Input.GetAxis("Horizontal") < deadZone)
        {
            isPlayerWalking = true;
        } else
        {
            isPlayerWalking = false;
        }

        movementModule.Movement(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed, 0));
    }


}

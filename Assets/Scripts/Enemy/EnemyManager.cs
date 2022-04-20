using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    //Enemy State Machines
    public enum EnemyState { Roaming, Chasing, Idle, Attacking, Fleeing };
    public EnemyState enemyState;

    //Player GameObject
    private GameObject player;

    //Enemy Current Room
    private RoomModule currentRoom;

    //Room Controller
    private RoomController roomController;

    //Enum: Enemy Facing Direction
    private enum EnemyDirection { Left, Right }

    //EnemyDirection: Facing Dir
    private EnemyDirection dir;

    //Enemy Move Speed
    public float moveSpeed;

    public float maxPlayerDist;

    //Start Room & End Room
    public RoomModule startRoom;
    private RoomModule endRoom;
    public RoomModule moveRoom;

    private RoomModule fleeRoom;

    //Bool: Check if Location Reached
    private bool reachedlocation;
    public bool fleeLocationSet = false;

    public float timer = 0;
    public float maxTime;
        //For Sound
    public AudioSource enemyNoises;
    public AudioClip roaming;
    public AudioClip jumpscareNoise;
    private Transform playerPos;
    public float soundDistMin;
    public float soundDistMax;

    //Current Enemy Path
    public List<RoomModule> currentPath = new List<RoomModule>();

    private void Start()
    {
        //Start Enemy Idle
        enemyState = EnemyState.Idle;

        //Set Player Gameobject
        player = GameObject.FindGameObjectWithTag("Player");

        roomController = GameObject.FindGameObjectWithTag("RoomController").GetComponent<RoomController>();
    }


    private void Update()
    {
        //Current Room Light is On!
        if (currentRoom.isLightOn)
        {
            //Flee
            enemyState = EnemyState.Fleeing;
        }

        //Chase Enemy
        if (Vector2.Distance(transform.position, player.transform.position) < maxPlayerDist)
        { 
            enemyState = EnemyState.Chasing;
        }

        
        //For Sound and Volume Based On Distance
        playerPos = player.transform;
            if(!enemyNoises.isPlaying){
                enemyNoises.Play();
            }
        else{
            enemyNoises.Pause();
        }

        float dist = Vector3.Distance(transform.position, playerPos.position);
        if(dist < soundDistMin){
            enemyNoises.volume = 1;
        }

        if(dist>soundDistMax){
            enemyNoises.volume = 0;
        }
        else{
            enemyNoises.volume = 1 - ((dist - soundDistMin) / (soundDistMax - soundDistMin));
        }

        switch (enemyState)
        {
            case EnemyState.Roaming:
                //Start Roaming Randomly
                RandomRoam();
                break;
            case EnemyState.Chasing:
                //Start Chasing Player
                ChasePlayer();
                break;
            case EnemyState.Fleeing:
                //Flee Room when Light Turned On
                //Random Neighbor Room to Flee to

                if (fleeRoom == null)
                {
                     fleeRoom = currentRoom.theseNeighbors[0];

                    //Clear Path
                    currentPath.Clear();

                    //Set Reached Destination to False
                    reachedlocation = false;

                }
                //Check if Reached Target
                if (Vector2.Distance(transform.position, fleeRoom.pos.transform.position) > 0.1)
                {

                    //Move Direction
                    if (fleeRoom.pos.transform.position.x < transform.position.x)
                    {
                        //Set Enemy Direction
                        dir = EnemyDirection.Left;

                    }
                    else if (fleeRoom.pos.transform.position.x > transform.position.x)
                    {
                        //Set Enemy Direction
                        dir = EnemyDirection.Right;
                    }


                    //Move to Target Position
                    transform.position = Vector2.MoveTowards(transform.position, fleeRoom.pos.transform.position, moveSpeed * Time.deltaTime);
                }
                else
                {
                    //Position Reached
                    reachedlocation = true;

                    enemyState = EnemyState.Idle;

                    fleeRoom = null;
                }
                break;
            case EnemyState.Attacking:
                //Attack Player - Start
                break;
            case EnemyState.Idle:
                if(timer >= maxTime && currentRoom.isLightOn == false)
                {
                    enemyState = EnemyState.Roaming;

                    timer = 0;
                }
                    else
                {
                    //Add 1 To Timer
                    timer = timer + 1 * Time.deltaTime;
                }
                break;
        }

        //Flip Sprite Based on move walk direction
        FacingDirection();
    }



    public void RandomRoam()
    {
        if (!fleeLocationSet)
        {
            //Generate Random Room
            moveRoom = roomController.rooms[Random.Range(0, roomController.rooms.Count - 1)].GetComponent<RoomModule>();

            fleeLocationSet = true;
            PathFind(moveRoom);
        } else
        {
                PathFind(moveRoom);
        }
    }

    public void ChasePlayer()
    {
         PathFind(player.GetComponent<PlayerController>().currentRoom);
    }
    

    public void AttackPlayer()
    {
        //TODO Play Attack Animation and Jump at Player!
    }

    public void PathFind(RoomModule eRoom)
    {
        if (reachedlocation)
        {
            //Set Current Room
            startRoom = currentRoom;

            //Create a Path towards the player
            currentPath = Pathfinder.Pathfind(startRoom, eRoom);
        }

        if (currentPath.Count > 0)
        {
            //Check if Reached Target
            if (Vector3.Distance(transform.position, currentPath[0].CurrentPos()) > 0.001)
            {

                //Move Direction
                if (currentPath[0].transform.position.x < transform.position.x)
                {
                    //Set Enemy Direction
                    dir = EnemyDirection.Left;

                }
                else if (currentPath[0].transform.position.x > transform.position.x)
                {
                    //Set Enemy Direction
                    dir = EnemyDirection.Right;
                }

                //Move to Target Position
                transform.position = Vector2.MoveTowards(transform.position, currentPath[0].CurrentPos(), moveSpeed * Time.deltaTime);

                //Position Not Reached
                reachedlocation = false;

                //Can't Go Light is ON
                if (currentPath[0].isLightOn)
                {
                    currentPath.Clear();

                    reachedlocation = true;
                }
            }
            else
            {
                //Removed From List because Location Reached
                currentPath.RemoveAt(0);
            }
        }
        else
        {
            //Position Reached
            reachedlocation = true;

            if(enemyState == EnemyState.Roaming)
            {
                fleeLocationSet = false;
                enemyState = EnemyState.Idle;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check for Current Room
        if (collision.gameObject.tag == "Room")
        {
            currentRoom = collision.gameObject.GetComponent<RoomModule>();
        }

        if(collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("JumpScare");
            enemyNoises.clip = jumpscareNoise;
            for(int i = 0; i < 1; i++){
            enemyNoises.Stop();
            enemyNoises.Play();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Check for Current Room
        if (collision.gameObject.tag == "Room")
        {
            //Set Current Room
            currentRoom = collision.gameObject.GetComponent<RoomModule>();
        }
    }

    private void FacingDirection()
    {
        //Flip Sprite Based on move walk direction
        switch (dir)
        {
            //Right
            case EnemyDirection.Right:
                //Facing Right
                transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            //Left
            case EnemyDirection.Left:
                //Facing Left
                transform.eulerAngles = new Vector3(0, 180, 0);
                break;
        }
    }
}

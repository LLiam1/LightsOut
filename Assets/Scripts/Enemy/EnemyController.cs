using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    //Enemy State Machines
    public enum EnemyState { Roaming, Chasing, Idle, Attacking, Fleeing };

    public EnemyState enemyState;

    public RoomModule currentRoom;

    //Start and End Room For Pathing
    public RoomModule startRoom;
    public RoomModule endRoom;

    //Current Enemy Path
    public List<RoomModule> currentPath = new List<RoomModule>();

    private Rigidbody2D rb;
    public bool reachedlocation = true;
    public bool roamLocation = true;

    public RoomModule roamRoom;

    public bool isRoamRoomset = false;

    public RoomController roomController;

    public float timer = 0;
    public float maxTime;

    public float maxDist;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        enemyState = EnemyState.Idle;
    }

    private void Update()
    {
        if (currentRoom.isLightOn)
        {
            enemyState = EnemyState.Fleeing;
        }
        if (Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= maxDist)
        {
            //Stop Chasing
            enemyState = EnemyState.Chasing;

        } else if(enemyState != EnemyState.Idle)
        {
            enemyState = EnemyState.Roaming;
        } else
        {
            enemyState = EnemyState.Idle;
        }
       
        switch (enemyState)
        {
            case EnemyState.Roaming:
                Debug.Log("Roaming");
                StartCoroutine(Roaming());
                break;
            case EnemyState.Chasing:
                StartCoroutine(ChasePlayer());
                break;
            case EnemyState.Idle:
                //Do Nothing for X Seconds
                if(timer >= maxTime && currentRoom.isLightOn == false)
                {
                    enemyState = EnemyState.Roaming;

                    timer = 0;
                } else
                {
                    //Add 1 To Timer
                    timer = timer + 1 * Time.deltaTime;
                }
                break;
            case EnemyState.Attacking:
                //Attack Player
                Debug.Log("Player Attacked! Game Over");
                break;

            case EnemyState.Fleeing:
                //Random Neighbor Room to Flee to
                RoomModule roomDest = currentRoom.theseNeighbors[0];

                //Clear Path
                currentPath.Clear();

                //Set Reached Destination to False
                reachedlocation = false;

                //Check if Reached Target
                if (Vector3.Distance(transform.position, roomDest.transform.position) > 0.001)
                {
                    float step = 3 * Time.deltaTime;

                    //Move to Target Position
                    transform.position = Vector2.MoveTowards(transform.position, roomDest.transform.position, step);
                }
                else
                {
                    //Position Reached
                    reachedlocation = true;

                    enemyState = EnemyState.Idle;
                }
                break;
        }
    }

    IEnumerator ChasePlayer()
    {
        //Check if Location is Reached
        if (reachedlocation)
        {
            //Set Current Room
            startRoom = currentRoom;

            //Create a Path towards the player
            currentPath = Pathfinder.Pathfind(currentRoom, GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().currentRoom);
        }

        if (currentPath.Count > 0)
        {
            //Check if Reached Target
            if (Vector3.Distance(transform.position, currentPath[0].CurrentPos()) > 0.001)
            {
                float step = 5 * Time.deltaTime;

                //Move to Target Position
                transform.position = Vector2.MoveTowards(transform.position, currentPath[0].CurrentPos(), step);

                //Position Not Reached
                reachedlocation = false;

                //Can't Go Light is ON
                if (currentPath[0].isLightOn)
                {
                    currentPath.Clear();

                    reachedlocation = true;

                    StopCoroutine(ChasePlayer());
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

            if (currentRoom.isLightOn)
            {
                enemyState = EnemyState.Roaming;
            }
            else
            {
                enemyState = EnemyState.Attacking;
            }
            yield return null;
        }
    }

    IEnumerator Roaming()
    {
        //Check if Location is Reached
        if (roamLocation)
        {
            //Set Current Room
            startRoom = currentRoom;

            //Set Roam Room
            roamRoom = roomController.rooms[Random.Range(0, roomController.rooms.Count - 1)].GetComponent<RoomModule>();


            //Create a Path towards the player
            currentPath = Pathfinder.Pathfind(currentRoom, roamRoom);

        }

        if (currentPath.Count > 0)
        {
            //Check if Reached Target
            if (Vector3.Distance(transform.position, currentPath[0].CurrentPos()) > 0.001)
            {
                float step = 5 * Time.deltaTime;

                //Move to Target Position
                transform.position = Vector2.MoveTowards(transform.position, currentPath[0].CurrentPos(), step);

                //Can't Go Light is ON
                if (currentPath[0].isLightOn)
                {
                    currentPath.Clear();

                    roamLocation = true;
                    StopCoroutine(Roaming());
                }

                //Position Not Reached
                roamLocation = false;
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
            roamLocation = true;

            enemyState = EnemyState.Idle;
            yield return null;
        }
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Get Current Room
        if(collision.gameObject.tag  == "Room")
        {
            currentRoom = collision.gameObject.GetComponent<RoomModule>();
        }


        //Enemy Found Player
        if(collision.gameObject.tag == "Player" && currentRoom.isLightOn == false)
        {
            //Attack Player
            enemyState = EnemyState.Attacking;

        } 

        if(collision.gameObject.tag == "Player")
        {
            //Attack Player
            Destroy(collision.gameObject);
            
        } 
    }
}


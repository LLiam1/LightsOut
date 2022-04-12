using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    //Gameobject[] of Entry Rooms
    public GameObject[] entryRooms;

    //Gameobject[] of Bottom Rooms
    public GameObject[] bottomRooms;

    //Gameobject[] of Top Rooms
    public GameObject[] topRooms;

    //Gameobject[] of Left Rooms
    public GameObject[] leftRooms;

    //Gameobject[] of Right Rooms
    public GameObject[] rightRooms;

    //Gameobject of ClosedRoom
    public GameObject closedRoom;

    //Gameobject Parent
    public GameObject roomParent;

    //Gameobject Fusebox Prefab 
    public GameObject fuseboxPrefab;

    //Gamobject Elevator Prefab
    public GameObject elevatorPrefab;

    //Gameobject Generator Prefab
    public GameObject generatorPrefab;

    //List of Rooms Gameobjects
    public List<GameObject> rooms = new List<GameObject>();

    //Max number of rooms
    public int maxRooms = 10;

    //Current Room Count (Excludes entry room)
    public int currentRoomCount = 0;

    //Room Setup Complete
    private bool isLevelSetupCompleted = false;

    //Trap room Assign
    private bool isTrapRoomAssigned = false;

    //FuseRoom Assigned
    private bool isFuseRoomAssigned = false;

    //Elevator Room Assigned
    private bool isElevatorRoomAssigned = false;

    //Timer to Check last spawned room
    public float roomSpawnTimer = 0f;

    //Max Room Spawner Time
    private const float MAX_ROOM_SPAWN_TIME = 5f;

    //Number of Generator Buttons
    public int numGenButtons = 3;

    //Number of Generator Buttons Assigned
    public int numGenButtonsAssigned = 0;

    //Button Generators For Elevator
    public bool isAllGeneratorButtonAssigned = false;

    //Game Controller
    public GameController gameController;

    public GameObject[,] levelRooms;

    public bool isTutorialLevel;

    public void Start()
    {
        if (gameController.randomlyGenerateRooms)
        {
            //Get Random Entry Room
            int rand = Random.Range(0, entryRooms.Length);

            //Instantiate Entry Room
            Instantiate(entryRooms[rand], Vector3.zero, entryRooms[rand].transform.rotation, roomParent.transform);

            //Set Room to be Entry Room
            entryRooms[rand].GetComponent<RoomModule>().isEntryRoom = true;
        }
    }

    public void Update()
    {
        if (gameController.randomlyGenerateRooms)
        {
            //Check if Level Setup
            if (!isLevelSetupCompleted)
            {
                //Check If Time between room spawn 
                if (roomSpawnTimer >= MAX_ROOM_SPAWN_TIME)
                {
                    //Setup Rooms
                    SetupRooms();
                }
                else
                {
                    //Timer
                    roomSpawnTimer = roomSpawnTimer + 1 * Time.deltaTime;
                }
            }

        }

        //DevTools to Reset Level
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }

        
    }


    public void SetupRooms()
    {
        //Check If Trap Room Assigned
        //if (!isTrapRoomAssigned)
        //{
        //    //Assign Trap Room
        //    AssignTrapRoom();
        //}

        ////Check if Elevator Room Assigned
        //if (!isElevatorRoomAssigned)
        //{
        //    //Assign Elevator Room
        //    AssignElevatorRoom();
        //}

        ////Check if Fuse Room Assigned
        //if (!isFuseRoomAssigned)
        //{
        //    //Assign Fuse Room
        //    AssignFuseRoom();
        //}

        //if (!isAllGeneratorButtonAssigned)
        //{
        //    for (int i = 0; i <= numGenButtons; i++)
        //    {
        //        AssignGeneratorRooms();
        //    }
        //}


        foreach (GameObject r in GameObject.FindGameObjectsWithTag("Room"))
        {
            rooms.Add(r);
        }

        int rand = Random.Range(0, rooms.Count - 1);

        //Setup Light Switches
        gameController.lightController.SetupLights();

        //Level Setup Complete
        isLevelSetupCompleted = true;
    }
    private void AssignTrapRoom()
    {
        //Get Random Number
        int rand = Random.Range(0, rooms.Count - 1);

        //Make Sure Room is NOT Elevator or Fuse or Entry
        if (rooms[rand].GetComponent<RoomModule>().isElevatorRoom
            || rooms[rand].GetComponent<RoomModule>().isFuseRoom
            || rooms[rand].GetComponent<RoomModule>().isEntryRoom)
        {
            //Cannot Assign Room 
            return;
        }

        //Set Room to be Trap Room
        rooms[rand].GetComponent<RoomModule>().isTrapRoom = true;

        //Trap Room has been Assigned
        isTrapRoomAssigned = true;
    }

    private void AssignFuseRoom()
    {
        //Lowest Y Level Room
        GameObject lowestYRoom = rooms[0];

        for (int i = 0; i <= rooms.Count - 1; i++)
        {
            //Check Room Y position
            if (rooms[i].transform.position.y < lowestYRoom.transform.position.y
                && rooms[i].gameObject.GetComponent<RoomModule>().isEntryRoom == false
                && rooms[i].gameObject.GetComponent<RoomModule>().isFuseRoom == false
                && rooms[i].gameObject.GetComponent<RoomModule>().isElevatorRoom == false)
            {
                //Set NEW lowest Room.
                lowestYRoom = rooms[i].gameObject;
            }
        }

        //Set Fuse Room to True
        lowestYRoom.gameObject.GetComponent<RoomModule>().isFuseRoom = true;

        //Instantiate Fusebox
        lowestYRoom.gameObject.GetComponent<RoomModule>().SpawnFusebox(lowestYRoom);
    }


    private void AssignElevatorRoom()
    {
        //Highest Y Level Room
        GameObject highestYRoom = rooms[0];

        for (int i = 0; i <= rooms.Count - 1; i++)
        {
            //Check Room Y position
            if (rooms[i].transform.position.y > highestYRoom.transform.position.y
                && rooms[i].gameObject.GetComponent<RoomModule>().isEntryRoom == false
                && rooms[i].gameObject.GetComponent<RoomModule>().isFuseRoom == false
                && rooms[i].gameObject.GetComponent<RoomModule>().isElevatorRoom == false)
            {
                //Set NEW highest Room.
                highestYRoom = rooms[i].gameObject;
            }
        }

        //Set Fuse Room to True
        highestYRoom.gameObject.GetComponent<RoomModule>().isElevatorRoom = true;

        //Instantiate Elevator
        highestYRoom.gameObject.GetComponent<RoomModule>().SpawnElevator(highestYRoom);


    }


    private void AssignGeneratorRooms()
    {
        if (numGenButtons != numGenButtonsAssigned)
        {
            //Get Random Number
            int rand = Random.Range(0, rooms.Count - 1);

            //Make Sure Room is NOT Elevator or Fuse or Entry
            if (rooms[rand].GetComponent<RoomModule>().isElevatorRoom
                || rooms[rand].GetComponent<RoomModule>().isFuseRoom
                || rooms[rand].GetComponent<RoomModule>().isEntryRoom
                || rooms[rand].GetComponent<RoomModule>().isGenerator)
            {
                //Cannot Assign Room 
                return;
            }

            //Set Room to be Generator Room
            rooms[rand].GetComponent<RoomModule>().isGenerator = true;

            //Increment Count
            numGenButtonsAssigned++;

            //Spawn Generator button
            rooms[rand].GetComponent<RoomModule>().SpawnGeneratorButton(rooms[rand]);
        }

        //Check if ALL buttons are spawned
        if (numGenButtons == numGenButtonsAssigned)
        {
            //All Generators are assigned
            isAllGeneratorButtonAssigned = true;
        }
        else
        {
            AssignGeneratorRooms();
        }
    }



    //Developer Tools: TODO - Better to put this in the GameController
    private void ResetLevel()
    {
        //Remove All Rooms
        for (int i = 0; i <= rooms.Count - 1; i++)
        {
            //Destroy Gameobject
            Destroy(rooms[i].gameObject);
        }

        //Clear List
        rooms.Clear();

        //Gather All Closed Rooms
        GameObject[] closedRooms = GameObject.FindGameObjectsWithTag("ClosedRooms");

        //Remove All Closed Rooms
        for (int i = 0; i <= closedRooms.Length - 1; i++)
        {
            //Destroy the GameObject
            Destroy(closedRooms[i].gameObject);
        }

        //Destroy Player
        Destroy(GameObject.FindGameObjectWithTag("Player").gameObject);

        //Current Room Count 
        currentRoomCount = 0;

        //Room Setup Complete
        isLevelSetupCompleted = false;

        //Trap room Assign
        isTrapRoomAssigned = false;

        //FuseRoom Assigned
        isFuseRoomAssigned = false;

        //Elevator Room Assigned
        isElevatorRoomAssigned = false;

        //Timer to Check last spawned room
        roomSpawnTimer = 0;

        //Get Random Entry Room
        int rand = Random.Range(0, entryRooms.Length);

        //Instantiate Entry Room
        Instantiate(entryRooms[rand], Vector3.zero, entryRooms[rand].transform.rotation, roomParent.transform);

        //Set Room to be Entry Room
        entryRooms[rand].GetComponent<RoomModule>().isEntryRoom = true;

    }


    private void GenerateLevel()
    {
        //Preset Row/Col Size
        int row = 5;
        int col = 5;

        levelRooms = new GameObject[row, col];

        
        for(int i = 0; i <= col; i++)
        {
            //Go Through Row.
            
        }
    }
}

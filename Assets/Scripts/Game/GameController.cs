using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class GameController : MonoBehaviour { 

    //Player Prefab
    public GameObject playerPrefab;

    //Enemy Prefab
    public GameObject enemyPrefab;

    //Light Controller
    public LightController lightController;

    //Room Controller
    private RoomController roomController;

    //Bool Blown Fuse
    public bool isFuseBlown = false;

    //Bool is Generators Active
    public bool isElevatorActive = false;

    //Bool Randomly Generate Rooms (ON/OFF)
    public bool randomlyGenerateRooms = true;

    //Parent Canvas
    public Transform uiParent;

    //Int: Generator Count
    public int generatorActiveCount = 0;

    //Bool: Tracks if Game is Over
    public bool isGameOver = false;

    //Bool: Tracks if Player Won
    public bool isWinner = false;

    //Player Spawn Room
    public RoomModule playerSpawnRoom;

    //Enemy Spawn Room
    public RoomModule enemySpawnRoom;

    public Sprite offLightSwitch;
    public Sprite onLightSwitch;

    private void Start()
    {
        //Get Light Controller
       lightController = GameObject.FindGameObjectWithTag("LightController").GetComponent<LightController>();

        //Get Room Controller
        roomController = GameObject.FindGameObjectWithTag("RoomController").GetComponent<RoomController>();

        //Randomly Generate Rooms
        if (!randomlyGenerateRooms)
        {
            //Setup Rooms
            roomController.SetupRooms();
        }

        //Spawn Player
        SpawnPlayer(playerSpawnRoom);

        //Enemy Spawn
        SpawnEnemy();
    }

    private void Update()
    {

        //Activate Help Menu
        if (isGameOver) {
            //TODO Display Win Screen
        }

        if(generatorActiveCount >= 3)
        {
            isElevatorActive = true;
        }

    }

    //Play Spawn Function
    public void SpawnPlayer(RoomModule spawnRoom)
    {
        Vector3 spawnPos = new Vector3(spawnRoom.transform.position.x, spawnRoom.transform.position.y + 3.25f, spawnRoom.transform.position.z);

        //Instantiate Player
        GameObject player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);

        player.GetComponent<PlayerController>().SetCameraPosition(spawnRoom);
    }

    private void SpawnEnemy()
    {
        //Spawn Room 
        Vector3 spawnPos = enemySpawnRoom.transform.position;

        spawnPos.y += 5.15f;

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}

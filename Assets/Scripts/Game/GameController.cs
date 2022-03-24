using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class GameController : MonoBehaviour { 

    //Player Prefab
    public GameObject playerPrefab;

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

    //Bool: Help Key Menu Activate
    public bool isHelpButtonActive;

    //Help Button Panel
    public GameObject buttonHelpMenu;

    //Help Button Text
    public TMP_Text helpButtonText;

    //Parent Canvas
    public Transform uiParent;

    //Int: Generator Count
    public int generatorActiveCount = 0;

    //Bool: Tracks if Game is Over
    public bool isGameOver = false;

    //Bool: Tracks if Player Won
    public bool isWinner = false;

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
    }

    private void Update()
    {

        //Activate Help Menu
        if (isGameOver) {
            if (isWinner)
            {
                SetHelpMenuText("You Escaped!");
            }  else
            {
                SetHelpMenuText("Game Over!");
            }
        }

        if(generatorActiveCount >= 3)
        {
            isElevatorActive = true;
        }

        buttonHelpMenu.SetActive(isHelpButtonActive);
    }

    //Play Spawn Function
    public void SpawnPlayer(RoomModule spawnRoom)
    {
        Vector3 spawnPos = new Vector3(spawnRoom.transform.position.x, spawnRoom.transform.position.y + 3.25f, spawnRoom.transform.position.z);

        //Instantiate Player
        GameObject player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);

        player.GetComponent<PlayerController>().SetCameraPosition(spawnRoom);
    }

    public void SetHelpMenuText(string text)
    {
        helpButtonText.text = text;
    }
}

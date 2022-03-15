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
    public bool isGeneratorsActive = false;

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

    //Int
    public int generatorActiveCount = 0;

    public bool isGameOver = false;

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
            isGeneratorsActive = true;
        }

        buttonHelpMenu.SetActive(isHelpButtonActive);
    }

    //Play Spawn Function
    public void SpawnPlayer(Vector3 pos)
    {
        //Instantiate Player
        Instantiate(playerPrefab, pos, Quaternion.identity);
    }

    public void SetHelpMenuText(string text)
    {
        helpButtonText.text = text;
    }
}

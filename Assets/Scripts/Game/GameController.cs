using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    public bool isEnemyActive = true;

    // the image you want to fade, assign in inspector
    public Image img;

    private GameObject fadeOBJ;

    public float fadeSpeed;

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


        if (isEnemyActive)
        {
            //Enemy Spawn
            SpawnEnemy();
        }

        fadeOBJ = GameObject.FindGameObjectWithTag("FadeImage");

        img = GameObject.FindGameObjectWithTag("FadeImage").GetComponent<Image>();

        StartCoroutine(FadeImage(true));

        Scene scene = SceneManager.GetActiveScene();

        //Current Scene
        PlayerPrefs.SetString("LastScene", scene.name);

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

        if (isWinner)
        {
            StartCoroutine(FadeImage(true));
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

        spawnPos.y += 5.10f;

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime * fadeSpeed)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime * fadeSpeed)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }

        fadeOBJ.SetActive(false);
    }
}


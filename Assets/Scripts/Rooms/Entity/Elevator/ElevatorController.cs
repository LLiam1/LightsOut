using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{

    private GameController gameController;
    public bool isElevatorHere;

    private bool isElevatorOpen;

    public GameObject elevatorObject;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        isElevatorHere = false;
        
    }

    // Update is called once per frame
    public void UseElevator()
    {
        //if(elev)
       if(isElevatorHere == false){
           elevatorObject.GetComponent<ElevatorScript>().enabled = true;
           elevatorObject.SetActive(true);
       }
       if(isElevatorHere == true){
                //Debug
            gameController.isGameOver = true;
            gameController.isWinner = true;

            Debug.Log("You Escaped the Level!");
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (gameController.isElevatorActive && collision.gameObject.tag == "Player")
        // {
        //     //Debug
        //     gameController.isGameOver = true;
        //     gameController.isWinner = true;

        //     gameController.SetHelpMenuText("Game Over");

        //     Debug.Log("You Escaped the Level!");
        // }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        elevatorObject.GetComponent<ElevatorScript>().enabled = false;
        elevatorObject.SetActive(false);
    }
}

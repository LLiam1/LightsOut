using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameController.isGeneratorsActive && collision.gameObject.tag == "Player")
        {
            //Debug
            gameController.isGameOver = true;
            gameController.isWinner = true;

            gameController.SetHelpMenuText("Game Over");

            Debug.Log("You Escaped the Level!");
        }
    }
}

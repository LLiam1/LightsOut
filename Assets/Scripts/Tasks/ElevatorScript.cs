using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorScript : MonoBehaviour
{

    public float timeUntilArrival; 
    public float startTime;
    public bool isMoving;

    public Text elevatorStatus; 

    string elevatorMoveStatus;
    

    void Start(){
        timeUntilArrival = startTime;
    }

    void Awake()
    {
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving == true){
            timeUntilArrival -= Time.deltaTime;
            elevatorMoveStatus = "Moving";
        }

        else{
            elevatorMoveStatus = "Not Moving";
        }


    }

    public void ElevatorGo(){
        isMoving = true;

    }
}

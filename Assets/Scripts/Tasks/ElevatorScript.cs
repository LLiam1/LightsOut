using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorScript : MonoBehaviour
{

    public float timeUntilArrival; 
    public float startTime;
    public bool isMoving;

    public bool isElevatorActive;

    public Text elevatorStatus; 
    float randomStop;

    

    void Start(){
        timeUntilArrival = startTime;
        isElevatorActive = false; 
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
            elevatorStatus.text = "Moving";
            Debug.Log(randomStop);
            if(timeUntilArrival <= randomStop){
                isMoving = false;
            }
            
        }

        else{
            elevatorStatus.text = "Not Moving";
        }


    }

    public void ElevatorGo(){
        isMoving = true;
        for(int i = 0; i < 1; i++){
           randomStop = Random.Range(-15, timeUntilArrival);
        }

        Debug.Log("test");
    }
}

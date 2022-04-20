using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorScript : MonoBehaviour
{

    public float timeUntilArrival; 
    public float startTime;
    public bool isMoving;

    public bool isHere;
    public bool isElevatorActive;

    public Text elevatorStatus; 
    float randomStop;

    
    public AudioSource sound;
    public AudioClip moving;
    public AudioClip stopped;
    public AudioClip here;

    

    void Start(){
        timeUntilArrival = startTime;
        isElevatorActive = false; 
        isHere = false;
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
                sound.clip = stopped;
                sound.Play();
                sound.loop = false;
            }

            if(timeUntilArrival <= 0){
                isMoving = false;
                isHere = true;
                sound.clip = here;
                sound.Play();
                sound.loop = false;
            }
            
        }

        else{
            elevatorStatus.text = "Not Moving";
            

        }


    }

    public void ElevatorGo(){
        isMoving = true;
        if(!sound.isPlaying){
            sound.clip = moving;
            sound.loop = true;
            sound.Play();
            }
        for(int i = 0; i < 1; i++){
           randomStop = Random.Range(-15, timeUntilArrival);
        }

        Debug.Log("test");
    }
}

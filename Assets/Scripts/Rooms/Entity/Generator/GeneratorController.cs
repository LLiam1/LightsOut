using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{ 
    //Generator Popup Window
    public GameObject popupPrefab;

    public AudioSource sound;
    private GameObject player;
    private Transform playerPos;
    public float soundDistMin;
    public float soundDistMax;

    public GameObject highlight;

    //Generator Status
    public bool isGeneratorActive = false;

    //Current Popup
    public GameObject currentPopup;
    public GameObject popupScript;

    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

    }

    public void Update()
    {
        isGeneratorActive = popupScript.GetComponent<GeneratorScript>().isTaskCompleted;

        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isPlayerInGenerator)
        {
            currentPopup.SetActive(false);
        }

<<<<<<< Updated upstream
        if (isGeneratorActive)
        {
            currentPopup.SetActive(false);
        }
=======
        //For Sound and Volume Based On Distance
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform;
        if(isGeneratorActive == true){
            if(!sound.isPlaying){
                sound.Play();
            }
        }
        else{
            sound.Pause();
        }

        float dist = Vector3.Distance(transform.position, playerPos.position);
        if(dist < soundDistMin){
            sound.volume = 1;
        }

        if(dist>soundDistMax){
            sound.volume = 0;
        }
        else{
            sound.volume = 1 - ((dist - soundDistMin) / (soundDistMax - soundDistMin));
        }

>>>>>>> Stashed changes

    }


    public void DisplayWindow()
    {
        if (!isGeneratorActive)
        {
            if (!popupScript.GetComponent<GeneratorScript>().isTaskCompleted)
            {
                if (currentPopup.activeSelf)
                {
                    currentPopup.SetActive(false);
                }
                else
                {
                    currentPopup.SetActive(true);
                }
            }
        }
    }

    //Just for presentation until we can figure this out.
    public void ScoreOverRide(){
        if(isGeneratorActive == false){
            gameController.generatorActiveCount++;
            isGeneratorActive = true;
            sound.Play();
        }
        else{
           return;
        }
    }

    public void DisplayPopupWindow()
    {

        isGeneratorActive = true;

        gameController.generatorActiveCount++;

        //Instantiate Popup
        currentPopup = Instantiate(popupPrefab, gameController.uiParent.transform.position, Quaternion.identity, gameController.uiParent.transform);
        

        //Display Appropriate Amounts
        //currentPopup.GetComponentInChildren<GenPopupController>().progressbar.value = currentActivationAmt;
        //popupPrefab.GetComponentInChildren<GenPopupController>().progressbar.maxValue = maxActivationAmt;
    }
}

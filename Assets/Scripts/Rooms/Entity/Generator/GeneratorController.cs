using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{ 
    //Generator Popup Window
    public GameObject popupPrefab;

    public AudioSource sound;

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

        if (isGeneratorActive)
        {
            currentPopup.SetActive(false);
        }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{ 
    //Generator Popup Window
    public GameObject popupPrefab;

    public AudioSource sound;

    //Generator Status
    public bool isGeneratorActive = false;

    //Current Popup
    public GameObject currentPopup;

    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void Update()
    {
        
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    //TODO: REDO THIS CLAS
    //Generator Popup Window
    public GameObject popupPrefab;

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

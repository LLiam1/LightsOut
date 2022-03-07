using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    //TODO: REDO THIS CLASS


    //Generator Popup Window
    public GameObject popupPrefab;

    //Current Activation Value
    public float currentActivationAmt;

    //Maximium Activation Amount
    public float maxActivationAmt = 50;

    //Generator Status
    public bool isGeneratorActive = false;

    //Current Popup
    public GameObject currentPopup;

    public void Update()
    {
        //Verify Popup is Activated
        if (currentPopup != null)
        {
            //Assign Current Amount
            //currentActivationAmt = currentPopup.GetComponentInChildren<GenPopupController>().progressbar.value;

            ////Check if it is more than the maximium.
            //if (currentActivationAmt >= maxActivationAmt)
            //{
            //    //Generator is Active
            //    isGeneratorActive = true;

            //    //Now Destroy the Popup
            //    Destroy(currentPopup);
            //}
        }
    }

    public void DisplayPopupWindow()
    {
        //Instantiate Popup
        currentPopup = Instantiate(popupPrefab, transform.position, Quaternion.identity);

        //Display Appropriate Amounts
        //currentPopup.GetComponentInChildren<GenPopupController>().progressbar.value = currentActivationAmt;
        //popupPrefab.GetComponentInChildren<GenPopupController>().progressbar.maxValue = maxActivationAmt;
    }
}

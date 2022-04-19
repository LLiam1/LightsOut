using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuseBoxScript : MonoBehaviour
{
    public Text targetValText;
    //public Text inputCode;
    public int targetVal; 
    public int currentVal; 
    Toggle currentToggle;

    public Image toggleImage;
    public Sprite toggleUp;
    public Sprite toggleDown;

    private GameController gameController; 
    
    void Start(){

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        currentVal = 0;
    }
    public void OnEnable(){
        targetVal = Random.Range(1, 78);
        this.enabled = true;

        //inputCode.text = string.Empty;
    }

    public void GetToggle(Toggle thisToggle){
        currentToggle = thisToggle;
        toggleImage = thisToggle.GetComponent<Image>();
        toggleImage.sprite = toggleUp;
    }

    public void ToggleClick(int number){
        if(currentToggle.isOn == true){
            currentVal += number;
            toggleImage.sprite = toggleDown;
        }

        if(currentToggle.isOn == false){
            currentVal -= number;
            toggleImage.sprite = toggleUp;
        }


    }

    void Update(){
        targetValText.text = "Target: " + targetVal + " | Current: " + currentVal ;
        //sDebug.Log("Target Val: " + targetVal + " | CurrentVal: " + currentVal);

        if(targetVal == currentVal){
            gameController.isFuseBlown = false;
        }
    }
}

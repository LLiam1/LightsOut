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

    public Image sr;
    public Sprite toggleUp;
    public Sprite toggleDown;
    
    void Start(){
        currentVal = 0;
    }
    public void OnEnable(){
        targetVal = Random.Range(1, 78);
        sr.sprite = toggleUp;

        //inputCode.text = string.Empty;
    }

    public void GetToggle(Toggle thisToggle){
        currentToggle = thisToggle;
        sr = thisToggle.GetComponent<Image>();
    }

    public void ToggleClick(int number){
        if(currentToggle.isOn == true){
            currentVal += number;
            sr.sprite = toggleDown;

        }

        if(currentToggle.isOn == false){
            currentVal -= number;
            sr.sprite = toggleUp;
        }


    }

    void Update(){
        targetValText.text = "Target Val: " + targetVal + " | CurrentVal: " + currentVal ;
    }
}

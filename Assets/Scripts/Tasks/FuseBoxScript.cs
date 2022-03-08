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

    void Start()
    {
        currentVal = 0;
    }
    public void OnEnable()
    {
        targetVal = Random.Range(1, 78);

        //inputCode.text = string.Empty;
    }

    public void GetToggle(Toggle thisToggle)
    {
        currentToggle = thisToggle;
    }

    public void ToggleClick(int number)
    {
        if (currentToggle.isOn == true)
        {
            currentVal += number;
        }

        if (currentToggle.isOn == false)
        {
            currentVal -= number;
        }


    }

    void Update()
    {
        targetValText.text = "Target Val: " + targetVal + " | CurrentVal: " + currentVal;
    }
}
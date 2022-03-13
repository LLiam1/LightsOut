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

    private GameController gameController;

    void Start()
    {
        currentVal = 0;

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
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


            ColorBlock colors = currentToggle.colors;

            colors.normalColor = Color.green;

            currentToggle.colors = colors;
        }

        if (currentToggle.isOn == false)
        {
            currentVal -= number;
            
            ColorBlock colors = currentToggle.colors;

            colors.normalColor = Color.white;

            currentToggle.colors = colors;
        }
    }

    void Update()
    {
        if (gameController.isFuseBlown) {
            targetValText.text = "Target Val: " + targetVal + " | CurrentVal: " + currentVal;
        }

        if(currentVal == targetVal)
        {
            //Fuse Fixed
            gameController.isFuseBlown = false;

            //Fuse Fixed
            targetValText.text = "FuseBox Reset! Lights Activated!";
        }
    }
}
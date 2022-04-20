using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //GameObjec: Main Menu 
    public GameObject mainMenu;


    //GameObject: Credits Menu
    public GameObject creditsMenu;

    private void Start()
    {
        DisplayMainMenu();
    }


    public void DisplayMainMenu()
    {
        //Set Main Menu Active
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);

    }

    public void DisplayCredits()
    {
        //Set Main Menu Active
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene("tutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

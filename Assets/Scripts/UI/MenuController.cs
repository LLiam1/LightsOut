using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //GameObjec: Main Menu 
    public GameObject mainMenu;

    //GameObject: Level Selector
    public GameObject levelSelector;

    //GameObject: Settings Menu
    public GameObject settingsMenu;

    //Screne: Array of Levels
    public Scene[] levels;

    private void Start()
    {
        DisplayMainMenu();
    }


    private void DisplayMainMenu()
    {
        //Set Main Menu Active
        mainMenu.SetActive(true);

        //Disable other Menus
        levelSelector.SetActive(false);
        settingsMenu.SetActive(false);

    }


}

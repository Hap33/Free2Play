using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {


    #region Instance

    public static UIManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    #endregion

    

    //Loads the Main Menu Scene
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    //Loads the Level Selection Scene
    public void LoadLevelSelection()
    {
        SceneManager.LoadScene("Level_Selection");
    }

    //Loads the Shop Scene
    public void LoadShop()
    {
        SceneManager.LoadScene("Shop");
    }

    //Loads the Game Scene
    public void LoadGame()
    {
        SceneManager.LoadScene("Proto");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Image boostLife;
    public Image boostSpeed;
    public Text timerText;

    private float timerSec;
    private int timerMin;

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

    private void Start()
    {
        timerSec = 0;
        timerMin = 0;
    }

    private void Update()
    {
        Timer();
        timerText.text = timerMin.ToString("00") + " : " + timerSec.ToString("00.00");
    }

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

    public void BoostUpdate(float boostForSpeed, float boostForLife)
    {
        boostLife.fillAmount = boostForLife;
        boostSpeed.fillAmount = boostForSpeed;
    }

    public void Timer()
    {
        timerSec += Time.deltaTime;
        if (timerSec >= 60)
        {
            timerSec = 0;
            timerMin += 1;
        }
    }
}
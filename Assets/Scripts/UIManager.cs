using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Image boostLife, boostSpeed, speedArrow;
    public Text timerText, textStart;

    private float timerSec;
    private float arrowY;
    private int timerMin;
    private int isReady;

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
        StartCoroutine(StartTimer());
        timerSec = 0;
        timerMin = 0;
        isReady = 0;
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
        timerSec += Time.deltaTime * isReady;
        if (timerSec >= 60)
        {
            timerSec = 0;
            timerMin += 1;
        }
    }

    public void CheckSpeed(float speed)
    {
        arrowY = speed * 250 + 30;
        speedArrow.transform.position = new Vector3(speedArrow.transform.position.x, arrowY, speedArrow.transform.position.z);
    }

    IEnumerator StartTimer()
    {
        textStart.text = "3";
        yield return new WaitForSeconds(1);
        textStart.text = "2";
        yield return new WaitForSeconds(1);
        textStart.text = "1";
        yield return new WaitForSeconds(1);
        textStart.text = "GO!";
        SpaceShip.instance.StartEngine();
        isReady = 1;
        yield return new WaitForSeconds(1);
        textStart.text = "";
    }
}
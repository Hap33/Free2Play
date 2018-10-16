﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerGame : MonoBehaviour {

    #region Instance

    public static UIManagerGame instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    #endregion

    public Image boostLife, boostSpeed, speedGrad;
    public Text timerText, textStart;

    private float timerSec;
    private float arrowY;
    private int timerMin;
    private int isReady;

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
        speedGrad.transform.position = new Vector3(speedGrad.transform.position.x, arrowY, speedGrad.transform.position.z);
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

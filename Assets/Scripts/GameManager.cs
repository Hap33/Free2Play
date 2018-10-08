using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Instance

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    #endregion

    public float timeLeft;

    private UIManager UImanager;
    private SpaceShip spaceShip;

    //Use this for initialization
    void Start () {
        UImanager = UIManager.instance;
        spaceShip = SpaceShip.instance;
	}
	
	//Update is called once per frame
	void Update () {

        //Decrease the timer
        timeLeft -= Time.deltaTime;
		
	}

    //Initiates the race
    private void InitRace()
    {

    }

    //Called when the race ends
    private void EndRace()
    {

    }
}
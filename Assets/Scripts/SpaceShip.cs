using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour {

    #region Instance

    public static SpaceShip instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    #endregion

    public enum GameMode { Time_Attack, Zone }; //Time_Attack = 0 / Zone = 1
    private enum States { Excellent, Good, Damaged, Broken }; //Excellent = 0 / Good = 1 / Damaged = 2 / Broken = 3

    public float[] maxSpeeds;
    public Material[] stateMaterials;
    public GameMode gameMode;

    private float speed, boostMax;
    private int nbStates;
    private bool isBoosting;
    private States states;

    //Use this for initialization
    void Start () {
        speed = 0;
        nbStates = 4;
        isBoosting = false;
	}
	
	//Update is called once per frame
	void Update () {
		
	}

    //Getter for boostMax
    public float BoostMax
    {
        get
        {
            return boostMax;
        }
    }
}

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

    public float acceleration;
    public float[] maxSpeeds;
    public Material[] stateMaterials;
    public GameMode gameMode;

    private float speed, boost, boostMax;
    private bool isBoosting;
    private States states;
    private GameManager gm;

    //Use this for initialization
    void Start () {
        speed = boost = 0;
        isBoosting = false;

        gm = GameManager.instance;
	}
	
	//Update is called once per frame
	void Update () {
		
	}

    //Used to move the SpaceShip
    private void Move(Vector3 direction)
    {

    }

    //Moves the actual state of the ship to a worse state
    private void DamageSpaceShip(int currentState)
    {
        //switch-case for different transitions
    }

    //Transitions from any state to the Excellent
    private void RepairSpaceShip()
    {

    }

    //Destroys the ship (usable only in Zone mode)
    private void DestroySpaceShip()
    {

    }

    //Returns a float between -1 for going left and 1 for going right using the gyroscope
    private float GetDirectionFromGyroscope()
    {
        return 0f;
    }

    //Returns a float between -1 for going left and 1 for going right using the accelerometer
    private float GetDirectionFromAccelerometer()
    {
        return 0f;
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

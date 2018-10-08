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
    private States state;
    private GameManager gm;

    //Use this for initialization
    void Start () {
        speed = boost = 0;
        isBoosting = false;

        gm = GameManager.instance;
	}
	
	//Update is called once per frame
	void Update () {
        Move();
	}

    //Used to move the SpaceShip
    private void Move()
    {
        float dir;

        if (Input.gyro.enabled == true)
        {
            dir = GetDirectionFromGyroscope();
        }
        else
        {
            dir = GetDirectionFromAccelerometer();
        }

        if (dir == 1f)
        {
            transform.Translate(Vector3.left * -0.5f);
            transform.Rotate(0, -3f, 0, 0);
        }
        else if (dir == -1f)
        {
            transform.Translate(Vector3.left * 0.5f);
            transform.Rotate(0, 3f, 0, 0);
        }
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
        float dir = 0;

        if (Input.gyro.attitude.y > 0.1f)
        {
            dir = 1f;
        }
        else if (Input.gyro.attitude.y < -0.1f)
        {
            dir = -1f;
        }
        
        return dir;
    }

    //Returns a float between -1 for going left and 1 for going right using the accelerometer
    private float GetDirectionFromAccelerometer()
    {
        float dir = 0f;

        if (Input.acceleration.x > 0.1f)
        {
            dir = 1f;
        }
        else if (Input.acceleration.x < 0.1f)
        {
            dir = -1f;
        }

        return dir;
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

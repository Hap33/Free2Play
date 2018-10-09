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
    public AnimationCurve accelerationCurve;

    private float speed, maxSpeed, boost, boostMax;
    private bool isBoosting;
    private States state;
    private GameManager gm;

    //Use this for initialization
    void Start () {
        speed = boost = 0;
        maxSpeed = maxSpeeds[0];
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
        int dir;

        //Gets direction from the gyro or
        if (Input.gyro.enabled == true)
            dir = GetDirectionFromGyroscope();
        else
            dir = GetDirectionFromAccelerometer();

        transform.Translate(Vector3.left * 0.5f * dir);
        transform.Rotate(0, 3f * dir, 0, 0);

        speed += maxSpeeds[0] * GetAcceleration();
        transform.Translate(0, 0, speed);
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
    private int GetDirectionFromGyroscope()
    {
        int dir = 0;

        if (Input.gyro.attitude.y > 0.2f)
        {
            dir = 1;
        }
        else if (Input.gyro.attitude.y < -0.2f)
        {
            dir = -1;
        }
        
        return dir;
    }

    //Returns a float between -1 for going left and 1 for going right using the accelerometer
    private int GetDirectionFromAccelerometer()
    {
        int dir = 0;

        if (Input.acceleration.x > 0.2f)
        {
            dir = 1;
        }
        else if (Input.acceleration.x < -0.2f)
        {
            dir = -1;
        }

        return dir;
    }

    //Returns the acceleration value [0; 1]
    private float GetAcceleration()
    {
        float clampSpeed = Mathf.Clamp01(speed / maxSpeed);
        return accelerationCurve.Evaluate(clampSpeed);
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

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
    public GameObject spaceShipAspect;
    public float sideSpeed;

    private float speed, maxSpeed, boost, boostMax, rotationZ;
    private bool isBoosting;
    private States state;
    private GameManager gm;

    //Use this for initialization
    void Start () {
        speed = boost = 0;
        state = States.Excellent;
        maxSpeed = maxSpeeds[(int)state];
        spaceShipAspect.GetComponent<MeshRenderer>().material = stateMaterials[0];
        isBoosting = false;

        gm = GameManager.instance;
	}
	
	//Update is called once per frame
	void Update () {
        Move();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            DamageSpaceShip((int)state);
            Destroy(collision.gameObject);
            speed /= 2;
        }
        
        if (collision.gameObject.CompareTag("Wall"))
        {
            speed /= 2;
        }
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
        
        transform.Rotate(0, 1.5f*dir, 0, 0);

        rotationZ = Mathf.Clamp(rotationZ, -20, 20);
        rotationZ = Mathf.MoveTowards(rotationZ, 0, Time.deltaTime * 30);
        rotationZ +=  dir;
        spaceShipAspect.transform.localEulerAngles = new Vector3(0, -90f, -rotationZ);


        speed += maxSpeeds[(int)state] * GetAcceleration();
        if(speed > maxSpeed)
        {
            speed = maxSpeed;
        }

        transform.Translate(sideSpeed * dir, 0, speed);
    }

    //Moves the actual state of the ship to a worse state
    private void DamageSpaceShip(int currentState)
    {
        currentState += 1;
        if (currentState > 3)
        {
            currentState = 3;
        }
        spaceShipAspect.GetComponent<MeshRenderer>().material = stateMaterials[currentState];
        state = (States)currentState;
        maxSpeed = maxSpeeds[currentState];
    }

    //Transitions from any state to the Excellent
    private void RepairSpaceShip()
    {
        state = (States)0;
        spaceShipAspect.GetComponent<MeshRenderer>().material = stateMaterials[0];
        maxSpeed = maxSpeeds[0];
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

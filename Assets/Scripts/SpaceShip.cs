﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public Material[] stateMaterials;
    public GameMode gameMode;
    public AnimationCurve accelerationCurve;
    public GameObject speedEffect, speedMotor, sparksWallHit;
    public GameObject[] spaceShipAspect, baseSpaceShip;
    public float sideSpeed, boostMultiplier, boostTimerBeforeBackToNormal, accelerationBoost, fovMax, turnSpeed;
    public float[] boostByState, maxSpeeds;
    public AudioClip soundSpeed, soundStart, threeSound, twoSound, oneSound, loopMusic, actualMusic;
    public AudioClip[] soundDamage;

    private AudioSource speedSoundSource, musicAndEffectsSound;
    private float speed, maxSpeed, boost, boostMax, rotationZ, boostBottom;
    private bool isBoosting;
    private States state;
    private GameManager gm;
    private bool hasEnded;
    private bool isStarting;
    
    //Use this for initialization
    void Start () {
        speedSoundSource = gameObject.GetComponent<AudioSource>();
        musicAndEffectsSound = Camera.main.GetComponent<AudioSource>();
        speedEffect.SetActive(false);
        speedMotor.SetActive(false);
        hasEnded = false;
        boostMax = 1;
        speed = boost = 0;
        state = States.Excellent;
        maxSpeed = maxSpeeds[(int)state];
        isBoosting = false;
        isStarting = false;
        hasEnded = false;
        musicAndEffectsSound.PlayOneShot(loopMusic);

        gm = GameManager.instance;
	}
	
	//Update is called once per frame
	void Update () {

        if (isStarting == false)
        {
            return;
        }

        speedSoundSource.PlayOneShot(soundSpeed);
        speedSoundSource.pitch = speed / 3.6f;

        if (Time.timeScale == 0)
        {
            speedSoundSource.Stop();
        }

        Move();
        UIManagerGame.instance.CheckSpeed(speed/5);
        //Checks if we swipe up
        if(Input.touchCount > 0 && Input.GetTouch(0).deltaPosition.y > 1.5f && boost > 0)
        {
            StartBoost();
        }

        BoostDraining();

        UIManagerGame.instance.BoostUpdate(boost, boostMax);

        CheckSpaceShip();
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasEnded == true)
        {
            return;
        }

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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Instantiate(sparksWallHit, collision.contacts[0].point, transform.rotation);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Boost"))
        {
            boost += Time.deltaTime * 0.2f;
            if (boost > boostMax)
            {
                boost = boostMax;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("EndTrigger"))
        {
            EndOfGame();
        }
        if (col.CompareTag("CheckPoint"))
        {
            RepairSpaceShip();
        }
    }

    //Used to move the SpaceShip
    private void Move()
    {
        if(hasEnded == true)
        {
            speedSoundSource.pitch = 0;
            transform.Translate(0, 0, speed * Time.deltaTime);
            return;
        }

        int dir;

        //Gets direction from the gyro or
        if (Input.gyro.enabled == true)
            dir = GetDirectionFromGyroscope();
        else
            dir = GetDirectionFromAccelerometer();
        
        transform.Rotate(0, 1.5f*dir* turnSpeed*Time.deltaTime*Time.timeScale, 0, 0);

        rotationZ = Mathf.Clamp(rotationZ, -20, 20);
        rotationZ = Mathf.MoveTowards(rotationZ, 0, Time.deltaTime * 30 * Time.timeScale);
        rotationZ +=  dir;
        spaceShipAspect[(int)state].transform.localEulerAngles = new Vector3(-rotationZ * Time.timeScale, -90f, 0);


        if (isBoosting == false)
        {
            speed += maxSpeeds[(int)state] * GetAcceleration()*0.1f;
            Camera.main.fieldOfView -= Time.deltaTime * 100;
            if (Camera.main.fieldOfView < 60)
            {
                Camera.main.fieldOfView = 60;
            }
        }

        if(speed > maxSpeed && isBoosting == false)
        {
            speed = maxSpeed;
        }

        transform.Translate(sideSpeed * dir * Time.deltaTime * Time.timeScale, 0, speed * Time.deltaTime * Time.timeScale);
    }

    public void CheckSpaceShip()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == (int)state)
            {
                spaceShipAspect[i].SetActive(true);
                continue;
            }
            spaceShipAspect[i].SetActive(false);
        }
    }

    //Moves the actual state of the ship to a worse state
    private void DamageSpaceShip(int currentState)
    {
        currentState += 1;
        if (currentState > 3)
        {
            currentState = 3;
        }
        state = (States)currentState;
        maxSpeed = maxSpeeds[currentState];
        boostMax = boostByState[currentState];
        speedSoundSource.Stop();
        musicAndEffectsSound.PlayOneShot(soundDamage[currentState]);
        if (boost > boostMax)
        {
            boost = boostMax;
        }
    }

    //Transitions from any state to the Excellent
    private void RepairSpaceShip()
    {
        boostMax = boostByState[0];
        state = (States)0;
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

    //Shows Score UI and Allow the player to quit, restart or go to the shop
    private void EndOfGame()
    {
        hasEnded = true;
        GameObject Camera;
        //call the UIManager to show the End UI and hide the Play UI
        Camera = this.gameObject.transform.GetChild(6).gameObject;
        Camera.transform.parent = null;
        //GameManager.instance.EndRace();
        UIManagerGame.instance.EndGame();
    }

    //Starts the game
    public void StartEngine()
    {
        isStarting = true;
    }

    //Adds the boost to our current speed
    public void StartBoost()
    {
        boostBottom = boost - 0.1f;
        isBoosting = true;
        speed = maxSpeeds[(int)state] * 2;
        speedEffect.SetActive(true);
        speedMotor.SetActive(true);
    }

    //Puts our speed back to what it was
    public void StopBoost()
    {
        isBoosting = false;
        speedEffect.SetActive(false);
        speedMotor.SetActive(false);
    }

    //Lower the boost meter when boosting
    public void BoostDraining()
    {
        if (isBoosting == true)
        {
            MovingFov();
            boost -= Time.deltaTime * boostTimerBeforeBackToNormal;
            if (boost < boostBottom)
            {
                boost = boostBottom;
                StopBoost();
            }
        }
    }

    //Getter for boostMax
    public float BoostMax
    {
        get
        {
            return boostMax;
        }
    }

    public void MovingFov()
    {

        Camera.main.fieldOfView += Time.deltaTime * 100;
        if (Camera.main.fieldOfView > fovMax)
        {
            Camera.main.fieldOfView = fovMax;
        }

    }

    public void StartCountdown()
    {
        StartCoroutine(StartSound());
    }

    public void ChooseModelBase(int skinId)
    {
        spaceShipAspect[0] = baseSpaceShip[skinId];
    }

    IEnumerator StartSound()
    {
        spaceShipAspect[(int)state].SetActive(true);
        musicAndEffectsSound.PlayOneShot(actualMusic);
        musicAndEffectsSound.PlayOneShot(threeSound);
        yield return new WaitForSeconds(1);
        musicAndEffectsSound.PlayOneShot(twoSound);
        yield return new WaitForSeconds(1);
        musicAndEffectsSound.PlayOneShot(oneSound);
        yield return new WaitForSeconds(1);
        musicAndEffectsSound.PlayOneShot(soundStart);
    }
}

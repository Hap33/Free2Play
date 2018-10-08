using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager_test : MonoBehaviour {

    public SpaceShip spaceShip;

    private Vector3 Direction;
    private float LowPassFilterFactor = (1 / 60) / 1;
    private Vector3 LowPassValue = Vector3.zero;

    //Use this for initialization
    private void Start()
    {
        LowPassValue = Input.acceleration;

        spaceShip = SpaceShip.instance;
    }

    //Update is called once per frame
    private void Update()
    {
        //Will be put in the ControlsManager
        if (Input.GetTouch(0).deltaPosition.x > 1.5f)
        {
            Debug.Log("Swipe vers la droite");
        }

        if (Input.GetTouch(0).deltaPosition.x < -1.5f)
        {
            Debug.Log("Swipe vers la gauche");
        }

        if (Input.GetTouch(0).deltaPosition.y < -1.5f)
        {
            Debug.Log("Swipe vers le bas");
        }

        if (Input.GetTouch(0).deltaPosition.y > 1.5f)
        {
            Debug.Log("Swipe vers le haut");
        }
    }

    private Vector3 LowPassFilterAccelerometer()
    {
        LowPassValue = Vector3.Lerp(LowPassValue, Input.acceleration, LowPassFilterFactor);
        return LowPassValue;
    }

    //Pauses the game
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    //Unpauses the game
    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }
}

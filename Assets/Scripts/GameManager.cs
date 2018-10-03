using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public SpaceShip spaceShip;

    private Vector3 Direction;
    private float LowPassFilterFactor = (1 / 60) / 1;
    private Vector3 LowPassValue = Vector3.zero;

    private void Start()
    {
        LowPassValue = Input.acceleration;

        spaceShip = SpaceShip.instance;
    }

    private void Update()
    {
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
    private void FixedUpdate()
    {
        Direction = Vector3.zero;
        Direction.x = Input.acceleration.x;
        Direction.y = Input.acceleration.y;
        if (Direction.sqrMagnitude > 1) { 
            Direction.Normalize();
        }
        Direction *= Time.deltaTime;
        spaceShip.transform.Translate(Direction * 10);
    }

    private Vector3 LowPassFilterAccelerometer()
    {
        LowPassValue = Vector3.Lerp(LowPassValue, Input.acceleration, LowPassFilterFactor);
        return LowPassValue;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }
}

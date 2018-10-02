using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Singleton;

    public GameObject Player;
    public GameObject Bomb;
    public GameObject Warning;
    public GameObject EndGame;
    public Text TextTime;

    private Vector3 Direction;
    private float LowPassFilterFactor = (1 / 60) / 1;
    private Vector3 LowPassValue = Vector3.zero;
    private float RandomX;
    private float RandomY;
    private Vector3 SpawnOfBomb;
    private float Scale;
    private float Seconds;
    private int SecondsInInt;
    private float CheckFifteen;

    private void Awake()
    {
        if(Singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Singleton = this;
        }
    }

    private void Start()
    {
        Bomb.transform.localScale = new Vector3(1, 1, 1);
        Warning.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        Scale = 0.1f;
        StartCoroutine(BombDrop());
        LowPassValue = Input.acceleration;
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
        Seconds += Time.deltaTime;
        SecondsInInt = Mathf.RoundToInt(Seconds);
        CheckTime();
        Direction = Vector3.zero;
        Direction.x = Input.acceleration.x;
        Direction.y = Input.acceleration.y;
        if (Direction.sqrMagnitude > 1) { 
            Direction.Normalize();
        }
        Direction *= Time.deltaTime;
        Player.transform.Translate(Direction * 10);
        CheckFifteen += Time.deltaTime;
        if (CheckFifteen >= 15)
        {
            BiggerAndBetter();
        }
    }

    private Vector3 LowPassFilterAccelerometer()
    {
        LowPassValue = Vector3.Lerp(LowPassValue, Input.acceleration, LowPassFilterFactor);
        return LowPassValue;
    }

    private IEnumerator BombDrop()
    {
        RandomX = Random.Range(-2.5f, 2.5f);
        RandomY = Random.Range(-4f, 5.5f);
        SpawnOfBomb = new Vector3(RandomX, RandomY, 0);
        yield return new WaitForSeconds(5);
        Instantiate(Warning, SpawnOfBomb, Player.transform.rotation);
        yield return new WaitForSeconds(2);
        Instantiate(Bomb, SpawnOfBomb, Player.transform.rotation);
        yield return new WaitForSeconds(4);
        StartCoroutine(BombDrop());
    }

    public void Death()
    {
        EndGame.SetActive(true);
        Time.timeScale = 0;
    }

    public void LoadThatScene(string scene)
    {
        if(scene == "Quit")
        {
            Application.Quit();
            return;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }

    private void CheckTime()
    {
        TextTime.text = SecondsInInt.ToString() + " Seconds";
    }

    private void BiggerAndBetter()
    {
        Bomb.transform.localScale += new Vector3(Scale, Scale, Scale);
        Warning.transform.localScale += new Vector3(Scale*1.5f, Scale*1.5f, Scale*1.5f);
        CheckFifteen = 0;
    }
}

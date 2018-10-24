using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerGame : MonoBehaviour {

    #region Instance

    public static UIManagerGame instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    #endregion

    [SerializeField]
    private TextMeshProUGUI timerTextMesh, textFinalTimeMesh;

    public Text speedText;
    public Image boostLife, boostSpeed, speedGrad, imageStart, imageSelectVehicle;
    public Sprite threeSprite, twoSprite, oneSprite, goSprite;
    public GameObject endGameScreen, selectButton;
    public Sprite[] selectVehicleSprite;

    private float timerSec, arrowY;
    private int timerMin, vehicleIndex;
    private int isReady;
    private bool isPaused, hasChosenVehicle;

    private void Start()
    {
        isPaused = false;
        hasChosenVehicle = false;
        timerSec = 0;
        timerMin = 0;
        isReady = 0;
        vehicleIndex = 0;
    }

    private void Update()
    {
        if (hasChosenVehicle)
        {
            Timer();
            timerTextMesh.text = timerMin.ToString("00") + " : " + timerSec.ToString("00.00");
        }

        if (SkinManager.instance.IsBought(vehicleIndex) == false)
        {
            imageSelectVehicle.sprite = selectVehicleSprite[3];
            selectButton.SetActive(false);
        }
        else
        {
            imageSelectVehicle.sprite = selectVehicleSprite[vehicleIndex];
            selectButton.SetActive(true);
        }
    }

    public void BoostUpdate(float boostForSpeed, float boostForLife)
    {
        boostLife.fillAmount = boostForLife;
        boostSpeed.fillAmount = boostForSpeed;
    }

    public void Timer()
    {
        timerSec += Time.deltaTime * isReady * Time.timeScale;
        if (timerSec >= 60)
        {
            timerSec = 0;
            timerMin += 1;
        }
    }

    public void CheckSpeed(float speed)
    {
        arrowY = speed * 250 + 30;
        speedGrad.transform.position = new Vector3(speedGrad.transform.position.x, 1000-arrowY, speedGrad.transform.position.z);
        float actualSpeed = speed * 500;
        speedText.text = actualSpeed.ToString("0");
    }

    public void EndGame()
    {
        timerTextMesh.enabled = false;
        endGameScreen.SetActive(true);
        textFinalTimeMesh.text = "Final Time : " + timerMin.ToString("00") + " : " + timerSec.ToString("00.00");
        isReady = 0;
    }

    public void PauseGame()
    {
        isPaused = !isPaused;

        if (!isPaused)
        {
            Time.timeScale = 1;
        }
        if (isPaused)
        {
            Time.timeScale = 0;
        }

    }

    public void RightChoice()
    {
        hasChosenVehicle = true;
        StartCoroutine(StartTimer());
    }

    public void LeftArrow()
    {
        vehicleIndex -= 1;
        if (vehicleIndex < 0)
        {
            vehicleIndex = 2;
        }
    }

    public void RightArrow()
    {
        vehicleIndex += 1;

        if (vehicleIndex > 2)
        {
            vehicleIndex = 0;
        }
    }

    IEnumerator StartTimer()
    {
        SpaceShip.instance.StartCountdown();
        imageStart.sprite = threeSprite;
        yield return new WaitForSeconds(1);
        imageStart.sprite = twoSprite;
        yield return new WaitForSeconds(1);
        imageStart.sprite = oneSprite;
        yield return new WaitForSeconds(1);
        imageStart.sprite = goSprite;
        SpaceShip.instance.StartEngine();
        isReady = 1;
        yield return new WaitForSeconds(1);
        imageStart.enabled = false;
    }

    IEnumerator TextChanging()
    {
        timerTextMesh.text = "This Vehicle is locked";
        yield return new WaitForSeconds(2);
        timerTextMesh.text = "Choose Your Vehicle";
    }
}

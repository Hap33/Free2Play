using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestGyro : MonoBehaviour {

    public GameObject Aspect;
    public Material[] StateMaterials = new Material[3];
    public Image BoostImage;

    /*private Vector3 Direction;
    private float LowPassFilterFactor = (1 / 60) / 1;
    private Vector3 LowPassValue = Vector3.zero;*/
    private float Vitesse = 0;
    private float VitMax = 4;
    private int State = 3;
    private bool IsBoosting = false;

    void Start()
    {
        Input.gyro.enabled = true;
        //LowPassValue = Input.acceleration;
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).deltaPosition.x != 0 && BoostImage.fillAmount == 1)
        {
            IsBoosting = true;
            Debug.Log("Swipe vers le haut");
        }

        if (IsBoosting == true)
        {
            Vitesse = 8;
            BoostImage.fillAmount -= Time.deltaTime * 0.5f;
            if (BoostImage.fillAmount == 0)
            {
                IsBoosting = false;
            }
        }

        //La vitesse qui augmente selon le temps
        Vitesse += Time.deltaTime * 0.5f;

        //Partie pour garder la vitesse Max
        if (Vitesse > VitMax && IsBoosting == false)
        {
            Vitesse = VitMax;
        }

        

        //Avancer
        transform.Translate(Vector3.forward*Vitesse);

        //Debug.Log(transform.rotation.z);
        Debug.Log(Input.gyro.attitude.y);
        if (Input.gyro.attitude.y > 0.1f)
        {
            transform.Translate(Vector3.left* Input.gyro.attitude.y);
            transform.Rotate(0, -Input.gyro.attitude.y * 6, 0, 0);
        }
        else if(Input.gyro.attitude.y < -0.1f)
        {
            transform.Translate(Vector3.left * Input.gyro.attitude.y);
            transform.Rotate(0, -Input.gyro.attitude.y * 6, 0, 0);
        }

        
        /*Direction = Vector3.zero;
        Direction.x = Input.acceleration.x;
        if (Direction.sqrMagnitude > 1)
        {
            Direction.Normalize();
        }
        Direction *= Time.deltaTime;*/

        //Déplacement du joueur selon le téléphone
        //TheGoodRotate.transform.Translate(Direction * 50);
        
        /*Aspect.transform.Rotate(0, 0, -Input.acceleration.x*0.5f);*/

        switch (State)
        {
            case 3:
                VitMax = 4;
                Aspect.GetComponent<MeshRenderer>().material = StateMaterials[0];
                break;
            case 2:
                VitMax = 2;
                Aspect.GetComponent<MeshRenderer>().material = StateMaterials[1];
                break;
            case 1:
                VitMax = 1;
                Aspect.GetComponent<MeshRenderer>().material = StateMaterials[2];
                break;
            default:
                gameObject.SetActive(false);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")|| collision.gameObject.CompareTag("Murs"))
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Destroy(collision.gameObject);
            }
            State -= 1;
            Vitesse = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Boost"))
        {
            BoostImage.fillAmount += 0.01f;
        }
    }
    
    /*private Vector3 LowPassFilterAccelerometer()
    {
        LowPassValue = Vector3.Lerp(LowPassValue, Input.acceleration, LowPassFilterFactor);
        return LowPassValue;
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGyro : MonoBehaviour {

    public GameObject TheGoodRotate;
    public GameObject Aspect;
    public Material[] StateMaterials = new Material[3];

    private Vector3 Direction;
    private float LowPassFilterFactor = (1 / 60) / 1;
    private Vector3 LowPassValue = Vector3.zero;
    private float Vitesse = 0;
    private int State = 3;

    void Start()
    {
        LowPassValue = Input.acceleration;
    }

    void Update()
    {
        Vitesse += Time.deltaTime * 0.1f;
        if (Vitesse > 4)
        {
            Vitesse = 4;
        }
        TheGoodRotate.transform.Translate(Vector3.forward*Vitesse);
        /*Debug.Log(Input.gyro.attitude);
        //Debug.Log(transform.rotation.z);
        transform.Rotate(0, 0, -Input.gyro.attitude.y*4, 0);
        if (transform.rotation.z > 0)
        {
            TheGoodRotate.transform.Translate(Vector3.left* transform.rotation.z);
        }
        else if(transform.rotation.z < 0)
        {
            TheGoodRotate.transform.Translate(Vector3.left * transform.rotation.z);
        }*/
        Direction = Vector3.zero;
        Direction.x = Input.acceleration.x;
        if (Direction.sqrMagnitude > 1)
        {
            Direction.Normalize();
        }
        Direction *= Time.deltaTime;
        TheGoodRotate.transform.Translate(Direction * 50);
        Aspect.transform.Rotate(0, 0, -Input.acceleration.x*0.5f);
    }

    private Vector3 LowPassFilterAccelerometer()
    {
        LowPassValue = Vector3.Lerp(LowPassValue, Input.acceleration, LowPassFilterFactor);
        return LowPassValue;
    }
}

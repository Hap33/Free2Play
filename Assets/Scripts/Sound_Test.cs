using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Sound_Test : MonoBehaviour {

    public AudioSource audioSource;

    //Sets the pitch to the value in parameter
    public void SetPitch(float _pitch)
    {
        audioSource.pitch = _pitch;
    }

    private void Update()
    {
        audioSource.Play();
    }
}

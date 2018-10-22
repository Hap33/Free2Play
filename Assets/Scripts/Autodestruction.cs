using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestruction : MonoBehaviour {

    public float secondsBeforeDestruction;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, secondsBeforeDestruction);
	}
}

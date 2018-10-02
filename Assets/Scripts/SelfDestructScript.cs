using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructScript : MonoBehaviour {

    public int Seconds;

	void Start () {
        StartCoroutine(DeathTime());
	}

    IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(Seconds);
        Destroy(gameObject);
    }
}

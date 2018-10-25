using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public float dissolveSpeed;
    public Material DissolveMaterial;
    public GameObject Wall;

    private Material m_DissolveMaterial;

    // Use this for initialization
    void Start () {
        Wall.GetComponent<MeshRenderer>().material = DissolveMaterial;
        m_DissolveMaterial = Wall.GetComponent<MeshRenderer>().material;
        m_DissolveMaterial.SetFloat("DissolveRate", 0f);

    }

    public void StartDissolveCoroutine()
    {
        StartCoroutine("StartDissolving");
    }
	
    private IEnumerator StartDissolving()
    {
        while(m_DissolveMaterial.GetFloat("DissovleRate") <= 1)
        {
            m_DissolveMaterial.SetFloat("DissolveRate", m_DissolveMaterial.GetFloat("DissovleRate") + (dissolveSpeed * Time.deltaTime));
            yield return null;
        }

        Destroy(gameObject);
    }
}

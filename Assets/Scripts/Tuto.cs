using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tuto : MonoBehaviour
{

    public void SceneLaunch(string nomScene)
    {
        SceneManager.LoadScene(nomScene);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public void SceneLaunch(string nomScene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nomScene);
    }

    public void SceneNext()
    {
        int newBuild = SceneManager.GetActiveScene().buildIndex + 1;
        if (newBuild > SceneManager.sceneCount)
        {
            SceneManager.LoadScene("Main_Menu");
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}

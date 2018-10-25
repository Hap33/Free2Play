using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public void SceneLaunch(string nomScene)
    {
        Time.timeScale = 1;

        StartCoroutine(LaunchSceneAsync(nomScene));
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

    public IEnumerator LaunchSceneAsync(string SceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

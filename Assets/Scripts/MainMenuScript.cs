﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_1 : MonoBehaviour {

    public void SceneLaunch(string nomScene)
    {
        SceneManager.LoadScene(nomScene);
    }
}

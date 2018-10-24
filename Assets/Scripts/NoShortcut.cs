using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoShortcut : MonoBehaviour {

    #region Instance
    public static NoShortcut instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    public void Destruction()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour {

    #region Instance
    public static SkinManager instance;

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
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    private int shipBought; // 0 = None, 1 = Ship 1 only, 2 = Ship 2 only, 3 = Both Ship;

    private void Start()
    {
        shipBought = 0;
    }

    // Checks if the Item is bought
    public bool IsBought(int shipID)
    {
        if (shipID == shipBought || shipID == 0 || shipBought == 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Have the 
    public void BuyShip(int shipID)
    {
        if (shipBought != 0 && shipID != shipBought)
        {
            shipBought = 3;
        }
        else
        {
            shipBought = shipID;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour {

    public Button BuyButton;
    public Sprite[] vehicleChoice;
    public Image imageVehicle;

    private int skinChoice;

    private void Update()
    {
        if (SkinManager.instance.IsBought(skinChoice))
        {
            imageVehicle.sprite = vehicleChoice[skinChoice];
            BuyButton.interactable = false;
        }
        else
        {
            imageVehicle.sprite = vehicleChoice[3];
            BuyButton.interactable = true;
        }
    }

    public void RightArrow()
    {
        skinChoice += 1;
        if (skinChoice > 2)
        {
            skinChoice = 0;
        }
    }

    public void LeftArrow()
    {
        skinChoice -= 1;
        if (skinChoice < 0)
        {
            skinChoice = 2;
        }
    }

    public void ChoseSkin()
    {
        SkinManager.instance.BuyShip(skinChoice);
    }
}

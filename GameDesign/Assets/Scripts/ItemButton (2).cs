using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Image buttonImage;
    public Text AmountText;
    public int ButtonValue;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        if (GameMenu.instance.TheMenu.activeInHierarchy)
        {
            if (GameManager.instance.ItemsBeingHeld[ButtonValue] != "")
            {
                GameMenu.instance.SelectItem(GameManager.instance.GetItemDetails(GameManager.instance.ItemsBeingHeld[ButtonValue]));
            }
        }

        if(Shop.Instance.ShopMenu.activeInHierarchy)
        {
            if (Shop.Instance.BuyMenu.activeInHierarchy)
            {
                Shop.Instance.SelectBuyItem(GameManager.instance.GetItemDetails(Shop.Instance.itemsforsale[ButtonValue]));
            }

            if(Shop.Instance.SellMenu.activeInHierarchy)
            {
                Shop.Instance.SelectSellItem(GameManager.instance.GetItemDetails(GameManager.instance.ItemsBeingHeld[ButtonValue]));
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop Instance;

    public GameObject ShopMenu;
    public GameObject BuyMenu;
    public GameObject SellMenu;

    public Text GoldText;

    public string[] itemsforsale;

    public ItemButton[] BuyItemButtons;
    public ItemButton[] SellItemButtons;

    public Item selectItem;
    public Text BuyItemName, BuyItemDescription, buyItemValue;
    public Text SellItemName, SellItemDescription, SellItemValue;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K) && !ShopMenu.activeInHierarchy)
        {
            OpenShop();
        }
    }

    public void OpenShop()
    {
        ShopMenu.SetActive(true);
        OpenBuyMenu();


        GameManager.instance.shopactive = true;

        GameMenu.instance.enabled = !GameMenu.instance.enabled;
        GoldText.text = GameManager.instance.CurrentGold.ToString() + "g";
    }

    public void CloseShop()
    {
        ShopMenu.SetActive(false);

        GameManager.instance.shopactive = false;
        GameMenu.instance.enabled = !GameMenu.instance.enabled;
    }

    public void OpenBuyMenu()
    {
        BuyItemButtons[0].Press();

        BuyMenu.SetActive(true);
        SellMenu.SetActive(false);

        for (int i = 0; i < BuyItemButtons.Length; i++)
        {

            BuyItemButtons[i].ButtonValue = i;
            //checks the gamemanager for the players inventory
            //as long as it is not empty then it will show the item in the inventory
            //else then it will show empty and the player will not be able to see it in their inventory
            if (itemsforsale[i] != "")
            {
                BuyItemButtons[i].buttonImage.gameObject.SetActive(true);

                BuyItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(itemsforsale[i]).itemSprite;

                BuyItemButtons[i].AmountText.text = "";
            }
            else
            {
                BuyItemButtons[i].buttonImage.gameObject.SetActive(false);
                BuyItemButtons[i].AmountText.text = "";
            }

        }
    }
    public void OpenSellMenu()
    {

        SellItemButtons[0].Press();

        BuyMenu.SetActive(false);
        SellMenu.SetActive(true);

        GameManager.instance.SortItems();

        ShowSellItems();
    }

    public void SelectBuyItem(Item buyItem)
    {
        selectItem = buyItem;
        if (selectItem != null)
        {
            BuyItemName.text = selectItem.ItemName;
            BuyItemDescription.text = selectItem.Description;
            buyItemValue.text = "Value: " + selectItem.Value + "g";
        }
        else
        {
            BuyItemName.text = "";
            BuyItemDescription.text = "";
            buyItemValue.text = "";
        }
    }

    public void SelectSellItem(Item SellItem)
    {
        selectItem = SellItem;

        if (selectItem != null)
        {
            SellItemName.text = selectItem.ItemName;
            SellItemDescription.text = selectItem.Description;
            SellItemValue.text = "Value: " + Mathf.FloorToInt(selectItem.Value * .5f).ToString() + "g";
        }
        else
        {
            SellItemName.text = "";
            SellItemDescription.text = "";
            SellItemValue.text = "";
        }

    }

    private void ShowSellItems()
    {
        for (int i = 0; i < SellItemButtons.Length; i++)
        {

            SellItemButtons[i].ButtonValue = i;
            //checks the gamemanager for the players inventory
            //as long as it is not empty then it will show the item in the inventory
            //else then it will show empty and the player will not be able to see it in their inventory
            if (GameManager.instance.ItemsBeingHeld[i] != "")
            {
                SellItemButtons[i].buttonImage.gameObject.SetActive(true);

                SellItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.ItemsBeingHeld[i]).itemSprite;

                SellItemButtons[i].AmountText.text = GameManager.instance.NumberofItems[i].ToString();
            }
            else
            {
                SellItemButtons[i].buttonImage.gameObject.SetActive(false);
                SellItemButtons[i].AmountText.text = "";
            }

        }
    }
    public void BuyItem()
    {
        if (selectItem != null)
        {

            if (GameManager.instance.CurrentGold >= selectItem.Value)
            {
                GameManager.instance.CurrentGold -= selectItem.Value;

                GameManager.instance.AddItem(selectItem.ItemName);
            }
        }

        GoldText.text = GameManager.instance.CurrentGold.ToString() +"g";
    }

    public void SellItem()
    {
        if(selectItem != null)
        {
            GameManager.instance.CurrentGold += Mathf.FloorToInt(selectItem.Value * .5f);


            GameManager.instance.RemoveItem(selectItem.ItemName);
        }

        GoldText.text = GameManager.instance.CurrentGold.ToString() +"g";
        ShowSellItems();
    }
}

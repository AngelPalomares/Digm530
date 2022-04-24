using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    #region Variables for the main menu;
    public static GameMenu instance;
    [SerializeField]
    public GameObject TheMenu;

    //Different menus for the ui
    public GameObject[] windows;

    //player stats;
    public CharStats[] playerstats;

    //information about the player
    public Text[] NameText, HPText, MPText, LevelText, ExperienceText;
    public Slider[] expSlider;
    public Image[] CharacterImage;
    public GameObject[] CharStatHolder;

    public Text statusname, statusHP, StatusMP, StatusStrength, StatusDefense, StatusWeaponEquipped,
        StatusWeaponPower, StatusArmorEquipped, StatusArmorPower, StatusExp;

    public GameObject[] statusbuttons;

    public string selectedItemd;
    public Item ActiveItem;
    public Text ItemName, ItemDescription, usebuttontext;
    #endregion;
    //the buttons that are used for items
    public ItemButton[] ItemButton;

    public GameObject itemCharChoiceMenu;
    public Text[] itemcharchoicename;
    public Text GoldText;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    #region Update for the game;
    // Update is called once per frame
    void Update()
    {
        if (!Shop.Instance.ShopMenu.activeInHierarchy)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                PlayerController.instance.canMove = false;
                if (TheMenu.activeInHierarchy)
                {
                    //TheMenu.SetActive(false);
                    //GameManager.instance.GamemenuOpen = false;

                    CloseMenu();
                }
                else
                {
                    PlayerController.instance.canMove = true;
                    TheMenu.SetActive(true);
                    UpdateMainStats();
                    GameManager.instance.GamemenuOpen = true;
                }
                AudioManager.instance.PlaySXF(0);
            }
        }
    }
    #endregion;
    public void UpdateMainStats()
    {
        playerstats = GameManager.instance.playerstats;

        for (int i = 0; i < playerstats.Length; i++)
        {
            if (playerstats[i].gameObject.activeInHierarchy)
            {
                CharStatHolder[i].SetActive(true);
                NameText[i].text = playerstats[i].CharName;
                HPText[i].text = "HP: " + playerstats[i].currentHP + "/" + playerstats[i].macHP;
                MPText[i].text = "MP: " + playerstats[i].currentMP + "/" + playerstats[i].MaxMP;
                LevelText[i].text = "LVL: " + playerstats[i].playerleverl;
                ExperienceText[i].text = "" + playerstats[i].CurrentExp + "/" + playerstats[i].exptonextlevel[playerstats[i].playerleverl];
                expSlider[i].maxValue = playerstats[i].exptonextlevel[playerstats[i].playerleverl];
                expSlider[i].value = playerstats[i].CurrentExp;
                CharacterImage[i].sprite = playerstats[i].CharacterImage;
            }
            else
            {
                CharStatHolder[i].SetActive(false);
            }
        }

        GoldText.text = GameManager.instance.CurrentGold.ToString() + "g";
    }

    public void ToggleWIndwo(int windowNumber)
    {
        UpdateMainStats();
        for (int i = 0; i < windows.Length; i++)
        {
            if (i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }

        itemCharChoiceMenu.SetActive(false);
    }

    public void CloseMenu()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        TheMenu.SetActive(false);

        GameManager.instance.GamemenuOpen = false;
        itemCharChoiceMenu.SetActive(false);
    }

    public void Openstatus()
    {
        UpdateMainStats();
        //update the information that is shown
        StatusCharater(0);


    }

    public void StatusCharater(int selected)
    {
        statusname.text = "Name: " + playerstats[selected].CharName;
        statusHP.text = "HP: " + playerstats[selected].currentHP + "/" + playerstats[0].macHP;
        StatusMP.text = "MP: " + playerstats[selected].currentMP + "/" + playerstats[0].MaxMP;
        StatusStrength.text = "Strength: " + playerstats[selected].Strength.ToString();
        StatusDefense.text = "Defense: " + playerstats[selected].Defence.ToString();

        if (playerstats[selected].equippedWeapon != "")
        {
            StatusWeaponEquipped.text = "Equpped Weapon " + playerstats[selected].equippedWeapon;
        }
        StatusWeaponPower.text = "Weapon Power: " + playerstats[selected].WeaponPower.ToString();

        if (playerstats[selected].EquippedArm != "")
        {
            StatusArmorEquipped.text = "Equpped Armor: " + playerstats[selected].EquippedArm;
        }

        StatusArmorPower.text = "Armor Power: " + playerstats[selected].armorpower.ToString();
        StatusExp.text = "Experience to next level " + (playerstats[selected].exptonextlevel[playerstats[selected].playerleverl] - playerstats[selected].CurrentExp).ToString();

    }

    public void QuitTheGame()
    {
        Application.Quit();
    }

    //function is used to show the items that are inside the inventory
    public void ShowItems()
    {
        GameManager.instance.SortItems();
        //for loop is used to go through the items that the character has
        for (int i = 0; i < ItemButton.Length; i++)
        {

            ItemButton[i].ButtonValue = i;
            //checks the gamemanager for the players inventory
            //as long as it is not empty then it will show the item in the inventory
            //else then it will show empty and the player will not be able to see it in their inventory
            if (GameManager.instance.ItemsBeingHeld[i] != "")
            {
                ItemButton[i].buttonImage.gameObject.SetActive(true);

                ItemButton[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.ItemsBeingHeld[i]).itemSprite;

                ItemButton[i].AmountText.text = GameManager.instance.NumberofItems[i].ToString();
            }
            else
            {
                ItemButton[i].buttonImage.gameObject.SetActive(false);
                ItemButton[i].AmountText.text = "";
            }

        }
    }

    public void SelectItem(Item NewItem)
    {
        ActiveItem = NewItem;

        if (ActiveItem.IsanItem)
        {
            usebuttontext.text = "Use";
        }

        if (ActiveItem.isweapon || ActiveItem.isArmour)
        {
            usebuttontext.text = "Equip";
        }

        ItemName.text = ActiveItem.ItemName;
        ItemDescription.text = ActiveItem.Description;
    }

    public void DiscardItem()
    {
        if(ActiveItem != null)
        {
            GameManager.instance.RemoveItem(ActiveItem.ItemName);
        }
    }

    public void OpenItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(true);

        for(int i = 0; i < itemcharchoicename.Length; i++)
        {
            itemcharchoicename[i].text = GameManager.instance.playerstats[i].CharName;
            itemcharchoicename[i].transform.parent.gameObject.SetActive(GameManager.instance.playerstats[i].gameObject.activeInHierarchy);
        }

    }

    public void CloseItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(false);
    }

    public void useItem(int selectChar)
    {
        ActiveItem.Use(selectChar);
        CloseItemCharChoice();
    }

    public void SaveTheGame()
    {
        GameManager.instance.savedata();
        QuestManager.instance.SaveQuestData();
    }

    public void PlayButtonSound()
    {
        AudioManager.instance.PlaySXF(2);
    }

    public void PlayItemButtonSound()
    {
        AudioManager.instance.PlaySXF(2);
    }
}

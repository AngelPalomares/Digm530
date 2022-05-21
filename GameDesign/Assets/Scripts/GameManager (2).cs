using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region GameStats;
    public static GameManager instance;
    public CharStats[] playerstats;
    public bool GamemenuOpen, dialogueActive,FadigBetweenAreas,shopactive,battleActive;
    [Header("Item Information")]
    public string[] ItemsBeingHeld;
    public int[] NumberofItems;
    public Item[] referenceItems;

    public int CurrentGold;
    #endregion;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        SortItems();
    }

    // Update is called once per frame
    void Update()
    {
        if(GamemenuOpen || dialogueActive|| FadigBetweenAreas || shopactive || battleActive)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }
    }

    public Item GetItemDetails(string ItemToGrab)
    {
        for(int i = 0; i < referenceItems.Length; i++)
        {
            if(referenceItems[i].ItemName == ItemToGrab)
            {
                return referenceItems[i];
            }
        }


        return null;
    }
    #region Sorting items for the player;
    public void SortItems()
    {
        bool itemAfterSpace = true;

        while (itemAfterSpace)
        {
            itemAfterSpace = false;
            for (int i = 0; i < ItemsBeingHeld.Length - 1; i++)
            {
                if (ItemsBeingHeld[i] == "")
                {
                    ItemsBeingHeld[i] = ItemsBeingHeld[i + 1];
                    ItemsBeingHeld[i + 1] = "";

                    NumberofItems[i] = NumberofItems[i + 1];
                    NumberofItems[i + 1] = 0;

                    if(ItemsBeingHeld[i] != "")
                    {
                        itemAfterSpace = true;
                    }
                }
            }
        }
    }
    #endregion;
    public void AddItem(string ItemtoAdd)
    {
        int newItemPosition = 0;
        bool foundspace = false;

        for(int i = 0; i < ItemsBeingHeld.Length; i++)
        {
            if(ItemsBeingHeld[i] == "" || ItemsBeingHeld[i] == ItemtoAdd)
            {
                newItemPosition = i;
                i = ItemsBeingHeld.Length;
                foundspace = true;
            }
        }

        if(foundspace)
        {
            bool itemexists = false;
            for(int i = 0; i < referenceItems.Length; i++)
            {
                if(referenceItems[i].ItemName == ItemtoAdd)
                {
                    itemexists = true;

                    i = referenceItems.Length;
                }
            }

            if (itemexists)
            {
                ItemsBeingHeld[newItemPosition] = ItemtoAdd;
                NumberofItems[newItemPosition]++;
            }
            else
            {
                Debug.LogError(ItemtoAdd + "Does not Exist!!");
            }
        }

        GameMenu.instance.ShowItems();
    }

    public void RemoveItem(string ItemToRemove)
    {
        bool foundItem = false;
        int itemposition = 0;

        for(int i = 0; i <ItemsBeingHeld.Length; i++)
        {
            if(ItemsBeingHeld[i] == ItemToRemove)
            {
                foundItem = true;
                itemposition = i;

                i = ItemsBeingHeld.Length;
            }
        }

        if (foundItem)
        {
            NumberofItems[itemposition]--;
            if(NumberofItems[itemposition] <= 0)
            {
                ItemsBeingHeld[itemposition] = "";
            }

            GameMenu.instance.ShowItems();
        }
        else
        {
            Debug.LogError("Couldnt find the item");
        }
    }

    public void savedata()
    {
        //saves the transform for the player
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_X", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_Y", PlayerController.instance.transform.position.y);

        //SaveCharacterInfo
        for(int i = 0; i < playerstats.Length; i++)
        {
            if(playerstats[i].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("Player_" + playerstats[i].CharName + "_active", 1);
            }
            else
                PlayerPrefs.SetInt("Player_" + playerstats[i].CharName + "_active", 0);
            #region Saves the player stats
            PlayerPrefs.SetInt("Player_" + playerstats[i].CharName + "_level", playerstats[i].playerleverl);
            PlayerPrefs.SetInt("Player_" + playerstats[i].CharName + "_CurrentExp", playerstats[i].CurrentExp);

            PlayerPrefs.SetInt("Player_" + playerstats[i].CharName + "_CurrentHP", playerstats[i].currentHP);
            PlayerPrefs.SetInt("Player_" + playerstats[i].CharName + "_CurrentMP", playerstats[i].currentMP);

            PlayerPrefs.SetInt("Player_" + playerstats[i].CharName + "_MaxHP", playerstats[i].macHP);
            PlayerPrefs.SetInt("Player_" + playerstats[i].CharName + "_MaxMP", playerstats[i].MaxMP);

            PlayerPrefs.SetInt("Player_" + playerstats[i].CharName + "_Strength", playerstats[i].Strength);
            PlayerPrefs.SetInt("Player_" + playerstats[i].CharName + "_Defence", playerstats[i].Defence);

            PlayerPrefs.SetInt("Player_" + playerstats[i].CharName + "_WeaponPower", playerstats[i].WeaponPower);
            PlayerPrefs.SetInt("Player_" + playerstats[i].CharName + "_ArmorPower", playerstats[i].armorpower);

            PlayerPrefs.SetString("Player_" + playerstats[i].CharName + "_EquippedArmor", playerstats[i].EquippedArm);
            PlayerPrefs.SetString("Player_" + playerstats[i].CharName + "_EquippedWeapon", playerstats[i].equippedWeapon);
            #endregion;
        }

        //stores the items for the player
        for(int i = 0; i < ItemsBeingHeld.Length; i++)
        {
            PlayerPrefs.SetString("ItemInInventory_" + i, ItemsBeingHeld[i]);
            PlayerPrefs.SetInt("ItemAmount_" + i, NumberofItems[i]);
        }
    }

    public void loaddata()
    {
        PlayerController.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_X"), PlayerPrefs.GetFloat("Player_Position_Y"));

        for(int i = 0; i < playerstats.Length; i++)
        {
            if(PlayerPrefs.GetInt("Player_" + playerstats[i].CharName + "_active") == 0)
            {
                playerstats[i].gameObject.SetActive(false);
            }
            else
            {
                playerstats[i].gameObject.SetActive(true);
            }

            playerstats[i].playerleverl = PlayerPrefs.GetInt("Player_" + playerstats[i].CharName + "_level");
            playerstats[i].CurrentExp = PlayerPrefs.GetInt("Player_" + playerstats[i].CharName + "_CurrentExp");

            playerstats[i].currentHP = PlayerPrefs.GetInt("Player_" + playerstats[i].CharName + "_CurrentHP");
            playerstats[i].currentMP = PlayerPrefs.GetInt("Player_" + playerstats[i].CharName + "_CurrentMP");

            playerstats[i].macHP = PlayerPrefs.GetInt("Player_" + playerstats[i].CharName + "_MaxHP");
            playerstats[i].MaxMP = PlayerPrefs.GetInt("Player_" + playerstats[i].CharName + "_MaxMP");

            playerstats[i].Strength = PlayerPrefs.GetInt("Player_" + playerstats[i].CharName + "_Strength");
            playerstats[i].Defence = PlayerPrefs.GetInt("Player_" + playerstats[i].CharName + "_Defence");

            playerstats[i].WeaponPower = PlayerPrefs.GetInt("Player_" + playerstats[i].CharName + "_WeaponPower");
            playerstats[i].armorpower = PlayerPrefs.GetInt("Player_" + playerstats[i].CharName + "_ArmorPower");

            playerstats[i].EquippedArm = PlayerPrefs.GetString("Player_" + playerstats[i].CharName + "_EquippedArmor");
            playerstats[i].equippedWeapon = PlayerPrefs.GetString("Player_" + playerstats[i].CharName + "_EquippedWeapon");
        }

        for(int i = 0; i < ItemsBeingHeld.Length; i++)
        {
            ItemsBeingHeld[i] = PlayerPrefs.GetString("ItemInInventory_" + i);
            NumberofItems[i] = PlayerPrefs.GetInt("ItemAmount_" + i);
        }
    }
}

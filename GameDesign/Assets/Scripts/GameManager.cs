using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region GameStats;
    public static GameManager instance;
    public CharStats[] playerstats;
    public bool GamemenuOpen, dialogueActive,FadigBetweenAreas;
    [Header("Item Information")]
    public string[] ItemsBeingHeld;
    public int[] NumberofItems;
    public Item[] referenceItems;
    #endregion;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(GamemenuOpen || dialogueActive|| FadigBetweenAreas)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            AddItem("Iron Armor");
            RemoveItem("Health Potion");
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
}

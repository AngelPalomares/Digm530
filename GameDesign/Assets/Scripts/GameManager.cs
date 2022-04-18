using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public CharStats[] playerstats;

    public bool GamemenuOpen, dialogueActive,FadigBetweenAreas;

    [Header("Item Information")]
    public string[] ItemsBeingHeld;
    public int[] NumberofItems;
    public Item[] referenceItems;

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
}

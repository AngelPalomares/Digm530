using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header ("Item Type")]
    public bool IsanItem;
    public bool isweapon;
    public bool isArmour;

    [Header("Item Details")]
    public string ItemName;
    public string Description;
    public int Value;
    public Sprite itemSprite;

    [Header("Item Details")]
    public int amountToChange;
    public bool effectHP, effectMP, effectSTR;

    [Header("Weapon/Armor Details")]
    public int WeaponStrentght;
    public int ArmorStrength;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

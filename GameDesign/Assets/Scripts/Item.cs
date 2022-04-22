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

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Uses the items;
    public void Use(int CharToUseOn)
    {
        CharStats selectedChar = GameManager.instance.playerstats[CharToUseOn];

        if(IsanItem)
        {
            if(effectHP)
            {
                selectedChar.currentHP += amountToChange;

                if(selectedChar.currentHP > selectedChar.macHP)
                {
                    selectedChar.currentHP = selectedChar.macHP;
                }
            }

            if (effectMP)
            {
                selectedChar.currentMP += amountToChange;

                if(selectedChar.currentMP  > selectedChar.MaxMP)
                {
                    selectedChar.currentMP = selectedChar.MaxMP;
                }
            }

            if(effectSTR)
            {
                selectedChar.Strength += amountToChange;
            }


        }

        if(isweapon)
        {
            if(selectedChar.equippedWeapon != "")
            {
                GameManager.instance.AddItem(selectedChar.equippedWeapon);
            }

            selectedChar.equippedWeapon = ItemName;
            selectedChar.WeaponPower = WeaponStrentght;

        }

        if (isArmour)
        {
            if(selectedChar.EquippedArm != "") 
            {
                GameManager.instance.AddItem(selectedChar.EquippedArm);
            }
            selectedChar.EquippedArm = ItemName;
            selectedChar.armorpower = ArmorStrength;
        }

        GameManager.instance.RemoveItem(ItemName);
    }
    #endregion;
}

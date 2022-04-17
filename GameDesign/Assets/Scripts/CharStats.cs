using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    [SerializeField]
    public string CharName;
    //Current Level of the player
    [SerializeField]
    public int playerleverl = 1;
    //curent Experience of the player
    [SerializeField]
    public int CurrentExp, baseEXP = 1000;

    [SerializeField]
    public int[] exptonextlevel, MPlvlBonus;

    [SerializeField]
    private int maxlevel = 100;
    //stats of the player
    [SerializeField]
    public int currentHP, macHP = 100, currentMP, MaxMP = 30, Strength, Defence, WeaponPower, armorpower;

    //name of the equipped items
    [SerializeField]
    public string equippedWeapon, EquippedArm;

    [SerializeField]
    public Sprite CharacterImage;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = macHP;
        currentMP = MaxMP;
        exptonextlevel = new int[maxlevel];

        MPlvlBonus = new int[100];

        exptonextlevel[1] = baseEXP;

        MPlvlBonus[1] = 9;
        
        for(int i = 2; i < exptonextlevel.Length; i++)
        {
            exptonextlevel[i] = Mathf.FloorToInt(exptonextlevel[i - 1] * 1.05f);
        }

        for (int i = 2; i < MPlvlBonus.Length; i++)
        {
            MPlvlBonus[i] = Mathf.FloorToInt(MPlvlBonus[i - 1] * 1.119f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.K))
        {
            Addexp(500);
        }
    }

    //This function is used to calculated the amount of experience that the player has gotten
    public void Addexp(int exptoadd)
    {
        CurrentExp += exptoadd;

        if (playerleverl < maxlevel)
        {
            if (CurrentExp > exptonextlevel[playerleverl])
            {
                CurrentExp -= exptonextlevel[playerleverl];

                playerleverl++;

                //determin whether to add to strentght or defense based on odd or even
                if (playerleverl % 2 == 0)
                {
                    Strength++;
                }
                else
                {
                    Defence++;
                }

                macHP = Mathf.FloorToInt(macHP * 1.05f);
                currentHP = macHP;

                MaxMP += MPlvlBonus[playerleverl];
                currentMP = MaxMP;

            }
        }

        if(playerleverl >= maxlevel)
        {
            CurrentExp = 0;
        }

    }
}

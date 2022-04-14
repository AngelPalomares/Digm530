using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    [SerializeField]
    private string CharName;
    //Current Level of the player
    [SerializeField]
    private int playerleverl = 1;
    //curent Experience of the player
    [SerializeField]
    private int CurrentExp;
    //stats of the player
    [SerializeField]
    private int currentHP, macHP = 100, currentMP, MaxMP = 30, Strength, Defence, WeaponPower;

    //name of the equipped items
    [SerializeField]
    private string equippedWeapon, EquippedArm;

    [SerializeField]
    private Sprite CharacterImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

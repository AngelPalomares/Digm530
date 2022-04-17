using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject TheMenu;

    public GameObject[] windows;

    public CharStats[] playerstats;

    public Text[] NameText, HPText, MPText, LevelText, ExperienceText;
    public Slider[] expSlider;
    public Image[] CharacterImage;
    public GameObject[] CharStatHolder;

    public Text statusname, statusHP, StatusMP, StatusStrength, StatusDefense, StatusWeaponEquipped,
        StatusWeaponPower, StatusArmorEquipped, StatusArmorPower, StatusExp;

    public GameObject[] statusbuttons;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (TheMenu.activeInHierarchy)
            {
                //TheMenu.SetActive(false);
                //GameManager.instance.GamemenuOpen = false;

                CloseMenu();
            }
            else
            {
                TheMenu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.GamemenuOpen = true;
            }
        }
    }

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
    }

    public void CloseMenu()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        TheMenu.SetActive(false);

        GameManager.instance.GamemenuOpen = false;

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

        StatusArmorPower.text = "Armor Power: " + playerstats[selected].WeaponPower.ToString();
        StatusExp.text = "Experience to next level " + (playerstats[selected].exptonextlevel[playerstats[selected].playerleverl] - playerstats[selected].CurrentExp).ToString();

    }
}

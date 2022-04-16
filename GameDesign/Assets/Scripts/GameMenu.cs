using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject TheMenu;

    public CharStats[] playerstats;

    public Text[] NameText, HPText,MPText,LevelText,ExperienceText;
    public Slider[] expSlider;
    public Image[] CharacterImage;
    public GameObject[] CharStatHolder;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if(TheMenu.activeInHierarchy)
            {
                TheMenu.SetActive(false);
                GameManager.instance.GamemenuOpen = false;
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

        for(int i = 0; i < playerstats.Length; i++)
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

}

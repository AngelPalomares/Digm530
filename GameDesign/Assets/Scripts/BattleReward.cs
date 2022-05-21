using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleReward : MonoBehaviour
{
    public static BattleReward instance;

    public Text XpText, ItemText;

    public GameObject rewardScreen;

    public string[] rewardItems;
    public int XPEarned;

    public bool MarkQuestComplete;
    public string QuestToMark;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            OpenRewardScreen(54, new string[] { "Health Potion", "Mana Potion" });
        }
    }

    public void OpenRewardScreen(int Xp,string[] rewards)
    {
        XPEarned = Xp;
        rewardItems = rewards;

        XpText.text = "Daniel earned " + XPEarned + " xp";
        ItemText.text = "";

        for(int i = 0; i < rewardItems.Length; i++)
        {
            ItemText.text += rewards[i] + "\n" ;
        }

        rewardScreen.SetActive(true);
    }

    public void CloseRewardScreen()
    {
        for(int i = 0; i < GameManager.instance.playerstats.Length; i++)
        {
            if(GameManager.instance.playerstats[i].gameObject.activeInHierarchy)
            {
                GameManager.instance.playerstats[i].Addexp(XPEarned);
            }
        }

        for(int i = 0; i < rewardItems.Length; i++)
        {
            GameManager.instance.AddItem(rewardItems[i]);
        }


        rewardScreen.SetActive(false);
        GameManager.instance.battleActive = false;

        if(MarkQuestComplete)
        {
            QuestManager.instance.MarkQuestComplete(QuestToMark);
        }
    }
}

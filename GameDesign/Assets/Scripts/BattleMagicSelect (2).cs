using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMagicSelect : MonoBehaviour
{
    public string SpellName;
    public int SpellCost;
    public Text NameText, CostText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        if (BattleManager.instance.activateBattlers[BattleManager.instance.currentTurn].CurrentMP >= SpellCost)
        {
            BattleManager.instance.MagicMenu.SetActive(false);
            BattleManager.instance.OpenTargetMenu(SpellName);
            BattleManager.instance.activateBattlers[BattleManager.instance.currentTurn].CurrentMP -= SpellCost;
        }
        else
        {
            //Let Player know there is not enough MP

            BattleManager.instance.BattleNotice.theTest.text = "NOT ENOUGH MP!!";

            BattleManager.instance.BattleNotice.Active();

            BattleManager.instance.MagicMenu.SetActive(false);
        }
    }
}

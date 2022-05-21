using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTargetButton : MonoBehaviour
{
    public string MoveName;
    public int activeBattleTarget;
    public Text TargetName;

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
        BattleManager.instance.PlayerAttack(MoveName,activeBattleTarget);
    }

}

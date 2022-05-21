using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStarter : MonoBehaviour
{
    public BattleType[] PotentialBattles;
    public bool activateOnEnter, ActivateOnStay, ActivateOnExit;
    private bool inArea;

    public float TimeBetweenBattleCounter = 10f;
    private float BetweenBattleCounter;

    public bool DeactivateAfterStarting;

    public bool CannotFlee;

    public bool ShouldCompleteQuest;
    public string QuestToComplete;
    // Start is called before the first frame update
    void Start()
    {
        BetweenBattleCounter = Random.Range(TimeBetweenBattleCounter *.5f, TimeBetweenBattleCounter * 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(inArea && PlayerController.instance.canMove)
        {
            if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                BetweenBattleCounter -= Time.deltaTime;
            }

            if(BetweenBattleCounter <= 0)
            {
                BetweenBattleCounter = Random.Range(TimeBetweenBattleCounter * .5f, TimeBetweenBattleCounter * 1.5f);
                StartCoroutine(StartBattleCo());
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(activateOnEnter)
            {
                StartCoroutine(StartBattleCo());
            }
            {
                inArea = true;
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(ActivateOnExit)
            {
                StartCoroutine(StartBattleCo());
            }
            else
            {
                inArea = false;
            }
        }
    }

    public IEnumerator StartBattleCo()
    {
        UIFade.instance.FadeToBlack();
        GameManager.instance.battleActive = true;


        int selectedBattle = Random.RandomRange(0, PotentialBattles.Length);

        BattleManager.instance.rewardItems = PotentialBattles[selectedBattle].rewardItems;
        BattleManager.instance.RewardXP = PotentialBattles[selectedBattle].RewardXP;

        yield return new WaitForSeconds((1.5f));

        BattleManager.instance.BattleStart(PotentialBattles[selectedBattle].enemies, CannotFlee);
        UIFade.instance.FadeFromBlack();

        if (DeactivateAfterStarting)
        {
            gameObject.SetActive(false);
        }

        BattleReward.instance.MarkQuestComplete = ShouldCompleteQuest;
        BattleReward.instance.QuestToMark = QuestToComplete;
    }
}

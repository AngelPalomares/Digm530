using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    #region Variables used for game
    public static BattleManager instance;

    public bool BattleActive;

    public GameObject BattleScene;

    public Transform[] PlayerPositions, EnemyPosition;

    public BattleChar[] PlayerPrefab, EnemyPrefab;

    public List<BattleChar> activateBattlers = new List<BattleChar>();

    public int currentTurn;
    public bool turnWaiting;

    public GameObject UIButtonHolder;

    public BattleMove[] MovesList;

    public GameObject EnemyAttactEffect;

    public DamageNumber TheDamgeNumber;

    public Text[] PlayerNames, PlayerHP, PlayerMP;

    public GameObject TargetMenu;

    public BattleTargetButton[] TargetButtons;

    public GameObject MagicMenu;

    public BattleMagicSelect[] MagicSelects;

    public BattleNotification BattleNotice;
    public int ChanceToFlee = 35;

    private bool Fleeing;

    public string GameOverScene;

    public int RewardXP;
    public string[] rewardItems;

    #endregion
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            BattleStart(new string[] { "Rimuru", "Rimuru" });
        }

        if(BattleActive)
        {
            if(turnWaiting)
            {
                if(activateBattlers[currentTurn].IsPlayer)
                {
                    UIButtonHolder.SetActive(true);
                }
                else
                {
                    UIButtonHolder.SetActive(false);

                    //enemy should Attack
                    StartCoroutine(EnemyMoveCo());
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.N))
        {
            NextTurn();
        }

    }

    #region Function that starts the battle
    public void BattleStart(string[] EnemiesToSpawn)
    {
        if(!BattleActive)
        {
            AudioManager.instance.PlayVGM(2);
            BattleActive = true;



            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
            GameManager.instance.battleActive = true;

            BattleScene.SetActive(true);

            //Dont forget to add music here
            #region Battle Playerstats
            for (int i = 0; i < PlayerPositions.Length; i++)
            {
                if(GameManager.instance.playerstats[i].gameObject.activeInHierarchy)
                {
                    for(int j = 0; j < PlayerPrefab.Length; j++)
                    {
                        if(PlayerPrefab[j].CharName == GameManager.instance.playerstats[i].CharName)
                        {
                            BattleChar newPlayer = Instantiate(PlayerPrefab[j], PlayerPositions[i].position,PlayerPositions[i].rotation);
                            newPlayer.transform.parent = PlayerPositions[i];

                            activateBattlers.Add(newPlayer);

                            CharStats thePlayer = GameManager.instance.playerstats[i];
                            activateBattlers[i].CurrentHP = thePlayer.currentHP;
                            activateBattlers[i].MaxHP = thePlayer.macHP;
                            activateBattlers[i].CurrentMP = thePlayer.currentMP;
                            activateBattlers[i].MaxMP = thePlayer.MaxMP;
                            activateBattlers[i].Strength = thePlayer.Strength;
                            activateBattlers[i].Defense = thePlayer.Defence;
                            activateBattlers[i].WeaponPower = thePlayer.WeaponPower;
                            activateBattlers[i].ArmorPower = thePlayer.armorpower;
                        }
                    }

                }
            }
            #endregion

            #region Enemy prefab locator
            for (int i = 0; i < EnemiesToSpawn.Length; i++)
            {
                if(EnemiesToSpawn[i] != "")
                {
                    for(int j = 0; j < EnemyPrefab.Length; j++)
                    {
                        if (EnemyPrefab[j].CharName == EnemiesToSpawn[i])
                        {
                            BattleChar newEnemy = Instantiate(EnemyPrefab[j], EnemyPosition[i].position,EnemyPosition[i].rotation);
                            newEnemy.transform.parent = EnemyPosition[i];
                            activateBattlers.Add(newEnemy);
                        }
                    }
                }
            }
            #endregion
            turnWaiting = true;
            currentTurn = 0;
            UpdateUIStats();
        }
    }
    #endregion
    #region Handles The Turn
    public void NextTurn()
    {
        currentTurn++;
        if(currentTurn >= activateBattlers.Count)
        {
            currentTurn = 0;
        }
        turnWaiting = true;
        UpdateBattle();
        UpdateUIStats();
    }
    #endregion
    #region Updates the battle
    public void UpdateBattle()
    {
        bool AllEnemiesDead = true;
        bool AllPlayersDead = true;

        for(int i = 0; i < activateBattlers.Count; i++)
        {
            if(activateBattlers[i].CurrentHP < 0)
            {
                activateBattlers[i].CurrentHP = 0;
            }

            if(activateBattlers[i].CurrentHP ==0)
            {
                //handle dead people
                if(activateBattlers[i].IsPlayer)
                {

                }
                else
                {
                    activateBattlers[i].EnemyFade();
                }

            }
            else
            {
                if(activateBattlers[i].IsPlayer)
                {
                    AllPlayersDead = false;
                }
                else
                {
                    AllEnemiesDead = false;
                }
            }
        }

        if(AllEnemiesDead || AllPlayersDead)
        {
            if (AllEnemiesDead)
            {
                //end Battle
                StartCoroutine(EndBattleCo());
            }
            else
            {
                //end battle in failute
                StartCoroutine(GameOverCo());
            }
            /*
            BattleScene.SetActive(false);
            GameManager.instance.battleActive = false;
            BattleActive = false;
            */
        }
        else
        {
            while(activateBattlers[currentTurn].CurrentHP == 0)
            {
                currentTurn++;
                if(currentTurn >= activateBattlers.Count)
                {
                    currentTurn = 0;
                }
            }
        }

    }
    #endregion

    public IEnumerator EnemyMoveCo()
    {
        turnWaiting = false;
        yield return new WaitForSeconds(1f);
        EnemyAttack();
        yield return new WaitForSeconds(1f);
        NextTurn();
    }

    public void EnemyAttack()
    {
        List<int> Players = new List<int>();
        for(int i = 0; i < activateBattlers.Count; i++)
        {
            if(activateBattlers[i].IsPlayer && activateBattlers[i].CurrentHP > 0)
            {
                Players.Add(i);
            }
        }
        int SelectedTarget = Players[Random.Range(0, Players.Count)];

        //activateBattlers[SelectedTarget].CurrentHP -= 10;

        int selectAttack = Random.Range(0, activateBattlers[currentTurn].MovesAvailable.Length);
        int movePower = 0;

        for(int i = 0; i < MovesList.Length;i++)
        {
            if(MovesList[i].MoveName == activateBattlers[currentTurn].MovesAvailable[selectAttack])
            {
                Instantiate(MovesList[i].theeffect, activateBattlers[SelectedTarget].transform.position, activateBattlers[SelectedTarget].transform.rotation);
                movePower = MovesList[i].MovePower;

            }
        }
        Instantiate(EnemyAttactEffect, activateBattlers[currentTurn].transform.position, activateBattlers[SelectedTarget].transform.rotation);
        DealDamage(SelectedTarget,movePower);

    }

    public void DealDamage(int target,int movePower)
    {
        float AttackPower = activateBattlers[currentTurn].Strength + activateBattlers[currentTurn].WeaponPower;
        float DefensePower = activateBattlers[target].Defense + activateBattlers[target].ArmorPower;

        float DamageCalc = (AttackPower / DefensePower) * movePower * Random.Range(.9f,1.1f);
        int damgeToGive = Mathf.RoundToInt(DamageCalc);

        Debug.Log(activateBattlers[currentTurn].CharName + " Is dealing" + DamageCalc + "(" + damgeToGive + " ) damage to " + activateBattlers[target].CharName);

        activateBattlers[target].CurrentHP -= damgeToGive;
        Instantiate(TheDamgeNumber, activateBattlers[target].transform.position, activateBattlers[target].transform.rotation).SetDamage(damgeToGive);
        UpdateUIStats();
    }

    public void UpdateUIStats()
    {
        for(int i = 0; i < PlayerNames.Length; i++)
        {
            if (activateBattlers.Count > 1)
            {
                if (activateBattlers[i].IsPlayer)
                {
                    BattleChar PlayerDate = activateBattlers[i];

                    PlayerNames[i].gameObject.SetActive(true);
                    PlayerNames[i].text = PlayerDate.CharName;
                    PlayerHP[i].text = Mathf.Clamp(PlayerDate.CurrentHP,0,int.MaxValue) + "/" + PlayerDate.MaxHP;
                    PlayerMP[i].text = Mathf.Clamp(PlayerDate.CurrentMP,0,int.MaxValue) + "/" + PlayerDate.MaxMP;

                }
                else
                {
                    PlayerNames[i].gameObject.SetActive(false);
                }
            }
            else
            {
                PlayerNames[i].gameObject.SetActive(true);
            }
        }
    }

    public void PlayerAttack(string MoveName, int SelectedTarget)
    {

        int movePower = 0;

        for (int i = 0; i < MovesList.Length; i++)
        {
            if (MovesList[i].MoveName == MoveName)
            {
                Instantiate(MovesList[i].theeffect, activateBattlers[SelectedTarget].transform.position, activateBattlers[SelectedTarget].transform.rotation);
                movePower = MovesList[i].MovePower;

            }
        }


        DealDamage(SelectedTarget, movePower);
        UIButtonHolder.SetActive(false);
        TargetMenu.SetActive(false);
        NextTurn();


    }

    public void OpenTargetMenu(string MoveName)
    {
        TargetMenu.SetActive(true);


        List<int> Enemies = new List<int>();
        for(int i = 0; i < activateBattlers.Count; i++)
        {
            if(!activateBattlers[i].IsPlayer)
            {
                Enemies.Add(i);

            }
        }

        for(int i = 0; i < TargetButtons.Length; i++)
        {
            if(Enemies.Count > i && activateBattlers[Enemies[i]].CurrentHP > 0)
            {
                TargetButtons[i].gameObject.SetActive(true);

                TargetButtons[i].MoveName = MoveName;
                TargetButtons[i].activeBattleTarget = Enemies[i];
                TargetButtons[i].TargetName.text = activateBattlers[Enemies[i]].CharName;
            }
            else
            {
                TargetButtons[i].gameObject.SetActive(false);
            }
        }

    }

    public void OpenMagicMenu()
    {
        MagicMenu.SetActive(true);

        for(int i = 0; i < MagicSelects.Length; i++)
        {
            if(activateBattlers[currentTurn].MovesAvailable.Length > i)
            {
                MagicSelects[i].gameObject.SetActive(true);

                MagicSelects[i].SpellName = activateBattlers[currentTurn].MovesAvailable[i];
                MagicSelects[i].NameText.text = MagicSelects[i].SpellName;

                for(int j = 0; j < MovesList.Length; j++)
                {
                    if(MovesList[j].MoveName == MagicSelects[i].SpellName)
                    {
                        MagicSelects[i].SpellCost = MovesList[j].MoveCost;
                        MagicSelects[i].CostText.text = MagicSelects[i].SpellCost.ToString(); 
                    }
                }
            }
            else
            {
                MagicSelects[i].gameObject.SetActive(false);
            }
        }

    }

    public void Flee()
    {
        int fleeSuccess = Random.Range(0, 100);

        if(fleeSuccess < ChanceToFlee)
        {
            //BattleActive = false;
            //BattleScene.SetActive(false);
            Fleeing = true;
            StartCoroutine(EndBattleCo());
        }
        else
        {
            NextTurn();
            BattleNotice.theTest.text = "Couldnt Escape!";
            BattleNotice.Active();
        }


    }

    public IEnumerator EndBattleCo()
    {
        BattleActive = false;
        UIButtonHolder.SetActive(false);
        TargetMenu.SetActive(false);
        MagicMenu.SetActive(false);

        yield return new WaitForSeconds(.5f);
        UIFade.instance.FadeToBlack();
        yield return new WaitForSeconds(1.5f);

        for(int i = 0; i <activateBattlers.Count; i++)
        {
            if(activateBattlers[i].IsPlayer)
            {
                for(int j = 0; j < GameManager.instance.playerstats.Length; j++)
                {
                    if(activateBattlers[i].CharName == GameManager.instance.playerstats[j].CharName)
                    {
                        GameManager.instance.playerstats[j].currentHP = activateBattlers[i].CurrentHP;
                        GameManager.instance.playerstats[j].currentMP = activateBattlers[i].CurrentMP;
                    }
                }
            }

            Destroy(activateBattlers[i].gameObject);
        }


        UIFade.instance.FadeFromBlack();
        BattleScene.SetActive(false);
        activateBattlers.Clear();
        currentTurn = 0;
        //GameManager.instance.battleActive = false;

        if(Fleeing)
        {
            GameManager.instance.battleActive = false;
            Fleeing = false;
        }
        else
        {
            BattleReward.instance.OpenRewardScreen(RewardXP, rewardItems);
        }

        AudioManager.instance.PlayVGM(FindObjectOfType<CameraController>().MusicToPlay);

    }


    public IEnumerator GameOverCo()
    {
        BattleActive = false;
        UIFade.instance.FadeToBlack();
        yield return new WaitForSeconds(1.5f);
        BattleScene.SetActive(false);
        SceneManager.LoadScene(GameOverScene);
    }
}

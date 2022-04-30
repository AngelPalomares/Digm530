using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    private bool BattleActive;

    public GameObject BattleScene;

    public Transform[] PlayerPositions, EnemyPosition;

    public BattleChar[] PlayerPrefab, EnemyPrefab;

    public List<BattleChar> activateBattlers = new List<BattleChar>();

    public int currentTurn;
    public bool turnWaiting;

    public GameObject UIButtonHolder;

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
            BattleStart(new string[] { "Rimuru", "Rimuru", "Rimuru", "Rimuru" });
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
            }
            else
            {
                //end battle in failute
            }

            BattleScene.SetActive(false);
            GameManager.instance.battleActive = false;
            BattleActive = false;
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

        activateBattlers[SelectedTarget].CurrentHP -= 10;

    }
}

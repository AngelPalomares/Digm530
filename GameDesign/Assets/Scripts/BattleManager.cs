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
            BattleStart(new string[] { "Rimuru" });
        }
    }

    public void BattleStart(string[] EnemiesToSpawn)
    {
        if(!BattleActive)
        {
            BattleActive = true;

            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
            GameManager.instance.battleActive = true;

            BattleScene.SetActive(true);
            
        }
    }

}

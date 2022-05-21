using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject UIScreen, Player, gamemanager,audioManager, battlemanager;

    // Start is called before the first frame update
    void Start()
    {
        if(UIFade.instance == null)
        {
            UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
        }

        if(PlayerController.instance == null)
        {
            PlayerController clone = Instantiate(Player).GetComponent<PlayerController>();

            PlayerController.instance = clone;
        }

        if(GameManager.instance == null)
        {
            Instantiate(gamemanager);
        }

        if(AudioManager.instance == null)
        {
            Instantiate(audioManager);
        }

        if(BattleManager.instance == null)
        {
            Instantiate(battlemanager);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

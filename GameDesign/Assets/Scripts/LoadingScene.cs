using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingScene : MonoBehaviour
{
    public float WaitToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(WaitToLoad > 0)
        {
            WaitToLoad -= Time.deltaTime;
            if(WaitToLoad <= 0)
            {
                SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));

                GameManager.instance.loaddata();
                QuestManager.instance.LoadQuestData();
            }
        }
    }
}

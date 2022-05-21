using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour
{
    public GameObject objectToActive;

    public string QuestToCheck;

    public bool ActiveIfComplete;

    private bool initialCheckDone;
    // Start is called before the first frame update
    void Start()
    {
        if(!initialCheckDone)
        {
            initialCheckDone = true;
            CheckCompletion();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckCompletion()
    {
        if (QuestManager.instance.CheckIfCompletest(QuestToCheck))
        {
            objectToActive.SetActive(ActiveIfComplete);
        }
    }
}

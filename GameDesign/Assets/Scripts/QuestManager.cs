using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public string[] questmarkernames;
    public bool[] QuestMarkersComplete;

    public static QuestManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        QuestMarkersComplete = new bool[questmarkernames.Length];
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log (CheckIfCompletest("Find the keycard"));
            MarkQuestComplete("Find the keycard");
            MarkQuestIncomplete("Find the me");
        }
    }

    public int GetQuestNumber(string QuestToFind)
    {
        for (int i = 0; i < questmarkernames.Length; i++)
        {
            if (questmarkernames[i] == QuestToFind)
            {
                return i;
            }
        }

        Debug.LogError("Quest " + QuestToFind + " does not exist");
        return 0;
    }

    public bool CheckIfCompletest(string questToCheck)
    {
        if (GetQuestNumber(questToCheck) != 0)
        {
            return QuestMarkersComplete[GetQuestNumber(questToCheck)];
        }

        return false;
    }

    public void MarkQuestComplete(string QuestToMark)
    {
        QuestMarkersComplete[GetQuestNumber(QuestToMark)] = true;
        UpdateLocalQuestObjects();
    }

    public void MarkQuestIncomplete(string questToMark)
    {
        QuestMarkersComplete[GetQuestNumber(questToMark)] = false;
        UpdateLocalQuestObjects();
    }

    public void UpdateLocalQuestObjects()
    {
        QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();

        if(questObjects.Length > 0)
        {
            for(int i = 0; i < questObjects.Length; i++)
            {
                questObjects[i].CheckCompletion();
            }
        }

    }
}
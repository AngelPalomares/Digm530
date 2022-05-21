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
    { /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SaveQuestData();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            LoadQuestData();
        }
        */
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

    public void SaveQuestData()
    {
        for(int i = 0; i < questmarkernames.Length; i++)
        {
            if (QuestMarkersComplete[i])
            {
                PlayerPrefs.SetInt("QuestMarker_" + questmarkernames[i], 1);
            }
            else
            {
                PlayerPrefs.SetInt("QuestMarker_" + questmarkernames[i], 0);
            }
        }
    }
    public void LoadQuestData()
    {
        for(int i = 0; i < questmarkernames.Length; i++)
        {
            int valuetoset = 0;

            if(PlayerPrefs.HasKey("QuestMarker_" + questmarkernames[i]))
            {
                valuetoset = PlayerPrefs.GetInt("QuestMarker_" + questmarkernames[i]);
            }

            if (valuetoset == 0)
            {
                QuestMarkersComplete[1] = false;
            }
            else
            {
                QuestMarkersComplete[i] = true;
            }
        }
    }
}
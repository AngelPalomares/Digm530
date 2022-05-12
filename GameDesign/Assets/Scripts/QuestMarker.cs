using UnityEngine;

public class QuestMarker : MonoBehaviour
{
    public string QuestToMark;
    public bool MarkComplete;

    public bool MarkOnEnter;
    private bool CanMark;

    public bool DeactiveOnMarking;
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (CanMark && Input.GetButtonDown("Fire1") || CanMark && Input.GetKeyDown(KeyCode.E))
        {
            CanMark = false;
            MarkQuest();
        }
    }

    public void MarkQuest()
    {
        if (MarkComplete)
        {
            QuestManager.instance.MarkQuestComplete(QuestToMark);
        }
        else
        {
            QuestManager.instance.MarkQuestIncomplete(QuestToMark);
        }

        gameObject.SetActive(!DeactiveOnMarking);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (MarkOnEnter)
            {
                MarkQuest();
            }
            else
            {
                CanMark = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CanMark = false;
        }
    }
}
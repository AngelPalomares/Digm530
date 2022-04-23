using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTheDialpgue2 : MonoBehaviour
{
    public static ActivateTheDialog instance;
    [SerializeField]
    private string[] Lines;

    private bool CanActivate;

    [SerializeField]
    private bool Human = true;

    public bool shouldActivateQuest, markcomplete;
    public string QuestToMark;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dialogueManager.instance.ShowDialog(Lines, Human);
            dialogueManager.instance.ShoulACtivateQuestAtEnd(QuestToMark, markcomplete);
            gameObject.SetActive(false);
        }
    }
}

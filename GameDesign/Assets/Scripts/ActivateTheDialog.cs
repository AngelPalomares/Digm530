using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTheDialog : MonoBehaviour
{
    public static ActivateTheDialog instance;
    [SerializeField]
    private string[] Lines;

    private bool CanActivate;

    [SerializeField]
    private bool Human = true;

    [SerializeField]
    public GameObject Item;

    public int count = 0;


    public bool shouldActivateQuest, markcomplete;
    public string QuestToMark;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Item.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(CanActivate && Input.GetButtonDown("Fire1") && !dialogueManager.instance.dialogbox.activeInHierarchy && !GameMenu.instance.TheMenu.activeInHierarchy || CanActivate && Input.GetKeyDown(KeyCode.E) && !dialogueManager.instance.dialogbox.activeInHierarchy && !GameMenu.instance.TheMenu.activeInHierarchy)
        {
            //passes the lines to the dialog manager
            dialogueManager.instance.ShowDialog(Lines, Human);
            dialogueManager.instance.ShoulACtivateQuestAtEnd(QuestToMark, markcomplete);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            CanActivate = true;
            Item.SetActive(true);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            CanActivate = false;
            Item.SetActive(false);
        }
    }
}

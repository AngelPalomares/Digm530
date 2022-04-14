using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTheDialog : MonoBehaviour
{
    [SerializeField]
    private string[] Lines;

    private bool CanActivate;

    [SerializeField]
    private bool Human = true;

    [SerializeField]
    private GameObject Item;

    // Start is called before the first frame update
    void Start()
    {
        Item.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(CanActivate && Input.GetButtonDown("Fire1") && !dialogueManager.instance.dialogbox.activeInHierarchy)
        {
            //passes the lines to the dialog manager
            dialogueManager.instance.ShowDialog(Lines, Human);
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

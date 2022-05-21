using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupKeyCard : MonoBehaviour
{
    private bool CanPickup;
    public string QuestToContinue;
    public bool checktrueifcomplete;

    // Update is called once per frame
    void Update()
    {
        if (CanPickup && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove)
        {
            dialogueManager.instance.ShoulACtivateQuestAtEnd(QuestToContinue, checktrueifcomplete);
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CanPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CanPickup = false;
        }
    }
}

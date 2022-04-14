using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{
    public static dialogueManager instance;

    [SerializeField]
    private Text DialoText;
    [SerializeField]
    public Text NameText;
    [SerializeField]
    public GameObject dialogbox, namebox;

    [SerializeField]
    public string[] Dialoglines;

    [SerializeField]
    private int currentline;

    private bool JustStarted;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        //DialoText.text = Dialoglines[currentline];
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogbox.activeInHierarchy)
        {
            if(Input.GetButtonUp("Fire1"))
            {
                if (!JustStarted)
                {
                    currentline++;

                    //if currenline >= the length of the script set it false to hide it and sets the ability for the player to move again
                    //if not the continue to go through the script
                    if (currentline >= Dialoglines.Length)
                    {
                        dialogbox.SetActive(false);
                        PlayerController.instance.canMove = true;
                    }
                    else
                    {
                        CheckIfName();
                        DialoText.text = Dialoglines[currentline];
                    }
                }
                else
                {
                    JustStarted = false;
                }
            }
        }
    }

    public void ShowDialog(string[] newLines,bool isPerson)
    {
        //makes the lines from the character into the lines in the UI
        Dialoglines = newLines;
        //Makes the currentline into zero to start the Dialog
        currentline = 0;

        CheckIfName();

        DialoText.text = Dialoglines[currentline];
        dialogbox.SetActive(true);
        JustStarted = true;

        namebox.SetActive(isPerson);

        PlayerController.instance.canMove = false;
    }

    public void CheckIfName()
    {
        if(Dialoglines[currentline].StartsWith("n-"))
        {
            NameText.text = Dialoglines[currentline].Replace("n-","");
            currentline++;
        }
    }
}

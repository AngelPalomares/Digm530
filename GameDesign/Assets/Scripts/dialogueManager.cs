using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {

        DialoText.text = Dialoglines[currentline];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

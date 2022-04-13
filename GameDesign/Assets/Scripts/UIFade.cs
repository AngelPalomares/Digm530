using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{

    public static UIFade instance;
    [SerializeField]
    private float FadeSpeed;
    [SerializeField]
    private Image FadeScreen;
    [SerializeField]
    private bool ShouldFadeToBlack;
    [SerializeField]
    private bool ShouldFadeFromBlack;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldFadeToBlack)
        {
            FadeScreen.color = new Color(FadeScreen.color.r, FadeScreen.color.g, FadeScreen.color.b, Mathf.MoveTowards(FadeScreen.color.a, 1f, FadeSpeed * Time.deltaTime));
            if(FadeScreen.color.a == 1f)
            {
                ShouldFadeToBlack = false;
            }
        }

        if (ShouldFadeFromBlack)
        {
            FadeScreen.color = new Color(FadeScreen.color.r, FadeScreen.color.g, FadeScreen.color.b, Mathf.MoveTowards(FadeScreen.color.a, 0f, FadeSpeed * Time.deltaTime));
            if(FadeScreen.color.a == 0f)
            {
                ShouldFadeFromBlack = false;
            }
        }
    }
    
    //Fades UI black
    public void FadeToBlack()
    {
        ShouldFadeToBlack = true;

        ShouldFadeFromBlack = false;
    }

    //clears fade
    public void FadeFromBlack()
    {
        ShouldFadeToBlack = false;
        ShouldFadeFromBlack = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public AudioSource[] SFX;
    public AudioSource[] FirstLevel;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySXF(int soundtoplay)
    {
        if (soundtoplay < SFX.Length)
        {
            SFX[soundtoplay].Play();
        }
    }

    public void PlayVGM(int currentScene)
    {
            StopVGM();
            if (currentScene < FirstLevel.Length)
            {
                FirstLevel[currentScene].Play();
            }
    }

    public void StopVGM()
    {
        for(int i = 0; i < FirstLevel.Length; i++)
        {
            FirstLevel[i].Stop();
        }
    }
}

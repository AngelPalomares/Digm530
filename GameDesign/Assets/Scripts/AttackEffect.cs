using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public float EffectLength;
    public int SoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySXF(SoundEffect);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject,EffectLength);
    }
}

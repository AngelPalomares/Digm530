using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleChar : MonoBehaviour
{
    public bool IsPlayer;

    public string[] MovesAvailable;

    public string CharName;
    public int CurrentHP, MaxHP, CurrentMP, MaxMP, Strength, Defense, WeaponPower, ArmorPower;

    public bool HasDied;

    public SpriteRenderer TheSprite;

    private bool SHouldFade;
    public float FadeSpeed = 1f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SHouldFade)
        {
            TheSprite.color = new Color(Mathf.MoveTowards(TheSprite.color.r,1f, FadeSpeed *Time.deltaTime), Mathf.MoveTowards(TheSprite.color.g, 0f, FadeSpeed * Time.deltaTime), Mathf.MoveTowards(TheSprite.color.b, 0f, FadeSpeed * Time.deltaTime), Mathf.MoveTowards(TheSprite.color.a, 0f, FadeSpeed * Time.deltaTime));
            if(TheSprite.color.a == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void EnemyFade()
    {
        SHouldFade = true;
    }
}

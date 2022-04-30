using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    public Text DamageText;
    public float Lifetime = 1f;
    public float MoveSpeed = 1f;

    public float PlacementJitter = .5f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject,Lifetime);
        transform.position += new Vector3(0f, MoveSpeed * Time.deltaTime, 0f);
    }

    public void SetDamage(int DamageAmount)
    {
        DamageText.text = DamageAmount.ToString();
        transform.position += new Vector3(Random.Range(-PlacementJitter, PlacementJitter), Random.Range(-PlacementJitter, PlacementJitter),0f);

    }

}

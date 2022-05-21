using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public float MoveSpeed;
    public string LoadScene;

    public Transform LeftPoint, Rightpoint;

    public GameObject LeftLight;
    public GameObject RightLight;

    private bool MovingRight;

    private Rigidbody2D therb;

    public SpriteRenderer theSR;

    private Animator anim;

    public float MoveTime, WaitTIme;
    private float Movecount,WaitCount;

    // Start is called before the first frame update
    void Start()
    {
        therb= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        LeftPoint.parent = null;
        Rightpoint.parent = null;
        MovingRight = true;

        Movecount = MoveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Movecount > 0)
        {

            Movecount -= Time.deltaTime;

            if (MovingRight)
            {
                therb.velocity = new Vector2(MoveSpeed, therb.velocity.y);
                theSR.flipX = true;
                LeftLight.SetActive(false);
                RightLight.SetActive(true);

                if (transform.position.x > Rightpoint.position.x)
                {
                    MovingRight = false;
                }
            }
            else
            {
                LeftLight.SetActive(true);
                RightLight.SetActive(false);
                theSR.flipX = false;
                therb.velocity = new Vector2(-MoveSpeed, therb.velocity.y);


                if (transform.position.x < LeftPoint.position.x)
                {
                    MovingRight = true;
                }
            }

            if (Movecount <= 0)
            {
                WaitCount = Random.Range(WaitTIme * .75f, WaitTIme * 1.25f); 
            }
            anim.SetBool("IsMoving", true);
        }
        else if (WaitTIme > 0) 
        {
            WaitCount -= Time.deltaTime;
            therb.velocity = new Vector2(0f, therb.velocity.y);

            if (WaitCount <= 0)
            {
                Movecount = Random.Range(MoveTime * .75f, WaitTIme * .75f);
            }
            anim.SetBool("IsMoving", false);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene(LoadScene);
        }
    }
}

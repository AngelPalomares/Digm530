using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChasingtheplayerScript : MonoBehaviour
{
    public Transform[] points;

    public float moveSpeed;
    public int currentpoint;

    public GameObject Leftlight, Rightlight;

    public string LoadScene;

    public float DistanceToAttackPlayer, ChaseSpeed;


    public SpriteRenderer theSR;

    private Vector3 AttackTarget;



    public float waitafterattack;

    private float attackCounter;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < points.Length; i++)
        {
            points[i].parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        }
        else
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > DistanceToAttackPlayer)
            {
                AttackTarget = Vector3.zero;
                transform.position = Vector3.MoveTowards(transform.position, points[currentpoint].position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, points[currentpoint].position) < .05f)
                {
                    currentpoint++;

                    if (currentpoint >= points.Length)
                    {
                        currentpoint = 0;

                    }
                }

                if (transform.position.x < points[currentpoint].position.x)
                {
                    theSR.flipX = true;
                    Leftlight.SetActive(true);
                    Rightlight.SetActive(false);
                }
                else if (transform.position.x > points[currentpoint].position.x)
                {
                    theSR.flipX = false;

                    Leftlight.SetActive(false);
                    Rightlight.SetActive(true);
                }
            }
            else
            {
                //attacking the player

                if (transform.position.x < PlayerController.instance.transform.position.x)
                {
                    theSR.flipX = true;
                    Leftlight.SetActive(true);
                    Rightlight.SetActive(false);
                }
                else if (transform.position.x > PlayerController.instance.transform.position.x)
                {
                    theSR.flipX = false;
                    Leftlight.SetActive(false);
                    Rightlight.SetActive(true);
                }

                if (AttackTarget == Vector3.zero)
                {
                    AttackTarget = PlayerController.instance.transform.position;
                }
                transform.position = Vector3.MoveTowards(transform.position, AttackTarget, ChaseSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, AttackTarget) <= .1f)
                {

                    attackCounter = waitafterattack;
                    AttackTarget = Vector3.zero;

                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("DetectedPlayer");
            SceneManager.LoadScene(LoadScene);
        }
    }
}

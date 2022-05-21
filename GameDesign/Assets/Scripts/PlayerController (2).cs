using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	//Rigidbody of the player
	[SerializeField]
	private Rigidbody2D TheRB;
	//The speed of the play
	[SerializeField]
	private float movespeed;
	//Animator for the character
	[SerializeField]
	private Animator Myanim;
	//
	public static PlayerController instance;

	[SerializeField]
	public string AreaTransitionName;

	[SerializeField]
	public bool canMove = true;

	//This limits the player where they are going
	private Vector3 bottomLeftLimity, ToprightLimity;

	// Use this for initialization
	void Start () {

		if(instance == null)
        {
			instance = this;
        }
        else
        {
			if (instance != this)
			{
				Destroy(gameObject);
			}
        }

		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {

		if(canMove)
        {
			//function that makes the player move
			TheRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * movespeed;
		}
        else
        {
			TheRB.velocity = Vector2.zero;
        }

		//Animator for the character
		Myanim.SetFloat("moveX", TheRB.velocity.x);
		Myanim.SetFloat("moveY", TheRB.velocity.y);

		if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 ||
			Input.GetAxisRaw("Vertical") == -1)
        {
			if (canMove)
			{
				Myanim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
				Myanim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
			}
				
		}

		transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimity.x, ToprightLimity.x), Mathf.Clamp(transform.position.y, bottomLeftLimity.y, ToprightLimity.y), transform.position.z);

	}

	public void Setbounds(Vector3 BottomLeft, Vector3 TopRight)
    {
		bottomLeftLimity = BottomLeft + new Vector3 (.5f,1f,0f);
		ToprightLimity = TopRight + new Vector3(-.5f,-1f,0f);
    }
}

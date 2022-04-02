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
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//function that makes the player move
		TheRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * movespeed;

		//Animator for the character
		Myanim.SetFloat("moveX", TheRB.velocity.x);
		Myanim.SetFloat("moveY", TheRB.velocity.y);

		if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 ||
			Input.GetAxisRaw("Vertical") == -1)
        {
			Myanim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
			Myanim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
				
		}

	}
}

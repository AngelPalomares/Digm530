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
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//function that makes the player move
		TheRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * movespeed;
	}
}

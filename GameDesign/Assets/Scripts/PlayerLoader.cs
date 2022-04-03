using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour {

	[SerializeField]
	private GameObject Player;
	// Use this for initialization
	void Start () {

		//This function will create a player if no player is actively in the scene
		if (PlayerController.instance == null)
			Instantiate(Player);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

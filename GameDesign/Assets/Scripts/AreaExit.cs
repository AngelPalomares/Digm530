using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour {

	//string of the scene
	[SerializeField]
	private string AreatoLoad,AreaTransitionName;

	[SerializeField]
	private AreaEntrance theEntrance;

	// Use this for initialization
	void Start () {
		theEntrance.transitionName = AreaTransitionName;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//this function is what loads the scene
		if(collision.tag == "Player")
        {
			SceneManager.LoadScene(AreatoLoad);
			PlayerController.instance.AreaTransitionName = AreaTransitionName;
        }
	}

	
}
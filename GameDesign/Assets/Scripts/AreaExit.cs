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

	[SerializeField]
	public float waitToLoad = 1f;

	[SerializeField]
	private bool shouldloadafterfade;

	// Use this for initialization
	void Start () {
		theEntrance.transitionName = AreaTransitionName;

	}

	// Update is called once per frame
	void Update() {
		if (shouldloadafterfade)
        {
			waitToLoad -= Time.deltaTime;
			if(waitToLoad <= 0f)
            {
				shouldloadafterfade = false;
				SceneManager.LoadScene(AreatoLoad);
            }
        }
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//this function is what loads the scene
		if(collision.tag == "Player")
        {
			//SceneManager.LoadScene(AreatoLoad);
			shouldloadafterfade = true;
			GameManager.instance.FadigBetweenAreas = true;
			UIFade.instance.FadeToBlack();
			PlayerController.instance.AreaTransitionName = AreaTransitionName;
        }
	}

	
}
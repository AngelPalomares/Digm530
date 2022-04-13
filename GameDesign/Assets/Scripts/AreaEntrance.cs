using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour {

	[SerializeField]
	public string transitionName;

	// Use this for initialization
    public void Start()
    {
		//if statement is used to put the player in the area entrance
		if(transitionName == PlayerController.instance.AreaTransitionName)
			PlayerController.instance.transform.position = transform.position;
		UIFade.instance.FadeFromBlack();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
	
}

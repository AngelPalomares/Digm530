using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour {

	[SerializeField]
	private Transform target;

	[SerializeField]
	private Tilemap Themap;

	private Vector3 BottomLeftLimit, TopRightLimit;

	private float Halfheight, HalfWidth;

	// Use this for initialization
	void Start () {
		target = PlayerController.instance.transform;

		Halfheight = Camera.main.orthographicSize;
		HalfWidth = Halfheight * Camera.main.aspect;

		//dont forget to put the tilemap into the camera
		BottomLeftLimit = Themap.localBounds.min + new Vector3(HalfWidth,Halfheight,0f);
		TopRightLimit = Themap.localBounds.max + new Vector3(-HalfWidth,-Halfheight,0f);


	}
	
	// Update is called once per frame
	void LateUpdate () {
		//gets the location of the player
		transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

		//keep the camera inside the bounds
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, BottomLeftLimit.x, TopRightLimit.x), Mathf.Clamp(transform.position.y,BottomLeftLimit.y,TopRightLimit.y), transform.position.z);

	}
}

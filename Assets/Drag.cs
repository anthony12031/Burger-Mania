using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {

	public bool isClicked = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isClicked) {
			Vector2 mousePos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			Vector2 objPos = Camera.main.ScreenToWorldPoint (mousePos);
			transform.position = objPos;
		}

	}

	void OnMouseDown(){
		isClicked = true;
	}

	void OnMouseUp(){
		isClicked = false;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log ("colision");
	}


}

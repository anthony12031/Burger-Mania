using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSalsa : MonoBehaviour {

	public bool isClicked = false;
	private Vector3 rotacion;
	private Quaternion rotacionOriginal;

	// Use this for initialization
	void Start () {
		rotacion = new Vector3 (0, 0, -135);
		rotacionOriginal = transform.rotation; 
	}
	
	// Update is called once per frame
	void Update () {
		if (isClicked) {
			Vector2 mousePos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			Vector2 objPos = Camera.main.ScreenToWorldPoint (mousePos);
			transform.position = objPos;
		}
	}

	void onTriggerEnter2D(Collider2D coll){
		Debug.Log ("colision");
	}

	void OnMouseDown(){
		isClicked = true;
		transform.Rotate (rotacion);
	}

	void OnMouseUp(){
		transform.rotation = rotacionOriginal;
		isClicked = false;
	}
}

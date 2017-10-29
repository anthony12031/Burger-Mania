using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DragPerroCaliente : MonoBehaviour {

	public bool isClicked = false;
	public PanControlador.PosicionParrilla posicionEnParrilla;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isClicked) {
			Vector2 mousePos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			Vector2 objPos = Camera.main.ScreenToWorldPoint (mousePos);
			transform.position = objPos;
			GetComponent<SortingGroup> ().sortingOrder = 2;	
		}

	}

	void OnMouseDown(){
		isClicked = true;
	}

	void OnMouseUp(){
		isClicked = false;
		GetComponent<SortingGroup> ().sortingOrder = 1;	
	}
}

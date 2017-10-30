﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSalchicha : MonoBehaviour {

	public bool isClicked = false;
	Collider2D colliderSalchicha;
	public  Transform perroCaliente;
	private Vector2 posOriginal;
	public string topSortinLayer;
	public SalchichaControlador.PosicionParrilla posicionEnParrilla;


	// Use this for initialization
	void Start () {
		colliderSalchicha = gameObject.GetComponent<Collider2D>();
		posOriginal = gameObject.transform.position;

	}

	// Update is called once per frame
	void Update () {
		if (isClicked) {
			Vector2 mousePos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			Vector2 objPos = Camera.main.ScreenToWorldPoint (mousePos);
			transform.position = objPos;
			gameObject.GetComponent<Cocinado> ().estaEnParrilla = false;
			GetComponent<SpriteRenderer> ().sortingOrder = 100;
		}

	}

	void OnMouseDown(){
		isClicked = true;
	}


	GameObject panPerroColision;
	void OnTriggerStay2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("panPerroEnProcesador")) {
			if(!PanControlador.tieneSalchicha(coll.gameObject))
				panPerroColision = coll.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("panPerroEnProcesador")) {
			panPerroColision = null;
		}
	}

	bool desecharSalchicha = false;

	void OnTriggerEnter2D(Collider2D coll){



	}


	void OnMouseUp(){
		isClicked = false;

		if (panPerroColision != null) {
			panPerroColision.transform.GetChild (0).localPosition = new Vector2 (-0.06f, 0.08f);
			//gameObject.transform.parent = panPerroColision.transform;
		//	gameObject.transform.localPosition = new Vector2 (0.04f, 0.02f);
			GetComponent<SpriteRenderer> ().sortingLayerName = "sobreMeson";
			GetComponent<SpriteRenderer> ().sortingOrder = 1;
			panPerroColision.transform.parent = gameObject.transform;
			panPerroColision.transform.localPosition = new Vector2 (0.0f, 0.0f);

			Destroy (GetComponent<DragSalchicha> ());
			Destroy (GetComponent<Collider2D> ());
			//posicionEnParrilla.libre = true;
		} else {
			gameObject.transform.position = posOriginal;
			GetComponent<SpriteRenderer> ().sortingOrder = 0;
		}




	}


}
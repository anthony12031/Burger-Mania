using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSalchicha : MonoBehaviour {

	public bool isClicked = false;
	Collider2D colliderSalchicha;
	public  Transform perroCaliente;
	private Vector2 posOriginal;


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
		}

	}

	void OnMouseDown(){
		isClicked = true;
	}

	void OnMouseUp(){
		
			bool colision = false;
			if (PanControlador.posParrilla1.estado == "ocupado") {
				Collider2D pan1 = PanControlador.posParrilla1.pan.gameObject.GetComponent<Collider2D> ();
				if (colliderSalchicha.IsTouching (pan1)) {
					Destroy (PanControlador.posParrilla1.pan.gameObject);
					Transform perro = Instantiate (perroCaliente, PanControlador.posParrilla1.v3Pos, Quaternion.identity);
					perro.GetChild (1).gameObject.GetComponent<SpriteRenderer>().sprite =
						gameObject.GetComponent<SpriteRenderer>().sprite;
				    PanControlador.posParrilla1.estado = "perroCaliente";
					SalchichaControlador.posParrilla1.estado = "vacio";
					Destroy (gameObject);
					colision = true;
				}
			}
			if (PanControlador.posParrilla2.estado == "ocupado") {
				Collider2D pan2 = PanControlador.posParrilla2.pan.gameObject.GetComponent<Collider2D> ();
				if (colliderSalchicha.IsTouching (pan2)) {
					Destroy (PanControlador.posParrilla2.pan.gameObject);
					Transform perro = Instantiate (perroCaliente, PanControlador.posParrilla2.v3Pos, Quaternion.identity);
					perro.GetChild (1).gameObject.GetComponent<SpriteRenderer>().sprite =
						gameObject.GetComponent<SpriteRenderer>().sprite;
					PanControlador.posParrilla2.estado = "perroCaliente";
					SalchichaControlador.posParrilla2.estado = "vacio";
					Destroy (gameObject);
					colision = true;
				}
			}
			if (PanControlador.posParrilla3.estado == "ocupado") {
				Collider2D pan3 = PanControlador.posParrilla3.pan.gameObject.GetComponent<Collider2D> ();
				if (colliderSalchicha.IsTouching (pan3)) {
					Destroy (PanControlador.posParrilla3.pan.gameObject);
					Transform perro = Instantiate (perroCaliente, PanControlador.posParrilla3.v3Pos, Quaternion.identity);
					perro.GetChild (1).gameObject.GetComponent<SpriteRenderer>().sprite =
						gameObject.GetComponent<SpriteRenderer>().sprite;
				    PanControlador.posParrilla3.estado = "perroCaliente";
					SalchichaControlador.posParrilla3.estado = "vacio";
					Destroy (gameObject);
					colision = true;
				}
			}
				

			if (!colision) {
				gameObject.transform.position = posOriginal;
				gameObject.GetComponent<Cocinado> ().estaEnParrilla = true;
			}
		
		isClicked = false;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log ("colision");
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSalsa : MonoBehaviour {

	public bool isClicked = false;
	private Vector3 rotacion;
	private Quaternion rotacionOriginal;
	public GameObject salsaTomate;
	private Vector3 posSalsa;
	private Vector3 posOriginal;

	// Use this for initialization
	void Start () {
		rotacion = new Vector3 (0, 0, -135);
		rotacionOriginal = transform.rotation;
		posOriginal = transform.position;
		posSalsa = new Vector3 (0.05f, -0.04f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (isClicked) {
			Vector2 mousePos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			Vector2 objPos = Camera.main.ScreenToWorldPoint (mousePos);
			transform.position = objPos;
		}
	}


	GameObject perro;
	GameObject[] perros = new GameObject[3];

	bool tieneSalsa(GameObject perro){
		bool tieneSalsa = false;
		foreach (Transform child in perro.transform) {
			if (child.CompareTag ("salsaTomate")) {
				tieneSalsa = true;
				break;
			}
		}
		return tieneSalsa;
	}


	void OnTriggerStay2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("perroCaliente")) {
			if (!tieneSalsa (coll.gameObject)) {
				perro = coll.gameObject;
			}

		}

	}

	void OnTriggerExit2D(Collider2D coll){

		if ( coll.gameObject.CompareTag ("perroCaliente")) {
			perro = null;
		}

	}

	void OnMouseDown(){
		isClicked = true;
	    transform.Rotate (rotacion);
	}

	void OnMouseUp(){
		transform.rotation = rotacionOriginal;
		Debug.Log (perro);
		if (perro) {
			GameObject salsa = Instantiate (salsaTomate, posSalsa, Quaternion.identity);
			salsa.transform.parent = perro.transform;
			salsa.transform.localPosition = posSalsa;
		} 
		else {
			transform.position = posOriginal;
		}
		isClicked = false;
	}
}

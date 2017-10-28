using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSalsa : MonoBehaviour {

	public bool isClicked = false;
	private Vector3 rotacion;
	private Quaternion rotacionOriginal;
	public GameObject salsa;
	private Vector3 posSalsa;
	private Vector3 posOriginal;
	public AudioClip sonidoSalsa;

	// Use this for initialization
	void Start () {
		rotacion = new Vector3 (0, 0, -135);
		rotacionOriginal = transform.rotation;
		posOriginal = transform.position;
		if(salsa.CompareTag("salsaTomate"))
			posSalsa = new Vector3 (0.05f, 0.03f, 0);
		else
			posSalsa = new Vector3 (0.09f, 0, 0);

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

	bool tieneSalsa(GameObject perro,string tipoSalsa){
		bool tieneSalsa = false;
		foreach (Transform child in perro.transform) {
			if (child.CompareTag (tipoSalsa)) {
				tieneSalsa = true;
				break;
			}
		}
		return tieneSalsa;
	}


	void OnTriggerStay2D(Collider2D coll){
		
		if (coll.gameObject.CompareTag ("panPerro")) {
			if (gameObject.name == "frascoTomate") {
				if (!tieneSalsa (coll.gameObject,"salsaTomate")) {
					perro = coll.gameObject;
				}
			}
			if (gameObject.name == "frascoMostaza") {
				if (!tieneSalsa (coll.gameObject,"salsaMostaza")) {
					perro = coll.gameObject;
				}
			}

		}

	}



	void OnTriggerExit2D(Collider2D coll){

		if ( coll.gameObject.CompareTag ("panPerro")) {
			perro = null;
		}


	}

	void OnMouseDown(){
		isClicked = true;
		if(transform.rotation == rotacionOriginal)
	   		 transform.Rotate (rotacion);
	}

	void OnMouseUp(){
		transform.rotation = rotacionOriginal;

		if (perro) {
			if (PanControlador.tieneSalchicha (perro)) {
				GetComponent<AudioSource> ().PlayOneShot (sonidoSalsa);
				GameObject salsaNueva = Instantiate (salsa, posSalsa, Quaternion.identity);
				salsaNueva.transform.parent = perro.transform;
				salsaNueva.transform.localPosition = posSalsa;
			} 
		} 

		transform.position = posOriginal;
		isClicked = false;
	}
}

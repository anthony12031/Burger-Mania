using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanControlador : MonoBehaviour {

	public Transform panPerro;
	public static PosicionParrilla posParrilla1;
	public static PosicionParrilla posParrilla2;
	public static PosicionParrilla posParrilla3;

	public  class PosicionParrilla{

		public string estado;
		public Vector3 v3Pos;
		public Transform pan;

		public PosicionParrilla(Vector3 pos){
			estado = "vacio";
			v3Pos = pos;
		}

	}

	PosicionParrilla getSigPosLibre(){
		if (posParrilla1.estado == "vacio") {
			return posParrilla1;
		}
		else if(posParrilla2.estado == "vacio") {
			return posParrilla2;
		}
		else if(posParrilla3.estado == "vacio") {
			return posParrilla3;
		}
		return null;
	}

	public static bool tieneSalchicha(GameObject perro){
		bool tiene = false;
		foreach (Transform child in perro.transform) {
			if (child.CompareTag ("salchicha")) {
				tiene = true;
				break;
			}
		}
		return tiene;
	}

	// Use this for initialization
	void Start () {
		posParrilla1 = new PosicionParrilla(new Vector3 (-0.9f, -0.37f, 0));
		posParrilla2 = new PosicionParrilla(new Vector3 (-0.37f, -0.37f, 0));
		posParrilla3 = new PosicionParrilla(new Vector3 (0.18f, -0.37f, 0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		PosicionParrilla posLibre = getSigPosLibre();
		if (posLibre != null) {
			posLibre.pan = Instantiate (panPerro, posLibre.v3Pos, Quaternion.identity);
			posLibre.estado = "ocupado";
		}
	}

}

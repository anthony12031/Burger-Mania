using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanControlador : MonoBehaviour {

	public Transform panPerro;
	public PosicionParrilla posParrilla1;
	public PosicionParrilla posParrilla2;
	public PosicionParrilla posParrilla3;

	public class PosicionParrilla{

		public string estado;
		public Vector3 v3Pos;

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

	// Use this for initialization
	void Start () {
		posParrilla1 = new PosicionParrilla(new Vector3 (-0.95f, -0.43f, 0));
		posParrilla2 = new PosicionParrilla(new Vector3 (-0.61f, -0.43f, 0));
		posParrilla3 = new PosicionParrilla(new Vector3 (-0.23f, -0.43f, 0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		PosicionParrilla posLibre = getSigPosLibre();
		if (posLibre != null) {
			Instantiate (panPerro, posLibre.v3Pos, Quaternion.identity);
			posLibre.estado = "ocupado";
		}
	}

}

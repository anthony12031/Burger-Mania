using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalchichaControlador : MonoBehaviour {

	public Transform salchicha;
	public static PosicionParrilla posParrilla1;
	public static PosicionParrilla posParrilla2;
	public static PosicionParrilla posParrilla3;
	public static PosicionParrilla posParrilla4;

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
		else if(posParrilla4.estado == "vacio") {
			return posParrilla4;
		}
		return null;
	}

	// Use this for initialization
	void Start () {
		posParrilla1 = new PosicionParrilla(new Vector3 (1.56f, -0.35f, 0));
		posParrilla2 = new PosicionParrilla(new Vector3 (1.91f, -0.35f, 0));
		posParrilla3 = new PosicionParrilla(new Vector3 (2.26f, -0.35f, 0));
		posParrilla4 = new PosicionParrilla(new Vector3 (2.64f, -0.35f, 0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		PosicionParrilla posLibre = getSigPosLibre();
		if (posLibre != null) {
			Instantiate (salchicha, posLibre.v3Pos, Quaternion.identity);
			posLibre.estado = "ocupado";
		}
	}


}

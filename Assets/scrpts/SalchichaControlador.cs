using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalchichaControlador : MonoBehaviour {

	public Transform salchicha;
	public static PosicionParrilla posParrilla1;
	public static PosicionParrilla posParrilla2;
	public static PosicionParrilla posParrilla3;

	public class PosicionParrilla{
	
		public bool libre;
		public Vector3 v3Pos;

		public PosicionParrilla(Vector3 pos){
			libre = true;
			v3Pos = pos;
		}

	}

	PosicionParrilla getSigPosLibre(){
		if (posParrilla1.libre) {
			return posParrilla1;
			}
		else if(posParrilla2.libre) {
			return posParrilla2;
		}
		else if(posParrilla3.libre) {
			return posParrilla3;
		}

		return null;
	}

	// Use this for initialization
	void Start () {

		posParrilla1 = new PosicionParrilla(new Vector3 (1.56f, -0.35f, 0));
		posParrilla2 = new PosicionParrilla(new Vector3 (1.91f, -0.35f, 0));
		posParrilla3 = new PosicionParrilla(new Vector3 (2.26f, -0.35f, 0));
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		PosicionParrilla posLibre = getSigPosLibre();
		if (posLibre != null) {
			GameObject nuevaSalchicha = Instantiate (salchicha, posLibre.v3Pos, Quaternion.identity).gameObject;
			nuevaSalchicha.GetComponent<DragSalchicha> ().posicionEnParrilla = posLibre;
			posLibre.libre = false;
		}
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalchichaControlador : MonoBehaviour {

	public Transform salchicha;

	public Transform salchicha1;
	public Transform salchicha2;
	public Transform salchicha3;

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

	PosicionParrilla getSigPosLibre(int cpu){
		/*if (posParrilla1.libre) {
			return posParrilla1;
		}
		else if(posParrilla2.libre) {
			return posParrilla2;
		}
		else if(posParrilla3.libre) {
			return posParrilla3;
		}*/

		if (cpu == 1) {
			return posParrilla1;
		}

		return null;
	}

	// Use this for initialization
	void Start () {

		posParrilla1 = new PosicionParrilla(salchicha1.position);
		posParrilla2 = new PosicionParrilla(salchicha2.position);
		posParrilla3 = new PosicionParrilla(salchicha3.position);

	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown(){
		//PosicionParrilla posLibre = getSigPosLibre();
		//if (posLibre != null) {
	
		//GameObject nuevaSalchicha = Instantiate (salchicha, posParrilla1.v3Pos, Quaternion.identity).gameObject;
			//nuevaSalchicha.GetComponent<DragSalchicha> ().posicionEnParrilla = posLibre;
			//posLibre.libre = false;
		//}
	}


}

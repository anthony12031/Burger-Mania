using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planificador : MonoBehaviour {

	seleccionTipoPerro seleccionPerro;


	public void crearOrden(){
		Debug.Log ("crear orden");
		Debug.Log ("tipo perro: " + seleccionPerro.getTipoPerro ());
	}

	// Use this for initialization
	void Start () {
		seleccionPerro = GetComponent<seleccionTipoPerro> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

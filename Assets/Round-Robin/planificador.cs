using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planificador : MonoBehaviour {

	seleccionTipoPerro seleccionPerro;
	public PersonajeController controladorPersonajes;

	//orden analogo a proceso
	public GameObject Orden;
	//colas de los procesos
	Queue listos;
	Queue bloqueados;
	Queue suspendidos;


	public void crearOrden(){
		Debug.Log ("crear orden");
		Debug.Log ("tipo perro: " + seleccionPerro.getTipoPerro ());
		controladorPersonajes.agregarPersonaje();

	}

	// Use this for initialization
	void Start () {
		seleccionPerro = GetComponent<seleccionTipoPerro> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

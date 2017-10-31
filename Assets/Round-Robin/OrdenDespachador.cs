using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdenDespachador : MonoBehaviour {

	seleccionTipoPerro seleccionPerro;
	SeleccionCPU seleccionCPU;

	public GameObject planificadorCPU1;
	public GameObject planificadorCPU2;
	public GameObject planificadorCPU3;


	// Use this for initialization
	void Start () {
		seleccionPerro = GetComponent<seleccionTipoPerro> ();
		seleccionCPU = GetComponent<SeleccionCPU> ();
	}

	public void crearOrden(){
		Debug.Log ("crear orden");
		int tipoPerro = seleccionPerro.getTipoPerro ()+1;
		int cpu = seleccionCPU.getCPU () + 1;

		if (cpu == 1) {
			planificadorCPU1.GetComponent<planificador> ().crearOrden (tipoPerro);
		}
		if (cpu == 2) {
			planificadorCPU2.GetComponent<planificador> ().crearOrden (tipoPerro);
		}
		if (cpu == 3) {
			planificadorCPU3.GetComponent<planificador> ().crearOrden (tipoPerro);
		}

	}

	// Update is called once per frame
	void Update () {
		
	}
}

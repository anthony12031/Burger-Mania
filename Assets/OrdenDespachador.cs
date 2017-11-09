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
			planificadorCPU1.GetComponent<PlanificadorSRTF>().crearProceso (tipoPerro);
		}
		if (cpu == 2) {
			planificadorCPU2.GetComponent<PlanificadorSRTF>().crearProceso (tipoPerro);
		}
		if (cpu == 3) {
			planificadorCPU3.GetComponent<PlanificadorSRTF>().crearProceso (tipoPerro);
		}

	}

	// Update is called once per frame
	void Update () {
		getInput ();
	}

	void getInput(){
		//listos
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			planificadorCPU1.GetComponent<PlanificadorSRTF> ().crearProceso (1);
		}	
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			planificadorCPU2.GetComponent<PlanificadorSRTF> ().crearProceso (1);
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			planificadorCPU3.GetComponent<PlanificadorSRTF> ().crearProceso (1);
		}
		// listo a suspendidos
		if (Input.GetKeyDown (KeyCode.Q)) {
			planificadorCPU1.GetComponent<PlanificadorSRTF> ().listoToSuspendido ();
		}
		if (Input.GetKeyDown (KeyCode.W)) {
			planificadorCPU2.GetComponent<PlanificadorSRTF> ().listoToSuspendido ();
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			planificadorCPU3.GetComponent<PlanificadorSRTF> ().listoToSuspendido ();
		}

		// listo a bloqueado
		if (Input.GetKeyDown (KeyCode.A)) {
			planificadorCPU1.GetComponent<PlanificadorSRTF> ().listoToBloqueado ();
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			planificadorCPU2.GetComponent<PlanificadorSRTF> ().listoToBloqueado ();
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			planificadorCPU3.GetComponent<PlanificadorSRTF> ().listoToBloqueado ();
		}
	}
}

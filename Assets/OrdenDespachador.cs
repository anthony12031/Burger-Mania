using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrdenDespachador : MonoBehaviour {

	seleccionTipoPerro seleccionPerro;
	SeleccionCPU seleccionCPU;

	public GameObject planificadorCPU1;
	public GameObject planificadorCPU2;
	public GameObject planificadorCPU3;

	public float tiempoProceso = 0;

	// Use this for initialization
	void Start () {
		seleccionPerro = GetComponent<seleccionTipoPerro> ();
		seleccionCPU = GetComponent<SeleccionCPU> ();
	}

	public void setTiempoProceso(string t){
		try{
			tiempoProceso = System.Convert.ToSingle (t);
		}catch(System.FormatException){
		
		}
	}

	public void crearOrden(){
		//Debug.Log ("crear orden");
		int tipoPerro = seleccionPerro.getTipoPerro ()+1;
		int cpu = seleccionCPU.getCPU () + 1;

		if (cpu == 1) {
			planificadorCPU1.GetComponent<PlanificadorSRTF>().crearProceso (tipoPerro,tiempoProceso);
		}
		if (cpu == 2) {
			planificadorCPU2.GetComponent<PlanificadorSRTF>().crearProceso (tipoPerro,tiempoProceso);
		}
		if (cpu == 3) {
			planificadorCPU3.GetComponent<PlanificadorSRTF>().crearProceso (tipoPerro,tiempoProceso);
		}

	}

	// Update is called once per frame
	void Update () {
		getInput ();


		if (planificadorCPU1.GetComponent<PlanificadorSRTF> ().procesoEnEjecucion == null) {
			//si hay procesos ejecutarl el siguiente
			if (planificadorCPU1.GetComponent<PlanificadorSRTF> ().listos.Count () > 0) {
				planificadorCPU1.GetComponent<PlanificadorSRTF> ().ejecutarSiguienteProceso ();
			}
		}

		float prCPU2 = planificadorCPU2.GetComponent<PlanificadorSRTF> ().totalCPUFloat;
		float prCPU3 = planificadorCPU3.GetComponent<PlanificadorSRTF> ().totalCPUFloat;

		if (prCPU2 > prCPU3) {
			if (planificadorCPU3.GetComponent<PlanificadorSRTF> ().procesoEnEjecucion == null) {
				//si hay procesos ejecutarl el siguiente
				if ( planificadorCPU3.GetComponent<PlanificadorSRTF> ().listos.Count()  > 0) {
					planificadorCPU3.GetComponent<PlanificadorSRTF> ().ejecutarSiguienteProceso ();
				}
			} 
		}
		if (prCPU2 < prCPU3) {
			if (planificadorCPU2.GetComponent<PlanificadorSRTF> ().procesoEnEjecucion == null) {
				//si hay procesos ejecutarl el siguiente
				if (planificadorCPU2.GetComponent<PlanificadorSRTF> ().listos.Count () > 0) {
					planificadorCPU2.GetComponent<PlanificadorSRTF> ().ejecutarSiguienteProceso ();
				}
			} 
		} else {
			if (planificadorCPU2.GetComponent<PlanificadorSRTF> ().procesoEnEjecucion == null) {
				//si hay procesos ejecutarl el siguiente
				if (planificadorCPU2.GetComponent<PlanificadorSRTF> ().listos.Count () > 0) {
					planificadorCPU2.GetComponent<PlanificadorSRTF> ().ejecutarSiguienteProceso ();
				}
			}
		} 

	}

	void getInput(){
		//listos
		/*if (Input.GetKeyDown (KeyCode.Alpha1)) {
			planificadorCPU1.GetComponent<PlanificadorSRTF> ().crearProceso (1);
		}	
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			planificadorCPU2.GetComponent<PlanificadorSRTF> ().crearProceso (1);
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			planificadorCPU3.GetComponent<PlanificadorSRTF> ().crearProceso (1);
		}*/
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

		// suspendido a listo
		if (Input.GetKeyDown (KeyCode.R)) {
			planificadorCPU1.GetComponent<PlanificadorSRTF> ().SuspendidoTolisto ();
		}
		if (Input.GetKeyDown (KeyCode.T)) {
			planificadorCPU2.GetComponent<PlanificadorSRTF> ().SuspendidoTolisto ();
		}
		if (Input.GetKeyDown (KeyCode.Y)) {
			planificadorCPU3.GetComponent<PlanificadorSRTF> ().SuspendidoTolisto ();
		}

		// bloqueado a listo
		if (Input.GetKeyDown (KeyCode.F)) {
			planificadorCPU1.GetComponent<PlanificadorSRTF> ().BloqueadoTolisto ();
		}
		if (Input.GetKeyDown (KeyCode.G)) {
			planificadorCPU2.GetComponent<PlanificadorSRTF> ().BloqueadoTolisto ();
		}
		if (Input.GetKeyDown (KeyCode.H)) {
			planificadorCPU3.GetComponent<PlanificadorSRTF> ().BloqueadoTolisto ();
		}



		// listo a procesador
		/*if (Input.GetKeyDown (KeyCode.Alpha4)) {
			planificadorCPU1.GetComponent<PlanificadorSRTF> ().listoTOprocesador (1);
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			planificadorCPU2.GetComponent<PlanificadorSRTF> ().listoTOprocesador (2);
		}
		if (Input.GetKeyDown (KeyCode.Alpha6)) {
			planificadorCPU3.GetComponent<PlanificadorSRTF> ().listoTOprocesador (3);
		}*/

		// procesador a suspendido
		if (Input.GetKeyDown (KeyCode.I)) {
			planificadorCPU1.GetComponent<PlanificadorSRTF> ().procesadorToSuspendido (1);
		}
		if (Input.GetKeyDown (KeyCode.O)) {
			planificadorCPU2.GetComponent<PlanificadorSRTF> ().procesadorToSuspendido (2);
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			planificadorCPU3.GetComponent<PlanificadorSRTF> ().procesadorToSuspendido (3);
		}

		// procesador a bloqueado
		if (Input.GetKeyDown (KeyCode.J)) {
			planificadorCPU1.GetComponent<PlanificadorSRTF> ().procesadorToBloqueado (1);
		}
		if (Input.GetKeyDown (KeyCode.K)) {
			planificadorCPU2.GetComponent<PlanificadorSRTF> ().procesadorToBloqueado (2);
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			planificadorCPU3.GetComponent<PlanificadorSRTF> ().procesadorToBloqueado (3);
		}

		if (Input.GetKeyDown (KeyCode.M)) {
			planificadorCPU1.GetComponent<PlanificadorSRTF> ().listos = planificadorCPU1.GetComponent<PlanificadorSRTF> ().ordenarCola (planificadorCPU1.GetComponent<PlanificadorSRTF> ().listos);
		}


	}
}

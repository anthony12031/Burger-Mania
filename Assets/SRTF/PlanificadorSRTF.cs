using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanificadorSRTF : MonoBehaviour,IPlanificador {

	PersonajeController controladorPersonaje;
	public int CPU;


	public void crearOrden(int tipoPerro){
		controladorPersonaje.agregarPersonaje (tipoPerro, CPU);
	}

	public void listoToSuspendido(){
		controladorPersonaje.listoToSuspendido (CPU);
	}

	public void listoToBloqueado(){
		controladorPersonaje.listoToBloqueado (CPU);
	}

	public void SuspendidoTolisto(){
		controladorPersonaje.suspendidoTOlisto (CPU);
	}

	public void BloqueadoTolisto(){
		controladorPersonaje.bloqueadoToListo ();
	}

	public void listoTOprocesador(int CPU){
		controladorPersonaje.listoToProcesador (CPU);
	}

	public void procesadorToSuspendido(int CPU){
		controladorPersonaje.procesadorToSuspendido (CPU);
	}

	public void procesadorToBloqueado(int CPU){
		controladorPersonaje.procesadorToBloqueado (CPU);
	}

	// Use this for initialization
	void Start () {
		controladorPersonaje = GetComponent<PersonajeController> ();
	}
	
	// Update is called once per frame
	void Update () {
		controladorPersonaje.updateVistaColas (CPU);
	}
}

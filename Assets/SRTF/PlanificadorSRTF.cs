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

	// Use this for initialization
	void Start () {
		controladorPersonaje = GetComponent<PersonajeController> ();
	}
	
	// Update is called once per frame
	void Update () {
		controladorPersonaje.updateVistaColas (CPU);
	}
}

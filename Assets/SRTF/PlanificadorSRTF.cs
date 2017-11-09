using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PlanificadorSRTF : MonoBehaviour,IPlanificador {

	public delegate void eventoDeHiloDelegate(string tipoEvento);


	Queue<ProcesoSRTF> listos;
	Queue<ProcesoSRTF> suspendidos;

	PersonajeController controladorPersonaje;
	public int CPU;

	public void eventoDeHilo(string evento){
	
	}

	public void crearProceso(int tipoPerro){
		ProcesoSRTF nuevoProceso = new ProcesoSRTF (this, CPU);
		Thread hiloProceso = new Thread (new ThreadStart (nuevoProceso.ejecutar));
		hiloProceso.Start ();

		//necesita tomate
		if (tipoPerro == 1) {
			nuevoProceso.recursos.Add (Recursos.lista ["salsaTomate"]);
		}
		//necesita mostaza
		if (tipoPerro == 2) {
			nuevoProceso.recursos.Add (Recursos.lista ["mostaza"]);
		}
		listos.Enqueue (nuevoProceso);
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
		listos = new Queue<ProcesoSRTF> ();
		suspendidos = new Queue<ProcesoSRTF> ();
	}

	
	// Update is called once per frame
	void Update () {
		controladorPersonaje.updateVistaColas (CPU);
		if (listos.Count > 0) {
			ProcesoSRTF procesoAejecutar = listos.Dequeue ();
			//verificar que los recursos esten libres
			bool recursosLibres = true;
			foreach(Recursos.Recurso recurso in procesoAejecutar.recursos){
				if (!recurso.libre) {
					recursosLibres = false;
					break;
				}
			}
			//si los recursos estan libres ejecutar y bloquear los recursos que usa
			if (recursosLibres) {
				procesoAejecutar.eventoDeEjecucion.Set ();
				procesoAejecutar.enEjecucion = false;
			} 
			//sino pasarlo a bloqueado hasta que se libere el recurso
			else {
			
			}
		}
	}
}

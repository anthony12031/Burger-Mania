using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class planificador : MonoBehaviour {

	seleccionTipoPerro seleccionPerro;
	public PersonajeController controladorPersonajes;
	public Text quantumTX;

	//orden analogo a proceso
	public GameObject Orden;
	//colas de los procesos
	Queue<Proceso> listos;
	Queue<Proceso> bloqueados;
	Queue<Proceso> suspendidos;

	Proceso procesoEnEjecucion;

	public class Proceso
	{
		float Quantum = 20; //segundos;
		float tiempoRestante;

		public Proceso(){
			tiempoRestante = Quantum;
		}

		public void ejecutar(float tiempo){
			if (tiempoRestante > 0)
				tiempoRestante -= tiempo;
			else
				tiempoRestante = 0;
		}

		public float getTiempoRestante(){
			return tiempoRestante;
		}

	}

	//crear proceso
	public void crearOrden(){
		Debug.Log ("crear orden");
		Debug.Log ("tipo perro: " + seleccionPerro.getTipoPerro ());
		controladorPersonajes.agregarPersonaje(1);
		Debug.Log (Recursos.getEstadoRecurso ("salsaTomate"));

		Proceso nuevoProceso = new Proceso ();
		listos.Enqueue (nuevoProceso);

	}

	// Use this for initialization
	void Start () {
		seleccionPerro = GetComponent<seleccionTipoPerro> ();
		listos = new Queue<Proceso> ();
		bloqueados = new Queue<Proceso> ();
		suspendidos = new Queue<Proceso> ();
	}

	public static void notificacionProcesoTerminado(){
		Debug.Log ("Termino proceso");
	}
	
	// Update is called once per frame
	void Update () {
		if (procesoEnEjecucion == null) {
			if (listos.Count > 0 ) {
				procesoEnEjecucion = listos.Dequeue ();
			}
		} 
		else {
			procesoEnEjecucion.ejecutar (Time.deltaTime);	
			quantumTX.text = System.Convert.ToString (procesoEnEjecucion.getTiempoRestante ());
		}

	}
}

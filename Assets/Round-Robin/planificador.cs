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
	 Queue<Proceso>  listos;
	 Queue<Proceso> bloqueados;
	 Queue<Proceso> suspendidos;

	static Proceso  procesoEnEjecucion;

	public class Proceso
	{
		public GameObject cliente;
		float Quantum = 5; //segundos;
		public bool haFinalizado = false;
		planificador planificador;
		bool enEjecucion = false;

		public Proceso(GameObject cliente,planificador plan){
			this.cliente = cliente;
			planificador = plan;
		}

		public void ejecutar(float tiempo){
			
				if (Quantum > 0)
					Quantum -= tiempo;
				else {
					Quantum = 0;
					planificador.notificacionQuantumTerminado ();
				}

		}


		public float getTiempoRestante(){
			return Quantum;
		}

		public void setQuantum(float val){
			Quantum = val;
		}

	}

	//crear proceso
	public void crearOrden(){
		//Debug.Log ("crear orden");
		//Debug.Log ("tipo perro: " + seleccionPerro.getTipoPerro ());
		GameObject cliente =  controladorPersonajes.agregarPersonaje();
		//Debug.Log (Recursos.getEstadoRecurso ("salsaTomate"));

		Proceso nuevoProceso = new Proceso (cliente,this);
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
		procesoEnEjecucion = null;
	}

	public  void notificacionQuantumTerminado(){
		Debug.Log ("quantum acabo");
		if (!procesoEnEjecucion.haFinalizado)
			suspendidos.Enqueue (procesoEnEjecucion);
		procesoEnEjecucion = null;
	
		//suspendidos.Enqueue (procesoEnEjecucion);
		//procesoEnEjecucion = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (procesoEnEjecucion == null) {
			if (listos.Count > 0 ) {
				procesoEnEjecucion = listos.Dequeue ();
				controladorPersonajes.atenderCliente ();
			}
		} 
		else {
			quantumTX.text = System.Convert.ToString (procesoEnEjecucion.getTiempoRestante ());
			procesoEnEjecucion.ejecutar (Time.deltaTime);	

		}

	}
}

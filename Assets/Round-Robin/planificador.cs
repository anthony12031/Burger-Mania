using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class planificador : MonoBehaviour {

	//orden analogo a proceso
	public GameObject Orden;
	//colas de los procesos
	Queue<Proceso>  listos;
	Queue<Proceso> bloqueados;
	Queue<Proceso> suspendidos;
	//el proceso que se esta ejecutando actualmente
	static Proceso  procesoEnEjecucion;

	seleccionTipoPerro seleccionPerro;
	public PersonajeController controladorPersonajes;
	public Text quantumTX;

	//controlador Panes
	public PanControlador panControlador;
	public Vector3 posAtendido;



	public class Proceso
	{
		public GameObject cliente;
		public GameObject clienteOriginal;
		public GameObject perroCaliente;
		float Quantum = 5; //segundos;
		public int tipoPerro;

		float tiempoEnSuspendido = 5;//segundos

		public bool haFinalizado = false;
		planificador planificador;
		bool enEjecucion = false;

		public Proceso(GameObject cliente,planificador plan,int perro){
			this.cliente = cliente;
			this.clienteOriginal = cliente;
			planificador = plan;
			tipoPerro = perro;
		}

		public void ejecutar(float tiempo){
			
				if (Quantum > 0)
					Quantum -= tiempo;
				else {
					Quantum = 0;
					planificador.notificacionQuantumTerminado ();
				}
		}

		public void tiempoEnSuspendidoTick(float tiempo){
			if(tiempoEnSuspendido >0 )
				tiempoEnSuspendido -= tiempo;
		}

		public float getTiempoEnSuspendidoRestante(){
			return tiempoEnSuspendido;
		}

		public float getQuantumRestante(){
			return Quantum;
		}

		public void setQuantum(float val){
			Quantum = val;
		}

	}

	//crear proceso
	public void crearOrden(){
		Debug.Log ("crear proceso");
		GameObject cliente =  controladorPersonajes.agregarPersonaje(seleccionPerro.getTipoPerro()+1);
		Proceso nuevoProceso = new Proceso (cliente,this,seleccionPerro.getTipoPerro()+1);
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
		if (!procesoEnEjecucion.haFinalizado) {
			suspendidos.Enqueue (procesoEnEjecucion);
			//llamar metodo para encolar en suspendido el cliente

		}
		procesoEnEjecucion = null;
	}


	void ejecutarProceso(){
		    procesoEnEjecucion = listos.Dequeue ();
			controladorPersonajes.listoTOprocesador();
	}
	
	// Update is called once per frame
	void Update () {
		if (procesoEnEjecucion == null) {
			if (listos.Count > 0 ) {
				ejecutarProceso ();
			}
		} 
		//ejecutar el proceso actual
		else {
			quantumTX.text = System.Convert.ToString (procesoEnEjecucion.getQuantumRestante());
			procesoEnEjecucion.ejecutar (Time.deltaTime);	
		}
		//aumentar el contador de los procesos en suspendido
			foreach (Proceso pr in suspendidos) {
				pr.tiempoEnSuspendidoTick (Time.deltaTime);
			}
		//pasar a la cola de listos los procesos que ya acabaron su tiempo en suspendido
		if (suspendidos.Count > 0) {
			if (suspendidos.Peek ().getTiempoEnSuspendidoRestante () <= 0) {
				Proceso pr = suspendidos.Dequeue ();
				//pasar  a la cola de listos este proceso

			}
		}

	}
}

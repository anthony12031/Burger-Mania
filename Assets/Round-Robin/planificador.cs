using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class planificador : MonoBehaviour {


	public float tiempoQuantum = 2;
	public float tiempoSuspendido = 2;
	public float tiempoEnBloqueado = 2;
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

	public int CPU;



	public class Proceso
	{
		public GameObject cliente;
		public GameObject clienteOriginal;
		public GameObject perroCaliente;
		public float Quantum ; //segundos;
		public int tipoPerro;

		public float tiempoEnSuspendido ;//segundos

		public bool haFinalizado = false;
		planificador planificador;
		bool enEjecucion = false;

		public Proceso(GameObject cliente,planificador plan,int perro){
			this.cliente = cliente;
			this.clienteOriginal = cliente;
			Quantum = plan.tiempoQuantum;
			tiempoEnSuspendido = plan.tiempoSuspendido;
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
			
	}

	//crear proceso
	public void crearOrden(){
		Debug.Log ("crear proceso");
		GameObject cliente =  controladorPersonajes.agregarPersonaje(seleccionPerro.getTipoPerro()+1,1);
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

	public void terminarProceso(){
		//sumar puntos 
		//eliminar proceso en ejecucion actual
		Recursos.Recurso recurso;
		if (Recursos.recursosEnUso.TryGetValue(procesoEnEjecucion,out recurso)) {
			recurso.libre = true;
			Debug.Log ("liberando recurso: " + recurso.nombre);
			Recursos.recursosEnUso.Remove (procesoEnEjecucion);
		}
		if (CPU == 1) {
			Debug.Log (PanControlador.posParrilla1.libre);
			if (!PanControlador.posParrilla1.libre)
				PanControlador.posParrilla1.libre = true;
		}

		controladorPersonajes.terminarProcesoActual();
		procesoEnEjecucion = null;
	}

	public  void notificacionQuantumTerminado(){
		Debug.Log ("quantum acabo");
		if (!procesoEnEjecucion.haFinalizado) {
			suspendidos.Enqueue (procesoEnEjecucion);
			Recursos.Recurso recurso;
			if (Recursos.recursosEnUso.TryGetValue(procesoEnEjecucion,out recurso)) {
				recurso.libre = true;
				Debug.Log ("liberando recurso: " + recurso.nombre);
				Recursos.recursosEnUso.Remove (procesoEnEjecucion);
			}

			//llamar metodo para encolar en suspendido el cliente
			controladorPersonajes.procesadorToSuspendido(CPU);
		}
		procesoEnEjecucion = null;
	}


	void ejecutarProceso(){
		Proceso procesoAEjecutar = listos.Dequeue ();
		bool recursoOcupado = false; 
			//identificar que recursos necesita el proceso 
			//si estan disponibles ejecutarlo
			//si no pasarlo a la cola de bloqueados
		GameObject pedido = procesoAEjecutar.cliente.transform.GetChild (0).gameObject;

		if (pedido.name.Contains ("pedido_perroTomate")) {
			if (!Recursos.lista ["salsaTomate"].libre) {
				recursoOcupado = true;
			} else {
				Recursos.recursosEnUso.Add (procesoAEjecutar, Recursos.lista ["salsaTomate"]);
				Debug.Log ("usando recurso: " + Recursos.lista ["salsaTomate"].nombre);
				Recursos.lista ["salsaTomate"].libre = false;
			}

		}
		if (pedido.name.Contains ("pedido_perroMostaza")) {
			if (!Recursos.lista ["mostaza"].libre) {
				recursoOcupado = true;
			} else {
				Recursos.recursosEnUso.Add (procesoAEjecutar, Recursos.lista ["mostaza"]);
				Debug.Log ("usando recurso: " + Recursos.lista ["mostaza"].nombre);
				Recursos.lista ["mostaza"].libre = false;
			}
		}

		if (recursoOcupado) {
			bloqueados.Enqueue (procesoAEjecutar);
			controladorPersonajes.listoToBloqueado (CPU);
		} else {
			procesoEnEjecucion = procesoAEjecutar;
			controladorPersonajes.listoToProcesador (CPU);
		}

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
				listos.Enqueue (pr);
				pr.Quantum = tiempoQuantum;
				pr.tiempoEnSuspendido = tiempoSuspendido;
				Debug.Log("mandar a listo");
				controladorPersonajes.suspendidoTOlisto(1);
			}
		}

	}
}

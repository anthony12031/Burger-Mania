using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class planificador : MonoBehaviour,IPlanificador {

	public bool esAutomatico = false;
	public float tiempoQuantum = 2;
	public float tiempoSuspendido = 2;
	public float tiempoEnBloqueado = 2;
	public int x=0;
	float timed = 0;
	//orden analogo a proceso
	public GameObject Orden;
	//colas de los procesos
	public Queue<Proceso>  listos;
	Queue<Proceso> suspendidos;

	//el proceso que se esta ejecutando actualmente
	Proceso  procesoEnEjecucion;

	seleccionTipoPerro seleccionPerro;
	public PersonajeController controladorPersonajes;

	public PersonajeController controladorPersonajesCPU1;
	public PersonajeController controladorPersonajesCPU2;
	public PersonajeController controladorPersonajesCPU3;

	public planificador planificadorCPU1;
	public planificador planificadorCPU2;
	public planificador planificadorCPU3;

	public Text quantumTX;

	//controlador Panes
	public PanControlador panControlador;
	public Vector3 posAtendido;

	public int CPU;

	public Text TTLText;



	public class Proceso
	{
		public GameObject cliente;
		public GameObject clienteOriginal;
		public GameObject perroCaliente;
		public float Quantum ; //segundos;
		public int tipoPerro;
		public float TTL = 30;
		public int CPU;

		public float tiempoEnSuspendido ;//segundos

		public bool haFinalizado = false;
		planificador planificador;
		bool enEjecucion = false;

		public Proceso(GameObject cliente,planificador plan,int perro,int CPU){
			this.cliente = cliente;
			this.clienteOriginal = cliente;
			this.CPU = CPU;
			Quantum = plan.tiempoQuantum;
			tiempoEnSuspendido = plan.tiempoSuspendido;
			planificador = plan;
			tipoPerro = perro;
			this.TTL = Random.Range(10,30);
		}

		public void ejecutar(float tiempo){
				
			if (planificador.esAutomatico) {
				if (Quantum > TTL) {
					Quantum = TTL;			
				}
				if (TTL <= 0) {
					Debug.Log ("termino proceso");
					//planificador.terminarProceso ();
					return;
				}
			}
			if (Quantum > 0) {
				Quantum -= tiempo;
				TTL -= tiempo;
			}
					
				else {
					Quantum = 0;
					//planificador.notificacionQuantumTerminado ();
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
	public void crearProceso(int tipoPerro){
		Debug.Log ("crear proceso");
		GameObject cliente =  controladorPersonajes.agregarPersonaje(tipoPerro,1);
		Proceso nuevoProceso = new Proceso (cliente,this,tipoPerro,CPU);
		listos.Enqueue (nuevoProceso);
	}



	// Use this for initialization
	void Start () {
		seleccionPerro = GetComponent<seleccionTipoPerro> ();
		listos = new Queue<Proceso> ();
		suspendidos = new Queue<Proceso> ();
	}

	public  void notificacionProcesoTerminado(){
		Debug.Log ("Termino proceso");
		procesoEnEjecucion = null;
	}

	public void terminarProceso(){
		//sumar puntos 
		//eliminar proceso en ejecucion actual
		Recursos.Recurso recurso;
		if (CPU == 1) {
			Debug.Log (PanControlador.posParrilla1.libre);
			if (!PanControlador.posParrilla1.libre)
				PanControlador.posParrilla1.libre = true;
		}
		liberarRecursos ();
		controladorPersonajes.terminarProcesoActual();
		procesoEnEjecucion = null;
	}

	public  void notificacionQuantumTerminado(){
		Debug.Log ("quantum acabo");
		if (!procesoEnEjecucion.haFinalizado) {
			liberarRecursos ();
			suspendidos.Enqueue (procesoEnEjecucion);
			//llamar metodo para encolar en suspendido el cliente
			controladorPersonajes.procesadorToSuspendido(CPU);
		}
		procesoEnEjecucion = null;
	}

	public void liberarRecursos(){
		Recursos.Recurso recurso;
		if (Recursos.recursosEnUso.TryGetValue(procesoEnEjecucion,out recurso)) {
			recurso.libre = true;
			Debug.Log ("liberando recurso: " + recurso.nombre);
			Recursos.recursosEnUso.Remove (procesoEnEjecucion);
			Debug.Log ("procesos en bloqueo x recurso: "+Recursos.bloqueados [recurso.nombre].Count);
			Queue<Proceso> prBloqueados = Recursos.bloqueados [recurso.nombre];
			while (prBloqueados.Count > 0) {
				Proceso pr = prBloqueados.Dequeue ();
				//Debug.Log (prBloqueados.Dequeue ());
				//listos.Enqueue (prBloqueados.Dequeue ());
				Debug.Log("proceso de CPU: "+pr.CPU);
				if (pr.CPU == 1) {
					controladorPersonajesCPU1.bloqueadoToListo ();
					planificadorCPU1.listos.Enqueue (pr);
				}
				if (pr.CPU == 2) {
					controladorPersonajesCPU2.bloqueadoToListo ();
					planificadorCPU2.listos.Enqueue (pr);
				}
				if (pr.CPU == 3) {
					controladorPersonajesCPU3.bloqueadoToListo ();
					planificadorCPU3.listos.Enqueue (pr);
				}
			}
		}

	}


	void ejecutarProceso(){
		Proceso procesoAEjecutar = listos.Dequeue ();
		bool recursoOcupado = false; 
			//identificar que recursos necesita el proceso 
			//si estan disponibles ejecutarlo
			//si no pasarlo a la cola de bloqueados
		GameObject pedido = procesoAEjecutar.cliente.transform.GetChild (0).gameObject;
		string nombreRecursoOcupado = "";
		if (pedido.name.Contains ("pedido_perroTomate")) {
			if (!Recursos.lista ["salsaTomate"].libre) {
				recursoOcupado = true;
				nombreRecursoOcupado = "salsaTomate";
			} else {
				Recursos.recursosEnUso.Add (procesoAEjecutar, Recursos.lista ["salsaTomate"]);
				Debug.Log ("usando recurso: " + Recursos.lista ["salsaTomate"].nombre);
				Recursos.lista ["salsaTomate"].libre = false;
			}

		}
		if (pedido.name.Contains ("pedido_perroMostaza")) {
			if (!Recursos.lista ["mostaza"].libre) {
				recursoOcupado = true;
				nombreRecursoOcupado = "mostaza";
			} else {
				Recursos.recursosEnUso.Add (procesoAEjecutar, Recursos.lista ["mostaza"]);
				Debug.Log ("usando recurso: " + Recursos.lista ["mostaza"].nombre);
				Recursos.lista ["mostaza"].libre = false;
			}
		}

		if (recursoOcupado) {
			Recursos.bloqueados[nombreRecursoOcupado].Enqueue (procesoAEjecutar);
			controladorPersonajes.listoToBloqueado (CPU);
		} else {
			procesoEnEjecucion = procesoAEjecutar;
			//calcular quantum
			if (esAutomatico && listos.Count>0) {
				float media = 0;
				foreach (Proceso pr in listos) {
					media += pr.TTL;
				} 
				media /= listos.Count;
				Debug.Log ("media: "+media);
				float varianza = 0;
				foreach (Proceso pr in listos) {	
					Debug.Log (pr.TTL - media);
					varianza += (pr.TTL - media)*(pr.TTL - media);
				}
				varianza /= listos.Count;
				float desviacion = +Mathf.Sqrt (varianza);
				Debug.Log ("desviacion: "+Mathf.Sqrt(varianza));
				if (desviacion > 0) {
					procesoAEjecutar.Quantum = desviacion;
					Debug.Log ("Q de proceso: "+procesoAEjecutar.Quantum);
				}
			}			
			controladorPersonajes.listoToProcesador (CPU);
		}
	}

	void hilo()
	{
		Thread myThread = new System.Threading.Thread(delegate(){
			procesoEnEjecucion.ejecutar (timed);
		});
		myThread.Start();
	}



	// Update is called once per frame
	void Update () {

		timed = Time.deltaTime;
		if (procesoEnEjecucion == null) {
			if (listos.Count > 0 ) {
				ejecutarProceso ();
			}
		} 
		//ejecutar el proceso actual
		else {
			//quantumTX.text = System.Convert.ToString (procesoEnEjecucion.getQuantumRestante());
			//if (TTLText != null) {
			//	TTLText.text = System.Convert.ToString (procesoEnEjecucion.TTL);
			//	if (procesoEnEjecucion.TTL < 0)
			//		TTLText.text = "0";
			//}
			//if(procesoEnEjecucion.getQuantumRestante()<0)
			//	quantumTX.text = "0";


			hilo ();
			if (procesoEnEjecucion.TTL <= 0) {
				Debug.Log ("termino proceso");
				terminarProceso ();
			}

			if (procesoEnEjecucion != null) {
				if (procesoEnEjecucion.Quantum <= 0) {
					procesoEnEjecucion.Quantum = 0;
					/*if (procesoEnEjecucion.CPU == 1) {
						planificadorCPU1.notificacionQuantumTerminado ();
					}
					if (procesoEnEjecucion.CPU == 2) {
						planificadorCPU2.notificacionQuantumTerminado ();
					}
					if (procesoEnEjecucion.CPU == 3) {
						planificadorCPU3.notificacionQuantumTerminado ();
					}*/
					notificacionQuantumTerminado ();
				}
			}

		//	Debug.Log (procesoEnEjecucion.Quantum);
		}
		//aumentar el contador de los procesos en suspendido
			foreach (Proceso pr in suspendidos) {
				pr.tiempoEnSuspendidoTick (Time.deltaTime);
			}

		if (listos.Count == 0) {
			if (suspendidos.Count > 0) 
				suspendidos.Peek ().tiempoEnSuspendido = 0;
		}

		//pasar a la cola de listos los procesos que ya acabaron su tiempo en suspendido
		if (suspendidos.Count > 0) {
			if (suspendidos.Peek ().getTiempoEnSuspendidoRestante () <= 0) {
				Proceso pr = suspendidos.Dequeue ();
				//pasar  a la cola de listos este proceso
				listos.Enqueue (pr);
				pr.Quantum = tiempoQuantum;
				pr.tiempoEnSuspendido = tiempoSuspendido;
				controladorPersonajes.suspendidoTOlisto(1);
			}
		}

	}
}

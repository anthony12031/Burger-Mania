﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class planificador : MonoBehaviour,IPlanificador {

	public ColaMDespachador despachador;
	int personajeContador = 0;
	public int personajeContador1 = 0;
	public int personajeContador2 = 0;
	public int personajeContador3 = 0;
	public bool esAutomatico = false;
	public float tiempoQuantum = 2;
	public float tiempoSuspendido = 2;
	public float tiempoEnBloqueado = 2;
	public int x=0;
	float timed = 0;
	//orden analogo a proceso
	public GameObject Orden;
	//colas de los procesos
	public Cola<Proceso>  listos;
	public Cola<Proceso> suspendidos;
	public Cola<Proceso> bloqueados;
	//el proceso que se esta ejecutando actualmente
	public Proceso  procesoEnEjecucion;
	public gantt diagrama;


	//seleccionTipoPerro seleccionPerro;
	public RRPersonajeController controladorPersonaje;

	//public PersonajeController controladorPersonajesCPU1;
	//public PersonajeController controladorPersonajesCPU2;
	//public PersonajeController controladorPersonajesCPU3;

	public planificador planificadorCPU1;
	public planificador planificadorCPU2;
	public planificador planificadorCPU3;

	public Text quantumTX;

	//controlador Panes
	public PanControlador panControlador;
	public Vector3 posAtendido;

	public int CPU;

	public Text TTLText;



	public class Proceso:AProceso
	{
		
		public GameObject clienteOriginal;
		public GameObject perroCaliente;
		public float Quantum ; //segundos;
		public int tipoPerro;
		public int CPU;
		public TextMesh textoTTL;
		public TextMesh textoQuantum;
		public float envejecimiento = 0;
		public float tiempoEnSuspendido = 5 ;//segundos

		public bool haFinalizado = false;
		IPlanificador planificador;
		bool enEjecucion = false;


		public Proceso(IPlanificador plan,int CPU,GameObject rep,float t):base(plan,CPU,t){
			//recursos = new List<Recursos.Recurso> ();
			textoTTL = rep.transform.GetChild (0).GetChild(0).GetComponent<TextMesh> ();
			textoTTL.text = System.Convert.ToString(t);
			enEjecucion = true;
			representacion = rep;
			textoQuantum = Instantiate(textoTTL);
			textoQuantum.transform.parent = textoTTL.transform.parent; 
			textoQuantum.transform.localPosition = new Vector2(textoTTL.transform.localPosition.x,-0.1f);
			textoQuantum.transform.localScale = textoTTL.transform.localScale ;
			textoQuantum.text = "Q: ";
			Debug.Log(textoQuantum);
			this.CPU = CPU;
			planificador = plan;
		}


		public override void ejecutar (){
			
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
	public void crearProceso(int tipoPerro,float tiempo){
		Debug.Log ("crear proceso RR");
		switch (CPU) {
		case 1:
			personajeContador1++;
			personajeContador = personajeContador1;
			break;
		case 2:
			personajeContador2++;
			personajeContador = personajeContador2;
			break;
		case 3:
			personajeContador3++;
			personajeContador = personajeContador3;
			break;
		}
		int lista = controladorPersonaje.PJlista;
		GameObject representacion = controladorPersonaje.agregarPersonaje (tipoPerro, CPU, personajeContador);
		Debug.Log (representacion);
		//GameObject cliente =  controladorPersonajes.agregarPersonaje(tipoPerro,1,1);
		Proceso nuevoProceso = new Proceso(this,CPU,representacion,tiempo);
		listos.Enqueue (nuevoProceso);
		diagrama.agregarPersonaje (CPU, lista, nuevoProceso.representacion.GetComponent<Personaje> ().id);
		diagrama.inicio = true;

		//necesita tomate
		if (tipoPerro == 1) {
			nuevoProceso.recurso = Recursos.lista ["salsaTomate"];
		}
		//necesita mostaza
		if (tipoPerro == 2) {
			nuevoProceso.recurso = Recursos.lista ["mostaza"];
		}

	}



	// Use this for initialization
	void Start () {
		//seleccionPerro = GetComponent<seleccionTipoPerro> ();
		listos = new Cola<Proceso> ();
		suspendidos = new Cola<Proceso> ();
		bloqueados = new Cola<Proceso> ();
		controladorPersonaje = GetComponent<RRPersonajeController>();
	}

	public  void notificacionProcesoTerminado(){
		Debug.Log ("Termino proceso");
		procesoEnEjecucion = null;
	}

	public void terminarProceso(){  
		controladorPersonaje.terminarProcesoActual (procesoEnEjecucion.representacion);
		procesoEnEjecucion.recurso.libre = true;
		procesoEnEjecucion = null;
		despachador.notificacionProcesoTerminado ();
		/*if (CPU == 2 || CPU == 3) {
			totalCPUFloat += 1;
		}*/
	}

	public  void notificacionQuantumTerminado(){
		Debug.Log ("quantum acabo");
		if (!procesoEnEjecucion.haFinalizado) {
			liberarRecursos ();
			suspendidos.Enqueue (procesoEnEjecucion);
			//llamar metodo para encolar en suspendido el cliente
			//controladorPersonajes.procesadorToSuspendido(CPU);
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
					//controladorPersonajesCPU1.bloqueadoToListo ();
					planificadorCPU1.listos.Enqueue (pr);
				}
				if (pr.CPU == 2) {
					//controladorPersonajesCPU2.bloqueadoToListo ();
					planificadorCPU2.listos.Enqueue (pr);
				}
				if (pr.CPU == 3) {
					//controladorPersonajesCPU3.bloqueadoToListo ();
					planificadorCPU3.listos.Enqueue (pr);
				}
			}
		}
	}

	public void calcularQuantum(Proceso proceso){
		float media = 0;
		//calcular la media
		Cola<Proceso> lisTemp = new Cola<Proceso>();
		while(listos.Count()>0){
			Proceso pr = listos.Dequeue ();
			media += pr.TTL;
			lisTemp.Enqueue (pr);
		}
		listos = lisTemp;

		media /= listos.Count();
		Debug.Log ("media: "+media);
		float varianza = 0;
		lisTemp = new Cola<Proceso>();
		while(listos.Count()>0){
			Proceso pr = listos.Dequeue ();
			varianza += (pr.TTL - media)*(pr.TTL - media);
			lisTemp.Enqueue (pr);
		}
		listos = lisTemp;
		varianza /= listos.Count();
		Debug.Log ("varianza: "+varianza);
		float desviacion = +Mathf.Sqrt (varianza);
		Debug.Log ("desviacion: "+Mathf.Sqrt(varianza));
		if (desviacion < proceso.TTL && desviacion >0) {
			proceso.Quantum = desviacion;
		}
		else
			proceso.Quantum = proceso.TTL;
	}


	void ejecutarProceso(){

		Proceso procesoAejecutar = listos.Dequeue ();
		Debug.Log ("ejecutar proceso RR");
		//verificar que los recursos esten libres
		//si los recursos estan libres ejecutar y bloquear los recursos que usa
		if (procesoAejecutar.recurso.libre) {
			//procesoAejecutar.eventoDeEjecucion.Set ();
			procesoEnEjecucion = procesoAejecutar;
			controladorPersonaje.listoToProcesador (CPU, procesoEnEjecucion.representacion);
			procesoAejecutar.recurso.libre = false;
			calcularQuantum (procesoAejecutar);
		} 
		//sino pasarlo a bloqueado hasta que se libere el recurso
		else {
			Debug.Log("recurso en uso");
			bloqueados.Enqueue (procesoAejecutar);
			controladorPersonaje.listoToBloqueado (CPU, procesoAejecutar.representacion);
		}

	}
		
	public void planificar(){
		
		timed = Time.deltaTime;
		if (procesoEnEjecucion == null) {
			if (listos.Count() > 0 ) {
				ejecutarProceso ();
			}
		} 
		//ejecutar el proceso actual
		else {
			
			procesoEnEjecucion.TTL -= Time.deltaTime;
			procesoEnEjecucion.Quantum -= Time.deltaTime;
			procesoEnEjecucion.textoTTL.text = System.Convert.ToString(procesoEnEjecucion.TTL);
			procesoEnEjecucion.textoQuantum.text = "Q: "+System.Convert.ToString(procesoEnEjecucion.Quantum);
			procesoEnEjecucion.textoTTL.tag = "texto";
			procesoEnEjecucion.textoQuantum.tag = "texto";

			if (procesoEnEjecucion.TTL <= 0) {
				Debug.Log ("termino proceso");
				terminarProceso ();
			}

			if (procesoEnEjecucion != null) {
				//Debug.Log ("Q: " + procesoEnEjecucion.Quantum);
				if (procesoEnEjecucion.Quantum <= 0) {
					suspendidos.Enqueue (procesoEnEjecucion);
					procesoEnEjecucion.recurso.libre = true;
					controladorPersonaje.procesadorToSuspendido (CPU,procesoEnEjecucion.representacion);
					procesoEnEjecucion = null;
				}
			}

		}

		/*foreach (Proceso pr in suspendidos) {
			pr.tiempoEnSuspendidoTick (Time.deltaTime);
		}*/

	}

	// Update is called once per frame
	void Update () {
		controladorPersonaje.updateVistaColas (CPU);

		//actualizar tiempo suspendido
		Cola<Proceso> susTemp = new Cola<Proceso>();
		while(suspendidos.Count()>0){
			Proceso pr = suspendidos.Dequeue ();
			pr.tiempoEnSuspendidoTick(Time.deltaTime);
			if (pr.tiempoEnSuspendido <= 0) {
				listos.Enqueue (pr);
				controladorPersonaje.suspendidoTOlisto (CPU,pr.representacion);
			} else {
				susTemp.Enqueue (pr);
			}

		}
		suspendidos = susTemp;

		//actualizar bloqueados
		Cola<Proceso> bloTemp = new Cola<Proceso>();
		while(bloqueados.Count()>0){
			Proceso pr = bloqueados.Dequeue ();
			if (pr.recurso.libre) {
				listos.Enqueue (pr);
				controladorPersonaje.bloqueadoToListo (pr.representacion);
			} else {
				bloTemp.Enqueue (pr);
			}
		}
		bloqueados = bloTemp;

		//si no hay procesos en listo no esperar en suspendido
		if (listos.Count() == 0) {
			if (suspendidos.Count() > 0) 
				suspendidos.Peek ().tiempoEnSuspendido = 0;
		}

		//pasar a la cola de listos los procesos que ya acabaron su tiempo en suspendido
		if (suspendidos.Count() > 0) {
			if (suspendidos.Peek ().getTiempoEnSuspendidoRestante () <= 0) {
				Proceso pr = suspendidos.Dequeue ();
				//pasar  a la cola de listos este proceso
				listos.Enqueue (pr);
				pr.Quantum = tiempoQuantum;
				pr.tiempoEnSuspendido = tiempoSuspendido;
				//controladorPersonajes.suspendidoTOlisto(1);
			}
		}

		//planificar ();
	}
}

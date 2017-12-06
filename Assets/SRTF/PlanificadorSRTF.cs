using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class PlanificadorSRTF : MonoBehaviour,IPlanificador {

	public delegate void eventoDeHiloDelegate(string tipoEvento);

	public ProcesoSRTF procesoEnEjecucion;
	public float tiempoSpawn;
	public float tiempoTranscurrido;
	public Cola<ProcesoSRTF> listos;
	public Cola<ProcesoSRTF> suspendidos;
	public Cola<ProcesoSRTF> bloqueados;

	public Text totalCPU;
	public float totalCPUFloat = 0;
	int personajeContador = 0;
	public int personajeContador1 = 0;
	public int personajeContador2 = 0;
	public int personajeContador3 = 0;
	public gantt diagrama;


	public nuevoPersonajeController controladorPersonaje;
	public int CPU;

	public void eventoDeHilo(string evento){
	
	}

	// Use this for initialization
	void Start () {
		controladorPersonaje = GetComponent<nuevoPersonajeController> ();
		listos = new Cola<ProcesoSRTF> ();
		suspendidos = new Cola<ProcesoSRTF> ();
		bloqueados = new Cola<ProcesoSRTF> ();
	}

	public Cola<ProcesoSRTF> ordenarCola(Cola<ProcesoSRTF> cola){
		int size = cola.Count();
		Cola<ProcesoSRTF> colaFinal = new Cola<ProcesoSRTF> ();

		ProcesoSRTF menor;
		int contador = 0;

		for (int j=0;j<size;j++) {
		menor = cola.Dequeue ();
			for (int i=0;i<cola.Count();i++) {
			ProcesoSRTF sacar = cola.Dequeue ();
			if (menor.TTL > sacar.TTL) {
				cola.Enqueue (menor);
				menor = sacar;
			} else {
				cola.Enqueue (sacar);
			}
					
		}
		colaFinal.Enqueue (menor);
		}

		return colaFinal;

	}

	public void crearProceso(int tipoPerro,float tiempoProceso){
		
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

		ProcesoSRTF nuevoProceso = new ProcesoSRTF (this, CPU,representacion,tiempoProceso);
		diagrama.agregarPersonaje (CPU, lista, nuevoProceso.representacion.GetComponent<Personaje> ().id);
        diagrama.inicio = true;
		listos.Enqueue (nuevoProceso);	
		Thread hiloProceso = new Thread (new ThreadStart (nuevoProceso.ejecutar));
		//hiloProceso.Start ();
		nuevoProceso.hiloDeEjecucion = hiloProceso;

		//necesita tomate
		if (tipoPerro == 1) {
			nuevoProceso.recurso = Recursos.lista ["salsaTomate"];
		}
		//necesita mostaza
		if (tipoPerro == 2) {
			nuevoProceso.recurso = Recursos.lista ["mostaza"];
		}
	}

	public void ejecutarSiguienteProceso(){
		//Debug.Log ("ejecutando sig proceso");
		ProcesoSRTF procesoAejecutar = listos.Dequeue ();
		Debug.Log ("ejecutar proceso");
		//verificar que los recursos esten libres
		//si los recursos estan libres ejecutar y bloquear los recursos que usa
		if (procesoAejecutar.recurso.libre) {
			//procesoAejecutar.eventoDeEjecucion.Set ();
			procesoEnEjecucion = procesoAejecutar;
			controladorPersonaje.listoToProcesador (CPU, procesoEnEjecucion.representacion);
			procesoAejecutar.recurso.libre = false;
		} 
		//sino pasarlo a bloqueado hasta que se libere el recurso
		else {
			Debug.Log("recurso en uso");
			bloqueados.Enqueue (procesoAejecutar);
			controladorPersonaje.listoToBloqueado (CPU, procesoAejecutar.representacion);
		}
	}

	void terminarProceso(){
		procesoEnEjecucion.enEjecucion = false;   
		controladorPersonaje.terminarProcesoActual (procesoEnEjecucion.representacion);
		procesoEnEjecucion.recurso.libre = true;
		procesoEnEjecucion = null;
		if (CPU == 2 || CPU == 3) {
			totalCPUFloat += 1;
		}
		//liberar recursos
	}

	public bool tieneSalsa(string tipoSalsa,string tagSalsa,Transform parent){
		bool tieneSalsa= false;
		if (parent.childCount > 1) {
			foreach (Transform child in parent.GetChild(1)) {
				if (child.CompareTag (tagSalsa)) {
					tieneSalsa = true;
					break;
				}
			}
		}
		return tieneSalsa;
	}

	public void terminarProcesoManual(){
		if (procesoEnEjecucion != null) {
			Transform parent = procesoEnEjecucion.representacion.transform.parent;

			if (parent != null) {
				if (procesoEnEjecucion.recurso.nombre == "salsaTomate") {
					//verificar si tiene tomate
					bool tieneTomate = tieneSalsa("salsaTomate","salsaTomate",parent);
					bool tieneMostaza = tieneSalsa ("mostaza", "salsaMostaza", parent);
					if (tieneTomate && !tieneMostaza) {
						totalCPUFloat += 1;
					} 
				}
				if (procesoEnEjecucion.recurso.nombre == "mostaza") {
					//verificar si tiene tomate
					bool tieneTomate = tieneSalsa("salsaTomate","salsaTomate",parent);
					bool tieneMostaza = tieneSalsa ("mostaza", "salsaMostaza", parent);
					if (tieneMostaza && !tieneTomate) {
						totalCPUFloat += 1;
					}
				}
			}
			terminarProceso ();
		}
	}
		


	public void planificar(){
		//Debug.Log (procesoEnEjecucion);
		if (procesoEnEjecucion == null) {
			//si hay procesos ejecutarl el siguiente
			if (listos.Count()  > 0) {
				ejecutarSiguienteProceso ();
			}
		} 

		//hay un proceso en ejecucion comparar su TTL con el siguiente proceso
		if(procesoEnEjecucion != null) {
			procesoEnEjecucion.TTL -= Time.deltaTime;
			procesoEnEjecucion.textoTTL.text = System.Convert.ToString(procesoEnEjecucion.TTL);
            procesoEnEjecucion.textoTTL.tag = "texto";

            if (procesoEnEjecucion.TTL <= 0) {
				terminarProceso ();
			}
			//hay un proceso on menor tiempo
			//pasar a suspendido el actual y a ejecucion el proceso con menos tiempo
			if (listos.Count()>0 && procesoEnEjecucion != null) {
				if (procesoEnEjecucion.TTL > listos.Peek ().TTL) {
					suspendidos.Enqueue (procesoEnEjecucion);
					controladorPersonaje.procesadorToSuspendido (CPU,procesoEnEjecucion.representacion);
					procesoEnEjecucion.recurso.libre = true;
					procesoEnEjecucion = null;
				}
			}
		}

		//actualizar total entregas
		totalCPU.text = System.Convert.ToString(totalCPUFloat);

		//crear procesos

		//tiempoTranscurrido += Time.deltaTime;
		if (tiempoTranscurrido >= tiempoSpawn) { 
			int totalprs = bloqueados.Count () + suspendidos.Count () + bloqueados.Count ();
			//Debug.Log ("prs: "+totalprs);
			if (totalprs <= 3) {
				int tipoPerro = 1;
				if (CPU == 1)
					tipoPerro = 1;
				else {
					tipoPerro = 2;
				}
				crearProceso (tipoPerro, Random.Range (3, 8));
				tiempoTranscurrido = 0;
			}
		}

	}

	// Update is called once per frame
	void Update () {
		//organizar cola de listos
		listos = ordenarCola(listos);
		controladorPersonaje.updateVistaColas (CPU);

		//si no hay procesos en listo no esperar en suspendido
		if (listos.Count() == 0) {
			if (suspendidos.Count() > 0) 
				suspendidos.Peek ().tiempoEnSuspendido = 0;
		}

		//actualizar tiempo suspendido
		Cola<ProcesoSRTF> susTemp = new Cola<ProcesoSRTF>();
		while(suspendidos.Count()>0){
			ProcesoSRTF pr = suspendidos.Dequeue ();
			pr.tiempoEnSuspendido -= Time.deltaTime;
			if (pr.tiempoEnSuspendido <= 0) {
				listos.Enqueue (pr);
				controladorPersonaje.suspendidoTOlisto (CPU,pr.representacion);
			} else {
				susTemp.Enqueue (pr);
			}

		}
		suspendidos = susTemp;

		//actualizar bloqueados
		Cola<ProcesoSRTF> bloTemp = new Cola<ProcesoSRTF>();
		while(bloqueados.Count()>0){
			ProcesoSRTF pr = bloqueados.Dequeue ();
			if (pr.recurso.libre) {
				listos.Enqueue (pr);
			} else {
				bloTemp.Enqueue (pr);
			}
		}
		bloqueados = bloTemp;
		//planificar ();
	}
		
	public void listoToSuspendido(){
		//controladorPersonaje.listoToSuspendido (CPU);
	}

	public void listoToBloqueado(){
		//controladorPersonaje.listoToBloqueado (CPU);
	}

	public void SuspendidoTolisto(){
		//controladorPersonaje.suspendidoTOlisto (CPU);
	}

	public void BloqueadoTolisto(){
		//controladorPersonaje.bloqueadoToListo ();
	}

	public void listoTOprocesador(int CPU){
		//controladorPersonaje.listoToProcesador (CPU);
	}

	public void procesadorToSuspendido(int CPU){
		//controladorPersonaje.procesadorToSuspendido (CPU);
	}

	public void procesadorToBloqueado(int CPU){
		//controladorPersonaje.procesadorToBloqueado (CPU);
	}



}

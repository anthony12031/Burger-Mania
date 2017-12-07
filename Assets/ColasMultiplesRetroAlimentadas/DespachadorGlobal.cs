using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DespachadorGlobal : MonoBehaviour {

	seleccionTipoPerro seleccionPerro;
	SeleccionCPU seleccionCPU;
	SeleccionPrioridad seleccionPrioridad;

	public ColaMDespachador despachadorCPU1;
	public ColaMDespachador despachadorCPU2;
	public ColaMDespachador despachadorCPU3;
	public float tiempoProceso = 0;
	public Text totalCPU1;
	public Text totalCPU2;
	public Text totalCPU3;
	public int ItotalCPU1=0;
	public int ItotalCPU2=0;
	public int ItotalCPU3=0;

	//crear procesos cada cierto tiempo
	public bool modoJuego = false;

	// Use this for initialization
	void Start () {
		seleccionPerro = GetComponent<seleccionTipoPerro> ();
		seleccionCPU = GetComponent<SeleccionCPU> ();
		seleccionPrioridad = GetComponent<SeleccionPrioridad> ();
		//inyectarse en los despachadores
		despachadorCPU1.despachadorGlobal = this;
		despachadorCPU2.despachadorGlobal = this;
		despachadorCPU3.despachadorGlobal = this;
	}

	public void spawnProceso(int CPU,int planificador,int tipoPerro,float tiempo){
		if (CPU == 1) {
			if (planificador == 1) {
				despachadorCPU1.planificadorRR.crearProceso (tipoPerro, tiempo);
			}
			if (planificador == 2) {
				despachadorCPU1.planificadorSRTF.crearProceso (tipoPerro, tiempo);
			}
			if (planificador == 3) {
				despachadorCPU1.planificadorFIFO.crearProceso (tipoPerro, tiempo);
			}
		}
		if (CPU == 2) {
			if (planificador == 1) {
				despachadorCPU2.planificadorRR.crearProceso (tipoPerro, tiempo);
			}
			if (planificador == 2) {
				despachadorCPU2.planificadorSRTF.crearProceso (tipoPerro, tiempo);
			}
			if (planificador == 3) {
				despachadorCPU2.planificadorFIFO.crearProceso (tipoPerro, tiempo);
			}
		}
		if (CPU == 3) {
			if (planificador == 1) {
				despachadorCPU3.planificadorRR.crearProceso (tipoPerro, tiempo);
			}
			if (planificador == 2) {
				despachadorCPU3.planificadorSRTF.crearProceso (tipoPerro, tiempo);
			}
			if (planificador == 3) {
				despachadorCPU3.planificadorFIFO.crearProceso (tipoPerro, tiempo);
			}
		}
	}

	float tiempoTranscurrido = 0;
	public float tiempoSpawn = 3;
	int frames = 0;
	// Update is called once per frame
	void Update () {
		if (modoJuego) {
			frames += 1;
			tiempoTranscurrido += Time.deltaTime;
			if (tiempoTranscurrido >= tiempoSpawn) {
				tiempoTranscurrido = 0;
				;
				int tipoPerro = 2;
				int CPU = 2;
				if (frames % 2 != 0) {
					CPU = 3;
				}
				int planificador = Random.Range(1,3);
				float tiempo = Random.Range (3, 15);
				spawnProceso (CPU,planificador, tipoPerro, tiempo);
				spawnProceso (1,planificador, 1, tiempo);
			}

		}
	}

	public void setTiempoProceso(string tiempo){
		Debug.Log (tiempo);
		try{
			tiempoProceso = System.Convert.ToSingle (tiempo);
			Debug.Log(tiempoProceso);
		}catch(System.FormatException){
			Debug.Log("format exception");
		}
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

	public void entregarPerro(){
		Debug.Log ("Entregar perro");
		Debug.Log ("Proceso en ejecucion CPU1");
		if(despachadorCPU1.procesoEnEjecucion != null){
			Transform parent = despachadorCPU1.procesoEnEjecucion.representacion.transform.parent;
			if (parent != null) {
				bool tieneTomate = tieneSalsa("salsaTomate","salsaTomate",parent);
				bool tieneMostaza = tieneSalsa ("mostaza", "salsaMostaza", parent);
				if (despachadorCPU1.procesoEnEjecucion.recurso.nombre == "salsaTomate") {
					//verificar si tiene tomate
					Debug.Log ("tomate ?: " + tieneTomate);
					Debug.Log ("mostaza ?: " + tieneMostaza);
					if (tieneTomate && !tieneMostaza) {
						ItotalCPU1 += 1;
					} 
				}
				if (despachadorCPU1.procesoEnEjecucion.recurso.nombre == "mostaza") {
					//verificar si tiene tomate
					Debug.Log ("tomate ?: " + tieneTomate);
					Debug.Log ("mostaza ?: " + tieneMostaza);
					if (tieneMostaza && !tieneTomate) {
						ItotalCPU1 += 1;
					}
				}
			}
			terminarProceso ();
		}

	}

	public void terminarProceso(){
		if (despachadorCPU1.planificadorProcesoActual == "RR") {
			despachadorCPU1.planificadorRR.terminarProceso ();
		}
		if (despachadorCPU1.planificadorProcesoActual == "SRTF") {
			despachadorCPU1.planificadorSRTF.terminarProceso ();
		}
		if (despachadorCPU1.planificadorProcesoActual == "FIFO") {
			despachadorCPU1.planificadorFIFO.terminarProceso ();
		}
		Transform parent = despachadorCPU1.procesoEnEjecucion.representacion.transform.parent;
		//eliminar el pan si hay
		if( parent!= null){
			Transform pan = parent.GetChild (1);
			if (pan != null) {
				Destroy (pan.gameObject);
			}
		}
		totalCPU1.text = "" + ItotalCPU1;
	}

	public void notificacionProcesoTerminado(int CPU){
		Debug.Log ("proceso terminado cpu: " + CPU);
		if (CPU == 1) {
		
		}
		if (CPU == 2) {
			ItotalCPU2 += 1;
			totalCPU2.text = "" + ItotalCPU2;
		}
		if (CPU == 3) {
			ItotalCPU3 += 1;
			totalCPU3.text = "" + ItotalCPU3;
		}
	}
		
	public void crearOrden(){
		Debug.Log ("crear Proceso");
		int tipoPerro = seleccionPerro.getTipoPerro ()+1;
		int cpu = seleccionCPU.getCPU () + 1;
		int prioridad = seleccionPrioridad.getPrioridad ()+1;
		if (cpu == 1) {
			despachadorCPU1.crearProceso (tipoPerro, tiempoProceso, prioridad);
			//planificadorCPU1.GetComponent<PlanificadorSRTF>().crearProceso (tipoPerro,tiempoProceso);
		}
		if (cpu == 2) {
			despachadorCPU2.crearProceso (tipoPerro, tiempoProceso, prioridad);
			//planificadorCPU2.GetComponent<PlanificadorSRTF>().crearProceso (tipoPerro,tiempoProceso);
		}
		if (cpu == 3) {
			despachadorCPU3.crearProceso (tipoPerro, tiempoProceso, prioridad);
			//planificadorCPU3.GetComponent<PlanificadorSRTF>().crearProceso (tipoPerro,tiempoProceso);
		}

	}


}

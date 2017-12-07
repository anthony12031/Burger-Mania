using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaMDespachador : MonoBehaviour {

	public DespachadorGlobal despachadorGlobal;
	public PlanificadorSRTF planificadorSRTF;
	public planificador planificadorRR;
	public PlanificadorFIFO planificadorFIFO;
	public AProceso procesoEnEjecucion;
	public string planificadorProcesoActual;
	public int CPU;

	// Use this for initialization
	void Start () {
		//planificador SRTF
		planificadorSRTF = transform.GetChild (0).gameObject.GetComponent<PlanificadorSRTF>();
		planificadorSRTF.CPU = CPU;
		planificadorSRTF.despachador = this;
		//planificador RR
		planificadorRR = transform.GetChild (1).gameObject.GetComponent<planificador>();
		planificadorRR.CPU = CPU;
		planificadorRR.despachador = this;
		//planificador FIFO
		planificadorFIFO = transform.GetChild (2).gameObject.GetComponent<PlanificadorFIFO>();
		planificadorFIFO.CPU = CPU;
		planificadorFIFO.despachador = this;
	}

	public void crearProceso(int tipoPerro,float tiempo,int prioridad){
		if (prioridad == 1) {
			planificadorRR.crearProceso (tipoPerro, tiempo);
		}
		if (prioridad == 2) {
			planificadorSRTF.crearProceso (tipoPerro, tiempo);
		}
		if (prioridad == 3) {
			planificadorFIFO.crearProceso (tipoPerro, tiempo);
		}
	}

	public void notificacionProcesoTerminado(){
		despachadorGlobal.notificacionProcesoTerminado (CPU);
	}
		
	// Update is called once per frame
	void Update () {
		//procesos prioridad 1
		int nProcesosP1 = planificadorRR.listos.Count() + planificadorRR.bloqueados.Count() + planificadorRR.suspendidos.Count();
		if (planificadorRR.procesoEnEjecucion != null)
			nProcesosP1 += 1;
		//procesos prioridad 2
		int nProcesosP2 = planificadorSRTF.listos.Count() + planificadorSRTF.bloqueados.Count() + planificadorSRTF.suspendidos.Count();
		if (planificadorSRTF.procesoEnEjecucion != null)
			nProcesosP2 += 1;
		//procesos prioridad 3
		int nProcesosP3 = planificadorFIFO.listos.Count() + planificadorFIFO.bloqueados.Count() + planificadorFIFO.suspendidos.Count();
		if (planificadorFIFO.procesoEnEjecucion != null)
			nProcesosP3 += 1;
		//Debug.Log ("procesos p1: " + nProcesosP1);
		//Debug.Log ("procesos p2: " + nProcesosP2);
		//Debug.Log ("procesos p3: " + nProcesosP3);

		if (nProcesosP1 > 0) {
			//si hay un proceso de SRTF o de FIFO ejecutandose expulsarlo
			if(planificadorSRTF.procesoEnEjecucion != null){
				planificadorSRTF.suspendidos.Enqueue (planificadorSRTF.procesoEnEjecucion);
				planificadorSRTF.controladorPersonaje.procesadorToSuspendido (CPU,planificadorSRTF.procesoEnEjecucion.representacion);
				planificadorSRTF.procesoEnEjecucion.recurso.libre = true;
				planificadorSRTF.procesoEnEjecucion = null;
			}
			if(planificadorFIFO.procesoEnEjecucion != null){
				planificadorFIFO.suspendidos.Enqueue (planificadorFIFO.procesoEnEjecucion);
				planificadorFIFO.controladorPersonaje.procesadorToSuspendido (CPU,planificadorFIFO.procesoEnEjecucion.representacion);
				planificadorFIFO.procesoEnEjecucion.recurso.libre = true;
				planificadorFIFO.procesoEnEjecucion = null;
			}
			planificadorRR.planificar ();
		} else {
			if (nProcesosP2 > 0) {
				//si hay un proceso de FIFO ejecutandose expulsarlo
				if(planificadorFIFO.procesoEnEjecucion != null){
					planificadorFIFO.suspendidos.Enqueue (planificadorFIFO.procesoEnEjecucion);
					planificadorFIFO.controladorPersonaje.procesadorToSuspendido (CPU,planificadorFIFO.procesoEnEjecucion.representacion);
					planificadorFIFO.procesoEnEjecucion.recurso.libre = true;
					planificadorFIFO.procesoEnEjecucion = null;
				}
				planificadorSRTF.planificar ();
			} else {
				planificadorFIFO.planificar ();
			}
		}

		if (planificadorRR.procesoEnEjecucion != null) {
			procesoEnEjecucion = planificadorRR.procesoEnEjecucion;
			planificadorProcesoActual = "RR";
		} else {
			if (planificadorSRTF.procesoEnEjecucion != null) {
				procesoEnEjecucion = planificadorSRTF.procesoEnEjecucion;
				planificadorProcesoActual = "SRTF";
			} else {
				if (planificadorFIFO.procesoEnEjecucion != null) {
					procesoEnEjecucion = planificadorFIFO.procesoEnEjecucion;
					planificadorProcesoActual = "FIFO";
				} else {
					procesoEnEjecucion = null;
					planificadorProcesoActual = null;
				}
					
			}
		}

		float tiempoEnvejecimiento = 30;

		//envejecer los procesos de FIFO para pasar a SRTF
		//actualizar tiempo suspendido
		Cola<ProcesoFIFO> listosFIFO = new Cola<ProcesoFIFO>();
		while(planificadorFIFO.listos.Count()>0){
			ProcesoFIFO pr = planificadorFIFO.listos.Dequeue ();
			pr.envejecimiento += Time.deltaTime;
			if (pr.envejecimiento>5&&pr.envejecimiento<=10) {
				pr.representacion.GetComponent<Personaje>().animP1.SetInteger("estado", 1);
			}
			if (pr.envejecimiento > 10 && pr.envejecimiento < 15){           
				pr.representacion.GetComponent<Personaje>().animP1.SetInteger("estado", 2);
			}
			if (pr.envejecimiento > 15 && pr.envejecimiento < 20){
				pr.representacion.GetComponent<Personaje>().animP1.SetInteger("estado", 3);
			}
			if (pr.envejecimiento > 20 && pr.envejecimiento < 25){
				pr.representacion.GetComponent<Personaje>().animP1.SetInteger("estado", 4);
			}
			if (pr.envejecimiento >= tiempoEnvejecimiento) {
				//pasar a la cola de SRTF
				pr.envejecimiento = 0;
				ProcesoSRTF prSRTF = new ProcesoSRTF (planificadorSRTF, CPU, Instantiate (pr.representacion), pr.TTL);
				prSRTF.recurso = pr.recurso;
				planificadorSRTF.listos.Enqueue (prSRTF);
				Destroy (pr.representacion);
				Debug.Log ("paso de FIFO  a SRTF");
			} else {
				listosFIFO.Enqueue (pr);
			}

		}
		planificadorFIFO.listos = listosFIFO;

		//envejecer los procesos de SRTF para pasar a RR
		//actualizar tiempo suspendido
		Cola<ProcesoSRTF> listosSRTF = new Cola<ProcesoSRTF>();
		while(planificadorSRTF.listos.Count()>0){
			ProcesoSRTF pr = planificadorSRTF.listos.Dequeue ();
			pr.envejecimiento += Time.deltaTime;
			if (pr.envejecimiento>5&&pr.envejecimiento<=10) {
				pr.representacion.GetComponent<Personaje>().animP1.SetInteger("estado", 1);
			}
			if (pr.envejecimiento > 10 && pr.envejecimiento < 15){           
				pr.representacion.GetComponent<Personaje>().animP1.SetInteger("estado", 2);
			}
			if (pr.envejecimiento > 15 && pr.envejecimiento < 20){
				pr.representacion.GetComponent<Personaje>().animP1.SetInteger("estado", 3);
			}
			if (pr.envejecimiento > 20 && pr.envejecimiento < 25){
				pr.representacion.GetComponent<Personaje>().animP1.SetInteger("estado", 4);
			}
			if (pr.envejecimiento >= tiempoEnvejecimiento) {
				//pasar a la cola de SRTF
				pr.envejecimiento = 0;
				planificador.Proceso prRR = new planificador.Proceso (planificadorRR, CPU, Instantiate (pr.representacion), pr.TTL);
				prRR.recurso= pr.recurso;
				planificadorRR.listos.Enqueue (prRR);
				Destroy (pr.representacion);
				Debug.Log ("paso de SRTF  a RR");
			} else {
				listosSRTF.Enqueue (pr);
			}

		}
		planificadorSRTF.listos = listosSRTF;

	}


}

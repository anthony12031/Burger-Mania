using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaMDespachador : MonoBehaviour {

	public PlanificadorSRTF planificadorSRTF;
	public planificador planificadorRR;
	public PlanificadorFIFO planificadorFIFO;
	public int CPU;

	// Use this for initialization
	void Start () {
		//planificador SRTF
		planificadorSRTF = transform.GetChild (0).gameObject.GetComponent<PlanificadorSRTF>();
		planificadorSRTF.CPU = CPU;
		//planificador RR
		planificadorRR = transform.GetChild (1).gameObject.GetComponent<planificador>();
		planificadorRR.CPU = CPU;
		//planificador FIFO
		planificadorFIFO = transform.GetChild (2).gameObject.GetComponent<PlanificadorFIFO>();
		planificadorFIFO.CPU = CPU;
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
		Debug.Log ("procesos p1: " + nProcesosP1);
		Debug.Log ("procesos p2: " + nProcesosP2);
		Debug.Log ("procesos p3: " + nProcesosP3);

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

	}



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaMDespachador : MonoBehaviour {

	public PlanificadorSRTF planificadorSRTF;
	public planificador planificadorRR;
	public int CPU;

	// Use this for initialization
	void Start () {
		//planificador SRTF
		planificadorSRTF = transform.GetChild (0).gameObject.GetComponent<PlanificadorSRTF>();
		planificadorSRTF.CPU = CPU;
		//planificador RR
		planificadorRR = transform.GetChild (1).gameObject.GetComponent<planificador>();
		planificadorRR.CPU = CPU;
	}

	public void crearProceso(int tipoPerro,float tiempo,int prioridad){
		if (prioridad == 1) {
			planificadorRR.crearProceso (tipoPerro, tiempo);
		}
		if (prioridad == 2) {
			planificadorSRTF.crearProceso (tipoPerro, tiempo);
		}
	}

	// Update is called once per frame
	void Update () {
		planificadorSRTF.planificar ();
		int nProcesosP1 = planificadorRR.listos.Count() + planificadorRR.bloqueados.Count() + planificadorRR.suspendidos.Count();
		if (planificadorRR.procesoEnEjecucion != null)
			nProcesosP1 += 1;
		//Debug.Log ("procesos p1: " + nProcesosP1);
		if (nProcesosP1 > 0) {
			//planificadorRR.planificar ();
		}
	}



}

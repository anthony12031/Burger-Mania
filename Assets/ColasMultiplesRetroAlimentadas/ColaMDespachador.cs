using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaMDespachador : MonoBehaviour {

	public PlanificadorSRTF planificadorSRTF;
	public int CPU;

	// Use this for initialization
	void Start () {
		planificadorSRTF = transform.GetChild (0).gameObject.GetComponent<PlanificadorSRTF>();
		planificadorSRTF.CPU = CPU;
	}

	public void crearProceso(int tipoPerro,float tiempo,int prioridad){
		if (prioridad == 1) {
			planificadorSRTF.crearProceso (tipoPerro, tiempo);
		}
	}

	// Update is called once per frame
	void Update () {
		int nProcesosP1 = planificadorSRTF.listos.Count () + planificadorSRTF.bloqueados.Count () + planificadorSRTF.suspendidos.Count();
		if (planificadorSRTF.procesoEnEjecucion != null)
			nProcesosP1 += 1;
		//Debug.Log ("procesos p1: " + nProcesosP1);
		if (nProcesosP1 > 0) {
			planificadorSRTF.planificar ();
		}
	}



}

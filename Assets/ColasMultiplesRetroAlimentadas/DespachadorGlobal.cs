using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespachadorGlobal : MonoBehaviour {

	seleccionTipoPerro seleccionPerro;
	SeleccionCPU seleccionCPU;
	SeleccionPrioridad seleccionPrioridad;

	public float tiempoProceso = 0;

	// Use this for initialization
	void Start () {
		seleccionPerro = GetComponent<seleccionTipoPerro> ();
		seleccionCPU = GetComponent<SeleccionCPU> ();
		seleccionPrioridad = GetComponent<SeleccionPrioridad> ();
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	public void setTiempoProceso(string tiempo){
		try{
			tiempoProceso = System.Convert.ToSingle (tiempo);
			Debug.Log(tiempoProceso);
		}catch(System.FormatException){
			Debug.Log("format exception");
		}
	}


	public void crearOrden(){
		Debug.Log ("crear orden");
		int tipoPerro = seleccionPerro.getTipoPerro ()+1;
		int cpu = seleccionCPU.getCPU () + 1;
		int prioridad = seleccionPrioridad.getPrioridad ()+1;


		if (cpu == 1) {
			//planificadorCPU1.GetComponent<PlanificadorSRTF>().crearProceso (tipoPerro,tiempoProceso);
		}
		if (cpu == 2) {
			//planificadorCPU2.GetComponent<PlanificadorSRTF>().crearProceso (tipoPerro,tiempoProceso);
		}
		if (cpu == 3) {
			//planificadorCPU3.GetComponent<PlanificadorSRTF>().crearProceso (tipoPerro,tiempoProceso);
		}

	}


}

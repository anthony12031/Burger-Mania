using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AProceso  {
	public float TTL = 30;
	public int CPU;
	public float tiempoEnSuspendido ;//segundos
	public bool haFinalizado = false;
	planificador planificador;
	bool enEjecucion = false;

	public AProceso(planificador plan,int CPU){
		this.CPU = CPU;
		tiempoEnSuspendido = plan.tiempoSuspendido;
		planificador = plan;
		this.TTL = Random.Range(10,30);
	}

	abstract public void ejecutar (float tiempo);

	public void tiempoEnSuspendidoTick(float tiempo){
		if(tiempoEnSuspendido >0 )
			tiempoEnSuspendido -= tiempo;
	}

	public float getTiempoEnSuspendidoRestante(){
		return tiempoEnSuspendido;
	}
}

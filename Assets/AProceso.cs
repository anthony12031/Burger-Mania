using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public abstract class AProceso :MonoBehaviour {
	public float TTL = 30;
	public int CPU;
	public float tiempoEnSuspendido = 10 ;//segundos
	public bool haFinalizado = false;
	IPlanificador planificador;
	public volatile bool enEjecucion = false;

	public AutoResetEvent eventoDeEjecucion;
	public AutoResetEvent eventoDeSuspendido;
	public AutoResetEvent eventoDeBloqueado;


	public AProceso(IPlanificador plan,int CPU){
		this.CPU = CPU;
		planificador = plan;
		this.TTL = Random.Range(10,30);
		eventoDeEjecucion = new AutoResetEvent (false);
		eventoDeSuspendido  = new AutoResetEvent (false);
		eventoDeBloqueado  = new AutoResetEvent (false);
	}

	abstract public void ejecutar ();


	public void tiempoEnSuspendidoTick(float tiempo){
		if(tiempoEnSuspendido >0 )
			tiempoEnSuspendido -= tiempo;
	}

	public float getTiempoEnSuspendidoRestante(){
		return tiempoEnSuspendido;
	}
}

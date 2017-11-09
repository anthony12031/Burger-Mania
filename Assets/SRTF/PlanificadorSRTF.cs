﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PlanificadorSRTF : MonoBehaviour,IPlanificador {

	public delegate void eventoDeHiloDelegate(string tipoEvento);


	public Cola<ProcesoSRTF> listos;
	Cola<ProcesoSRTF> suspendidos;


	PersonajeController controladorPersonaje;
	public int CPU;

	public void eventoDeHilo(string evento){
	
	}

	// Use this for initialization
	void Start () {
		controladorPersonaje = GetComponent<PersonajeController> ();
		listos = new Cola<ProcesoSRTF> ();
		suspendidos = new Cola<ProcesoSRTF> ();
		controladorPersonaje.ColaClientes = listos;
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

	public void crearProceso(int tipoPerro){
		GameObject representacion = controladorPersonaje.agregarPersonaje (tipoPerro, CPU);
		GameObject TTL = null;
		foreach (Transform tr in representacion.transform) {
			if (tr.tag == "ttl") {
				TTL = tr.gameObject;
				break;
			}
		}
		ProcesoSRTF nuevoProceso = new ProcesoSRTF (this, CPU,TTL,representacion);
		listos.Enqueue (nuevoProceso);	
		Thread hiloProceso = new Thread (new ThreadStart (nuevoProceso.ejecutar));
		hiloProceso.Start ();
		nuevoProceso.hiloDeEjecucion = hiloProceso;

		//necesita tomate
		if (tipoPerro == 1) {
			nuevoProceso.recursos.Add (Recursos.lista ["salsaTomate"]);
		}
		//necesita mostaza
		if (tipoPerro == 2) {
			nuevoProceso.recursos.Add (Recursos.lista ["mostaza"]);
		}
	}

	void organizarListos(){
		
	}

	void ejecutarSiguienteProceso(){
		Debug.Log ("ejecutando sig proceso");
		ProcesoSRTF procesoAejecutar = listos.Dequeue ();
		//verificar que los recursos esten libres
		bool recursosLibres = true;
		foreach(Recursos.Recurso recurso in procesoAejecutar.recursos){
			if (!recurso.libre) {
				recursosLibres = false;
				break;
			}
		}
		//si los recursos estan libres ejecutar y bloquear los recursos que usa
		if (recursosLibres) {
			//procesoAejecutar.eventoDeEjecucion.Set ();
			procesoEnEjecucion = procesoAejecutar;
			listoTOprocesador (CPU);
		} 
		//sino pasarlo a bloqueado hasta que se libere el recurso
		else {
			Debug.Log("recurso en uso");
		}
	}

	void terminarProceso(){
		procesoEnEjecucion.enEjecucion = false;
		controladorPersonaje.terminarProcesoActual ();
		procesoEnEjecucion = null;
		//liberar recursos
	}

	ProcesoSRTF procesoEnEjecucion;

	// Update is called once per frame
	void Update () {
		//organizar cola de listos
		//listos = ordenarCola(listos);
		controladorPersonaje.updateVistaColas (CPU);
		//Debug.Log (procesoEnEjecucion);
		if (procesoEnEjecucion == null) {
			//si hay procesos ejecutarl el siguiente
			if (listos.Count()  > 0) {
				ejecutarSiguienteProceso ();
			}
		} 
		//hay un proceso en ejecucion comparar su TTL con el siguiente proceso
		else {
			procesoEnEjecucion.TTL -= Time.deltaTime;
			procesoEnEjecucion.textoTTL.text = System.Convert.ToString(procesoEnEjecucion.TTL);
			if (procesoEnEjecucion.TTL <= 0) {
				terminarProceso ();
			}
			//hay un proceso on menor tiempo
			//pasar a suspendido el actual y a ejecucion el proceso con menos tiempo
			if (listos.Count()>0 && procesoEnEjecucion != null) {
				if (procesoEnEjecucion.TTL > listos.Peek ().TTL) {
					suspendidos.Enqueue (procesoEnEjecucion);
					procesadorToSuspendido (CPU);
					procesoEnEjecucion = null;
				}
			}
		}
		//actualizar tiempo suspendido
		Cola<ProcesoSRTF> susTemp = new Cola<ProcesoSRTF>();
		while(suspendidos.Count()>0){
			ProcesoSRTF pr = suspendidos.Dequeue ();
			pr.tiempoEnSuspendido -= Time.deltaTime;
			if (pr.tiempoEnSuspendido <= 0) {
				listos.Enqueue (pr);
				controladorPersonaje.suspendidoTOlisto (CPU);
			} else {
				susTemp.Enqueue (pr);
			}

		}
		suspendidos = susTemp;


	}
		
	public void listoToSuspendido(){
		controladorPersonaje.listoToSuspendido (CPU);
	}

	public void listoToBloqueado(){
		controladorPersonaje.listoToBloqueado (CPU);
	}

	public void SuspendidoTolisto(){
		controladorPersonaje.suspendidoTOlisto (CPU);
	}

	public void BloqueadoTolisto(){
		controladorPersonaje.bloqueadoToListo ();
	}

	public void listoTOprocesador(int CPU){
		controladorPersonaje.listoToProcesador (CPU);
	}

	public void procesadorToSuspendido(int CPU){
		controladorPersonaje.procesadorToSuspendido (CPU);
	}

	public void procesadorToBloqueado(int CPU){
		controladorPersonaje.procesadorToBloqueado (CPU);
	}



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class planificador : MonoBehaviour {

	//orden analogo a proceso
	public GameObject Orden;
	//colas de los procesos
	Queue<Proceso>  listos;
	Queue<Proceso> bloqueados;
	Queue<Proceso> suspendidos;
	//el proceso que se esta ejecutando actualmente
	static Proceso  procesoEnEjecucion;

	seleccionTipoPerro seleccionPerro;
	public PersonajeController controladorPersonajes;
	public Text quantumTX;

	//controlador Panes
	public PanControlador panControlador;
	public Vector3 posAtendido;



	public class Proceso
	{
		public GameObject cliente;
		public GameObject clienteOriginal;
		public GameObject perroCaliente;
		float Quantum = 5; //segundos;
		public int tipoPerro;

		float tiempoEnSuspendido = 5;//segundos

		public bool haFinalizado = false;
		planificador planificador;
		bool enEjecucion = false;

		public Proceso(GameObject cliente,planificador plan,int perro){
			this.cliente = cliente;
			this.clienteOriginal = cliente;
			planificador = plan;
			tipoPerro = perro;
		}

		public void ejecutar(float tiempo){
			
				if (Quantum > 0)
					Quantum -= tiempo;
				else {
					Quantum = 0;
					planificador.notificacionQuantumTerminado ();
				}
		}

		public void tiempoEnSuspendidoTick(float tiempo){
			if(tiempoEnSuspendido >0 )
				tiempoEnSuspendido -= tiempo;
		}

		public float getTiempoEnSuspendidoRestante(){
			return tiempoEnSuspendido;
		}

		public float getTiempoRestante(){
			return Quantum;
		}

		public void setQuantum(float val){
			Quantum = val;
		}

	}

	//crear proceso
	public void crearOrden(){
		GameObject cliente =  controladorPersonajes.agregarPersonaje(seleccionPerro.getTipoPerro()+1,null);
		Proceso nuevoProceso = new Proceso (cliente,this,seleccionPerro.getTipoPerro()+1);
		listos.Enqueue (nuevoProceso);

	}

	public void crearOrden(int tipoPerro,GameObject personaje){
		GameObject cliente =  controladorPersonajes.agregarPersonaje(tipoPerro,personaje);
		Proceso nuevoProceso = new Proceso (cliente,this,tipoPerro);
		listos.Enqueue (nuevoProceso);

	}

	// Use this for initialization
	void Start () {
		seleccionPerro = GetComponent<seleccionTipoPerro> ();
		listos = new Queue<Proceso> ();
		bloqueados = new Queue<Proceso> ();
		suspendidos = new Queue<Proceso> ();
	}

	public static void notificacionProcesoTerminado(){
		Debug.Log ("Termino proceso");
		procesoEnEjecucion = null;
	}

	public  void notificacionQuantumTerminado(){
		Debug.Log ("quantum acabo");
		if (!procesoEnEjecucion.haFinalizado) {
			suspendidos.Enqueue (procesoEnEjecucion);
			//llamar metodo para encolar en suspendido el cliente
			procesoEnEjecucion.cliente.transform.position = new Vector2(-2,0);

		}
		procesoEnEjecucion = null;
	}


	void atender(){
		    procesoEnEjecucion = listos.Dequeue ();
			procesoEnEjecucion.cliente = Instantiate (procesoEnEjecucion.cliente, posAtendido, Quaternion.identity);
			procesoEnEjecucion.cliente.GetComponent<Personaje> ().esAnimado = false;
			procesoEnEjecucion.cliente.transform.localScale = new Vector3 (0.5f, 0.5f, 1);
			controladorPersonajes.atenderCliente ();

	}
	
	// Update is called once per frame
	void Update () {
		if (procesoEnEjecucion == null) {
			if (listos.Count > 0 ) {
				atender ();
			}
		} 
		else {
			quantumTX.text = System.Convert.ToString (procesoEnEjecucion.getTiempoRestante ());
			procesoEnEjecucion.ejecutar (Time.deltaTime);	
		}
			
			foreach (Proceso pr in suspendidos) {
				pr.tiempoEnSuspendidoTick (Time.deltaTime);
			}

		if (suspendidos.Count > 0) {
			if (suspendidos.Peek ().getTiempoEnSuspendidoRestante () <= 0) {
				Proceso pr = suspendidos.Dequeue ();
				pr.cliente.transform.localScale = new Vector3 (1, 1, 1);
				pr.cliente.GetComponent<Personaje> ().esAnimado = true;
				crearOrden (pr.tipoPerro, pr.cliente);
				Destroy (pr.cliente);
			}
		}

	}
}

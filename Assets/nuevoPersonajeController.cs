using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nuevoPersonajeController : MonoBehaviour {

	public Cola<ProcesoSRTF> listos;
	public Cola<ProcesoSRTF> suspendidos;
	public Cola<ProcesoSRTF> bloqueados;

	public GameObject TTL;
	public GameObject personajeBase;
	public GameObject nuevoPersonaje;
	public GameObject personaje1;
	public GameObject personaje2;
	public GameObject personaje3;
	public GameObject personajeA;
	public GameObject personajeB;
	public GameObject personajeC;
	public GameObject personajeD;
	public GameObject personajeE;

	public GameObject nuevoPerro;
	public GameObject perroBase;
	public GameObject perroT1;
	public GameObject perroConTomate;
	public GameObject perroConMostaza;
	public GameObject perroT4;

	public GameObject perroCrudo1;
	public GameObject perroCrudo2;
	public GameObject perroCrudo3;
	public GameObject perroCrudo4;

	public GameObject procesadorPR;
	public GameObject procesadorPJ;

	public  Queue<GameObject> ColaClientes;
	public  Queue<GameObject> ColaPerros;

	public  Queue<GameObject> ColaBloqueadoPJ;
	public  Queue<GameObject> ColaBloqueadoPR;

	public  Queue<GameObject> ColaSuspendidosPJ;
	public  Queue<GameObject> ColaSuspendidosPR;

	public Vector3 escalaEnFilaPJ;
	public Vector3 posListoCPU1;
	public Vector3 posListoCPU2;
	public Vector3 posListoCPU3;

	public Transform transPosListoCPU1;
	public Transform transPosListoCPU2;
	public Transform transPosListoCPU3;

	Vector2 posBloqueadoCPU1;
	Vector2 posBloqueadoCPU2;
	Vector2 posBloqueadoCPU3;

	public Transform transPosBloqueadoCPU1;
	public Transform transPosBloqueadoCPU2;
	public Transform transPosBloqueadoCPU3;

	Vector2 posSuspendidoCPU1;
	Vector2 posSuspendidoCPU2;
	Vector2 posSuspendidoCPU3;

	public Transform transPosSuspendidoCPU1;
	public Transform transPosSuspendidoCPU2;
	public Transform transPosSuspendidoCPU3;

	public float factorDivision = 0.1f;

	public float inicial = 0.4f;
	public float salto = 5f;
	public int estado = 0; // 0. ningun movimiento
	public int PJlista = 1;
	int sizeSUS = 0;
	int sizeBLO = 0;
	//1. moviendo a posicion x

	// Use this for initialization
	void Start () {

		listos = GetComponent<PlanificadorSRTF> ().listos;
		suspendidos = GetComponent<PlanificadorSRTF> ().suspendidos;
		bloqueados = GetComponent<PlanificadorSRTF> ().bloqueados;

		escalaEnFilaPJ = new Vector3(0.5F, 0.5F, 0.5F);
		posListoCPU1 =  transPosListoCPU1.position;
		posListoCPU2 =  transPosListoCPU2.position;
		posListoCPU3 =  transPosListoCPU3.position;

		posBloqueadoCPU1 = transPosBloqueadoCPU1.position;
		posBloqueadoCPU2 =  transPosBloqueadoCPU2.position;
		posBloqueadoCPU3 =  transPosBloqueadoCPU3.position;

		posSuspendidoCPU1 = transPosSuspendidoCPU1.position;
		posSuspendidoCPU2 = transPosSuspendidoCPU2.position;
		posSuspendidoCPU3 = transPosSuspendidoCPU3.position;

		ColaClientes = new Queue<GameObject>();
		ColaPerros = new Queue<GameObject>();

		ColaBloqueadoPJ = new Queue<GameObject>();
		ColaBloqueadoPR = new Queue<GameObject>();

		ColaSuspendidosPJ = new Queue<GameObject>();
		ColaSuspendidosPR = new Queue<GameObject>();

	}

	public GameObject agregarPersonaje(int orden,int cpu){

		if (PJlista > 8)
		{
			PJlista = 1;
		}

		switch (PJlista)
		{
		case 1:
			personajeBase = personajeA;
			break;
		case 2:
			personajeBase = personaje1;
			break;
		case 3:
			personajeBase = personajeB;
			break;
		case 4:
			personajeBase = personajeC;
			break;
		case 5:
			personajeBase = personaje2;
			break;
		case 6:
			personajeBase = personajeD;
			break;
		case 7:
			personajeBase = personajeE;
			break;
		case 8:
			personajeBase = personaje1;
			break;
		}
		PJlista++;
		switch (orden)
		{
		case 1:
			perroBase = perroConTomate;
			break;
		case 2:
			perroBase = perroConMostaza;
			break;

		}


		nuevoPersonaje = Instantiate(personajeBase, new Vector3(inicial + (6 * salto), 0.9f, 0), Quaternion.identity) as GameObject;
		nuevoPersonaje.GetComponent<Personaje>().posicion = -1;

		GameObject ttl = Instantiate (TTL, Vector2.zero, Quaternion.identity);
		ttl.transform.parent = nuevoPersonaje.transform;
		ttl.transform.localPosition = new Vector2 (0.8f, 0);

		nuevoPerro = Instantiate(perroBase, new Vector3(inicial + (6 * salto), 1f, 0), Quaternion.identity) as GameObject;
		nuevoPerro.GetComponent <Perros> ().posicion = -1;
		//  ColaPerros.Enqueue(nuevoPerro);

		nuevoPerro.transform.parent = nuevoPersonaje.transform;
		nuevoPerro.transform.localPosition = new Vector2(0,-1f);

		nuevoPersonaje.transform.localScale = escalaEnFilaPJ;
		nuevoPerro.transform.localScale = escalaEnFilaPJ*2;

		return nuevoPersonaje;
	}




	public void atenderCliente(){
		Destroy(ColaClientes.Dequeue());
		Destroy(ColaPerros.Dequeue());
	}


	public void listoToBloqueado(int cpu,GameObject cliente)
	{
			Vector2 pos = Vector2.zero;
			if (cpu == 1) {
				pos = posBloqueadoCPU1;

			}
			if (cpu == 2) {
				pos = posBloqueadoCPU2;
			}
			if (cpu == 3) {
				pos = posBloqueadoCPU3;
			}
			sizeBLO = GetComponent<PlanificadorSRTF>().bloqueados.Count();
			//GameObject cliente = ColaClientes.Dequeue ();
			if (cliente.transform.parent != null) {
				pos [0] = pos [0] - 0.6f;
				cliente.transform.parent.position = pos;
				cliente.transform.parent.localScale = new Vector3 (0.3f,0.3f,0.3f);
			} else {
				cliente.GetComponent<Personaje> ().inicial = pos[0];
				pos [0] = pos [0] - 0.6f;
				cliente.transform.position = pos;
				cliente.transform.localScale = new Vector3 (0.3f,0.3f,0.3f);
				cliente.GetComponent<Personaje> ().posicion = sizeBLO;
				cliente.GetComponent<Personaje> ().salto = 0.2f;
				//cliente.GetComponent<Personaje> ().inicial = -2.22f;
			}
	}


	public void listoToProcesador(int cpu,GameObject cliente)
	{
			Vector2 pos = Vector2.zero;
			if (cpu == 1)
				pos = SalchichaControlador.posParrilla1.v3Pos;
			if (cpu == 2)
				pos = SalchichaControlador.posParrilla2.v3Pos;
			if (cpu == 3)
				pos = SalchichaControlador.posParrilla3.v3Pos;
		
			if (cliente.transform.parent == null) {
				GameObject salchicha = perroCrudo1;
				GameObject nuevaSalchicha = Instantiate (salchicha, pos, Quaternion.identity) as GameObject;       
				cliente.transform.position = SalchichaControlador.posParrilla1.v3Pos;
				cliente.transform.parent = nuevaSalchicha.transform;
				cliente.transform.localPosition = new Vector2 (0.2f, 0);
				cliente.GetComponent<Personaje> ().posicion = -1;
			} else {
				cliente.transform.parent.position = pos;
			}
			foreach (Transform child in cliente.transform.parent) {
				if (child.gameObject.tag == "panPerro") {
					child.gameObject.tag = "panPerroEnProcesador";
					break;
				}
			}
	}




	public void listoToSuspendido(int CPU)
	{
		if(ColaClientes.Count>0){
			Vector2 pos = Vector2.zero;
			if (CPU == 1)
				pos = posSuspendidoCPU1;
			if (CPU == 2)
				pos = posSuspendidoCPU2;
			if (CPU == 3)
				pos = posSuspendidoCPU3;

			pos.y += (float)ColaSuspendidosPJ.Count / 100;

			sizeSUS = ColaSuspendidosPJ.Count;
			GameObject cliente = ColaClientes.Dequeue ();
			if (cliente.transform.parent != null) {
				pos [0] = pos [0] - 0.6f;
				cliente.transform.parent.position = pos;
				cliente.transform.parent.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
			} else {
				cliente.GetComponent<Personaje> ().inicial = pos [0];
				pos [0] = pos [0] - 0.6f;
				cliente.transform.position = pos;
				cliente.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
				cliente.GetComponent<Personaje> ().posicion = sizeSUS;
				cliente.GetComponent<Personaje> ().salto = 0.2f;
			}
			ColaSuspendidosPJ.Enqueue (cliente);   
		}
	}


	public void bloqueadoToListo()
	{
		GameObject sacado = ColaBloqueadoPJ.Dequeue ();
		sacado.transform.localScale = new Vector3 (0.6f,0.6f,0.6f);
		ColaClientes.Enqueue(sacado);

		int sizeBLOJ = ColaBloqueadoPJ.Count; 
		for(int i=0;i<sizeBLOJ;i++){
			GameObject sacar = ColaBloqueadoPJ.Dequeue();
			sacar.GetComponent<Personaje>().posicion = sacar.GetComponent<Personaje>().posicion - 1;
			ColaBloqueadoPJ.Enqueue (sacar);
		}

	}

	public void suspendidoTOlisto(int cpu,GameObject cliente)
	{
		
		cliente.transform.localScale = escalaEnFilaPJ;
		suspendidos = GetComponent<PlanificadorSRTF>().suspendidos; 
		int sizeSUSPJ = suspendidos.Count ();
		for(int i=0;i<sizeSUSPJ;i++){
			ProcesoSRTF pr = suspendidos.Dequeue ();
			GameObject sacar = pr.representacion;
			sacar.GetComponent<Personaje>().posicion = sacar.GetComponent<Personaje>().posicion - 1;
			suspendidos.Enqueue (pr);
		}
	}


	public void terminarProcesoActual(GameObject cliente){
		if (cliente.transform.parent)
			Destroy (cliente.transform.parent.gameObject);
		Destroy (cliente);
	}


	public void procesadorToListo(GameObject cliente)
	{     
		//GameObject cliente = procesadorPJ;

		//perroBase = procesadorPR;

		//nuevoPersonaje = Instantiate(personajeBase, new Vector3(inicial + (6 * salto), 0.9f, 0), Quaternion.identity) as GameObject;
		//nuevoPersonaje.GetComponent<Personaje>().posicion = ColaClientes.Count;
		//nuevoPersonaje.GetComponent<Transform>().localScale = new Vector3(0.64F, 0.64F, 0.64F);
		//ColaClientes.Enqueue(cliente);

		//Destroy(personajeBase);
		//Destroy(perroBase);

		//actualizarVista();
	}
	public void procesadorToBloqueado(int CPU)
	{

		if(procesadorPJ!=null){
			Vector2 pos = Vector2.zero;
			if (CPU == 1)
				pos = posBloqueadoCPU1;
			if (CPU == 2)
				pos = posBloqueadoCPU2;
			if (CPU == 3)
				pos = posBloqueadoCPU3;

			//pos.y += (float)ColaSuspendidosPJ.Count / 100;



			sizeBLO = ColaBloqueadoPJ.Count;
			GameObject cliente = procesadorPJ;

			cliente.GetComponent<Personaje> ().inicial = pos [0];
			pos [0] = pos [0] - 0.6f;
			cliente.transform.parent.position = pos;
			cliente.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);

			cliente.GetComponent<Personaje> ().posicion = sizeBLO;

			cliente.GetComponent<Personaje> ().salto = 0.2f;


			ColaBloqueadoPJ.Enqueue (cliente);
			procesadorPJ = null;
			foreach (Transform child in cliente.transform.parent) {
				if (child.gameObject.CompareTag ("panPerroEnProcesador")) {
					child.gameObject.GetComponent<PanPosicion> ().posicionEnParrilla.libre = true;
					child.gameObject.tag = "panPerro";
					break;
				}
			}

			Transform salchicha = cliente.transform.parent;
			cliente.transform.parent = null;
			Destroy (salchicha.gameObject);
			procesadorPR = null;
		}
	}

	public void procesadorToSuspendido(int CPU,GameObject cliente)
	{
			Vector2 pos = Vector2.zero;
			if (CPU == 1)
				pos = posSuspendidoCPU1;
			if (CPU == 2)
				pos = posSuspendidoCPU2;
			if (CPU == 3)
				pos = posSuspendidoCPU3;

			//pos.y += (float)ColaSuspendidosPJ.Count / 100;

			sizeSUS = ColaSuspendidosPJ.Count;
			//GameObject cliente = procesadorPJ;
			cliente.GetComponent<Personaje> ().inicial = pos [0];
			pos [0] = pos [0] - 0.6f;
			cliente.transform.parent.position = pos;
			cliente.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);

			cliente.GetComponent<Personaje> ().posicion = sizeSUS;

			cliente.GetComponent<Personaje> ().salto = 0.2f;

			foreach (Transform child in cliente.transform.parent) {
				if (child.gameObject.CompareTag ("panPerroEnProcesador")) {
					child.gameObject.GetComponent<PanPosicion> ().posicionEnParrilla.libre = true;
					child.gameObject.tag = "panPerro";
					break;
				}
			}

			Transform salchicha = cliente.transform.parent;
			cliente.transform.parent = null;
			Destroy (salchicha.gameObject);

	}

	public Vector2 getPosEnCola(Queue<GameObject> cola,int cpu){
		if (cpu == 1) {
			float posY = posListoCPU1.y + ((float)cola.Count)/factorDivision;
			return new Vector2 (posListoCPU1.x, posY);
		}
		return Vector2.zero;
	}

	public void updateVistaColas(int cpu){
		listos = GetComponent<PlanificadorSRTF>().listos;
		//Debug.Log (listos.Count ());
		/* colas de procesos listos */
		for (int i = 0; i < listos.Count(); i++) {
			ProcesoSRTF pr = listos.Dequeue ();
			GameObject cliente = pr.representacion;
			cliente.transform.localScale = escalaEnFilaPJ;
			float posX=0;
			float posY=0;
			if (cpu == 1) {
				posX = posListoCPU1.x;
				posY = posListoCPU1.y + ((float)(i+1))/factorDivision;
			}
			if (cpu == 2) {
				posX = posListoCPU2.x;
				posY = posListoCPU2.y + ((float)(i+1))/factorDivision;
			}

			if (cpu == 3) {
				posX = posListoCPU3.x;
				posY = posListoCPU3.y + ((float)(i+1))/factorDivision;
			}

			if(cliente.transform.parent == null)
				cliente.transform.position = new Vector2 (posX, posY);
			else
				cliente.transform.parent.position = new Vector2 (posX, posY);
			listos.Enqueue (pr);
		}
	}


	// Update is called once per frame
	void Update () {
		//updateVistaColas (CPU);
	}



}

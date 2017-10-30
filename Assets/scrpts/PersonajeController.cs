using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeController : MonoBehaviour {
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
	public GameObject perroT2;
	public GameObject perroT3;
	public GameObject perroT4;

	public GameObject perroCrudo1;
	public GameObject perroCrudo2;
	public GameObject perroCrudo3;
	public GameObject perroCrudo4;

	public GameObject procesadorPR;
	public GameObject procesadorPJ;

	public static Queue<GameObject> ColaClientes;
	public static Queue<GameObject> ColaPerros;

	public GameObject[] perrosHechos;
	public GameObject[] time;
	public GameObject[] reja;

	public GameObject rejao;
	public float aumento = 0.1f;

	public float inicial = -2.7f;
	public float salto = 5f;
	public int estado = 0; // 0. ningun movimiento
	public int PJlista = 1;
	public float tiempo = 0;
	public float tiempoAleatorio = 0;
	public float tiempoFinal = 40;
	public float tiempocompleto = 0;
	//1. moviendo a posicion x

	// Use this for initialization
	void Start () {
		tiempoAleatorio = 1;
		ColaClientes = new Queue<GameObject>();
		ColaPerros = new Queue<GameObject>();


		reja = GameObject.FindGameObjectsWithTag("reja");
		foreach (GameObject r in reja) {

			rejao = r;
		}



	}

	public GameObject agregarPersonaje(int orden,GameObject personaje){

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
			perroBase = perroT1;
			break;
		case 2:
			perroBase = perroT2;
			break;
		case 3:
			perroBase = perroT3;
			break;
		case 4:
			perroBase = perroT4;
			break;
		}

		if (personaje != null) {
			personajeBase = personaje;
		}


		nuevoPersonaje = Instantiate(personajeBase, new Vector3(inicial + (11 * salto), 0.9f, 0), Quaternion.identity) as GameObject;
		nuevoPersonaje.GetComponent<Personaje>().posicion = ColaClientes.Count;
		ColaClientes.Enqueue(nuevoPersonaje);

		nuevoPerro = Instantiate(perroBase, new Vector3(inicial + (11 * salto), 1.9f, 0), Quaternion.identity) as GameObject;
		nuevoPerro.GetComponent <Perros> ().posicion = ColaPerros.Count;
		ColaPerros.Enqueue(nuevoPerro);



		return nuevoPersonaje;


	}



	public void atenderCliente(int posicion){
		int i = 0;
		int size1 = ColaClientes.Count; 
		int size2 = ColaPerros.Count; 

		int contador = 0;
		for (i = 0; i < size1; i++)
		{
			
			GameObject sacar = ColaClientes.Dequeue();

			if (i == posicion) {
				sacar.GetComponent<Personaje>().posicion = -5;
			}
			if (i < posicion) {
				ColaClientes.Enqueue (sacar);
			}
			if (i > posicion) {
				sacar.GetComponent<Personaje>().posicion -= 1;
				ColaClientes.Enqueue (sacar);
			}
			
		}

		for (i = 0; i < size2; i++)
		{
			
			GameObject sacar = ColaPerros.Dequeue();

			if (i == posicion) {
				Destroy (sacar);
			}
			if (i < posicion) {
				ColaPerros.Enqueue (sacar);
			}
			if (i > posicion) {
				sacar.GetComponent<Perros>().posicion -= 1;
				ColaPerros.Enqueue (sacar);
			}
			
		}
	}

	int clienteMasCerca(float x, float y){

		int sizeCola = ColaClientes.Count;
		GameObject sacado;

		float AX = 0;
		float AY = 0;
		float BX = 0;
		float BY = 0;
		float CX = 0;
		float CY = 0;
		float DX = 0;
		float DY = 0;

		float masX = 0;
		float masY = 0;
		int solucion = -1;

		for (int i = 0; i < sizeCola; i++) {
			sacado = ColaClientes.Dequeue ();

			Sprite sp = sacado.GetComponent<SpriteRenderer> ().sprite;
			Vector3 array = sp.bounds.size;


			masX = array [0];
			masY = array [1];



			AX = sacado.transform.position.x - (array [0]/2);
			AY = sacado.transform.position.y + (array [1]/2);

			BX = AX + masX;
			BY = AY ;

			CX = AX;
			CY = BY - masY;

			DX = AX + masX;
			DY = BY - masY;

			//Debug.Log ("AX: " + AX + " AY: " + AY + " BX: " + BX + " BY: " + BY + " CX: " + CX + " CY: " + CY + " DX" + DX + " DY: " + DY);

			if ((AX <= x && AY >= y) && (BX >= x && BY >= y) && (CX <= x && CY <= y) && (DX >= x && DY <= y))
				solucion = i;
					
			ColaClientes.Enqueue(sacado);
		}	

		return solucion;

	}

	float obtenerPropina(int cliente, int salchicha, int tomate, int mostaza){
		//Debug.Log ("Salchicha" + salchicha);
		float porSalchichaCruda = 0f;
		float porSalchichaCocinada1 = 2f;
		float porSalchichaCocinada2 = 3f;
		float porSalchichaCocinada3 = 2f;
		float porSalchichaQuemada = 0f;
		float porBienSalsa = 3f;
		float propina = 0f;

		switch (salchicha) {
		case 0:
			propina = propina + porSalchichaCruda;
			break;
		case 1:
			propina = propina + porSalchichaCocinada1;
			break;
		case 2:
			propina = propina + porSalchichaCocinada2;

			break;
		case 3:
			propina = propina + porSalchichaCocinada3;
			break;
		case 4:
			propina = propina + porSalchichaQuemada;
			break;
		}

		int sizeP = ColaPerros.Count;


		for (int i = 0; i < sizeP; i++) {
			GameObject sacado = ColaPerros.Dequeue ();
			if (i == cliente) {
				//Debug.Log (sacado.name);
				if (sacado.name.Contains ("pedido_perro") && tomate == 0 && mostaza == 0)
					propina = propina + porBienSalsa;
				
				if (sacado.name.Contains ("pedido_perroTomate") && tomate == 1 && mostaza == 0)
					propina = propina + porBienSalsa;
				
				if (sacado.name.Contains ("pedido_perroMostaza") && tomate == 0 && mostaza == 1)
					propina = propina + porBienSalsa;
				
				if (sacado.name.Contains ("pedido_perroTomateMostaza") && tomate == 1 && mostaza == 1)
					propina = propina + porBienSalsa;
			}



			ColaPerros.Enqueue (sacado);
		}
		return propina;
	}

	GameObject obtenerPJ(int id){
		int size = ColaClientes.Count;
		GameObject sacado;
		GameObject resultado = null;

		for (int i = 0; i < size; i++) {
			sacado = ColaClientes.Dequeue ();
			if (i == id)
				resultado = sacado;
			ColaClientes.Enqueue (sacado);
		}

		return resultado;
	}
	// Update is called once per frame
	void Update () {
		
		tiempo  += Time.deltaTime;
		tiempocompleto += Time.deltaTime;
		if (tiempo>tiempoAleatorio && tiempocompleto<tiempoFinal) {
			tiempo = 0;
			tiempoAleatorio = Random.Range(4,7);
			agregarPersonaje (Random.Range(1,4),null);
		}
			
		int salchichap = 0;
		int tomate = 0;
		int mostaza = 0;

		perrosHechos = GameObject.FindGameObjectsWithTag("panPerro");
		foreach (GameObject go in perrosHechos) {
			if (go.GetComponent<DragPerroCaliente> ()) {
				if (go.GetComponent<DragPerroCaliente> ().isClicked == false) {
					//Debug.Log ("x" + go.transform.position.x + "y" + go.transform.position.y);
					int sol = clienteMasCerca (go.transform.position.x, go.transform.position.y);

					if (sol >= 0) {
						

						Transform[] ts = go.transform.GetComponentsInChildren <Transform> (true);
						foreach (Transform t in ts) {
							
							if (t.gameObject.name.Contains("salchicha")) {
								string salchicha = t.gameObject.GetComponent<SpriteRenderer>().sprite.name;
								switch (salchicha) {
								case "256":
									//Debug.Log ("Cruda");
									salchichap = 0;
									break;
								case "257":
									//Debug.Log ("Cocido1");
									salchichap = 1;
									break;
								case "258":
									//Debug.Log ("Cocido2");
									salchichap = 2;
									break;
								case "259":
									//Debug.Log ("Cocido3");
									salchichap = 3;
									break;
								case "260":
									//Debug.Log ("Quemada");
									salchichap = 4;
									break;
								}
							}
							if (t.gameObject.name.Contains ("salsaTomate")) {
								//Debug.Log ("HayTomate");
								tomate=1;

							}
							if (t.gameObject.name.Contains ("salsaMostaza")) {
								//Debug.Log ("HayMostaza");
								mostaza=1;
							}


						}

						//Debug.Log("Propina: " + obtenerPropina(sol,salchichap,tomate,mostaza));
						//obtenerPropina(sol,salchichap,tomate,mostaza);
						obtenerPJ (sol).GetComponent<Personaje> ().propina = obtenerPropina(sol,salchichap,tomate,mostaza);
						salchichap = 0;
						tomate = 0;
						mostaza = 0;

						obtenerPJ (sol).GetComponent<Personaje> ().hayPropina = true;

						atenderCliente (sol);
						go.GetComponent<PanPosicion> ().posicionEnParrilla.libre = true;
						Destroy (go);
					}
				}
			}

		}

		int sizec = ColaClientes.Count;
		int porsacar = 0;
		bool esSacar = false;
		for (int i = 0; i < sizec; i++) {
			GameObject sacado = ColaClientes.Dequeue();
			if (!sacado.GetComponent<Personaje> ().espera) {
				esSacar = true;
				porsacar = i;
			}
			ColaClientes.Enqueue (sacado);
		}

		if (esSacar == true) {
			atenderCliente (porsacar);
			esSacar = false;
		}


		time = GameObject.FindGameObjectsWithTag("time");
		foreach (GameObject t in time) {
			
			t.GetComponent<UnityEngine.UI.Text>().text = ((int)tiempocompleto).ToString();		
		
		}

		if (tiempocompleto > tiempoFinal) {
			Debug.Log ("Terminado");
			aumento = aumento + 0.001f;
			if(rejao.transform.position.y >= 0.3f)
				rejao.transform.position = new Vector3 (rejao.transform.position.x ,rejao.transform.position.y - aumento,rejao.transform.position.z);
		}


		if (Input.GetKeyDown("c"))
			agregarPersonaje(1,null);
		if (Input.GetKeyDown("0"))
			atenderCliente(0);
		if (Input.GetKeyDown("1"))
			atenderCliente(1);
		if (Input.GetKeyDown("2"))
			atenderCliente(2);
		if (Input.GetKeyDown("3"))
			atenderCliente(3);
		if (Input.GetKeyDown("4"))
			atenderCliente(4);
		if (Input.GetKeyDown("5"))
			atenderCliente(5);
		




	}

}

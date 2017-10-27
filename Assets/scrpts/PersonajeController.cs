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


	public float inicial = -2.7f;
	public float salto = 5f;
	public int estado = 0; // 0. ningun movimiento
	public int PJlista = 1;
	public float tiempo = 0;
	public float tiempoAleatorio = 0;
	//1. moviendo a posicion x

	// Use this for initialization
	void Start () {
		tiempoAleatorio = Random.Range(5,10);
		ColaClientes = new Queue<GameObject>();
		ColaPerros = new Queue<GameObject>();

		//for (int i = 0; i < 10; i++)
		//{
		//    Instantiate(personaje1, new Vector3(inicial, 0.9f, 0), Quaternion.identity);
		//    inicial += 0.6f; 

		//}




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
		//Destroy(ColaClientes.Dequeue());
		//Destroy(ColaPerros.Dequeue());
		//actualizarVista();
	

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

	// Update is called once per frame
	void Update () {
		
		tiempo  += Time.deltaTime;

		Debug.Log("Tiempo:" + tiempo + "Tiempo Aleatorio:" + tiempoAleatorio);
		if (tiempo>tiempoAleatorio) {
			tiempo = 0;
			tiempoAleatorio = Random.Range(5,7);
			agregarPersonaje (Random.Range(1,4),null);
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

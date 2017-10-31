using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recursos : MonoBehaviour {

	public static Dictionary<string,Recurso> lista;
	public static Dictionary<planificador.Proceso,Recurso> recursosEnUso;

	public class Recurso{

		public string nombre;
		public bool libre;

		public Recurso(string nombre){
			this.nombre = nombre;
			libre =true;
		}


	}

	// Use this for initialization
	void Start () {
		Debug.Log ("creando recursos");
		lista = new Dictionary<string,Recurso> ();
		recursosEnUso = new Dictionary<planificador.Proceso,Recurso> ();
		Recurso salsaTomate = new Recurso ("salsaTomate");
		Recurso mostaza = new Recurso ("mostaza");
		lista.Add (salsaTomate.nombre, salsaTomate);
		lista.Add (mostaza.nombre, mostaza);
	}


	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recursos : MonoBehaviour {

	public static Dictionary<string,Recurso> lista;

	public class Recurso{

		public string nombre;
		public string estado;

		public Recurso(string nombre){
			this.nombre = nombre;
			estado = "libre";
		}

		public string getEstado(){
			return estado;
		}

		public void setEstado(string nuevoEstado){
			estado = nuevoEstado;
		}

	}

	// Use this for initialization
	void Start () {
		Debug.Log ("creando recursos");
		lista = new Dictionary<string,Recurso> ();
		Recurso salsaTomate = new Recurso ("salsaTomate");
		Recurso mostaza = new Recurso ("mostaza");
		lista.Add (salsaTomate.nombre, salsaTomate);
		lista.Add (mostaza.nombre, mostaza);
	}

	public static string getEstadoRecurso(string nombre){
		return lista [nombre].getEstado ();
	} 
	
	// Update is called once per frame
	void Update () {
		
	}
}

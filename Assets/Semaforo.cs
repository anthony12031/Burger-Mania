using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semaforo : MonoBehaviour {

	public GameObject rojo;
	public GameObject verde;
	public Recursos.Recurso recurso;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("semaforo de recurso: " + recurso.nombre);
		if (recurso.libre) {
			verde.SetActive (true);
			rojo.SetActive (false);
		}
		if (!recurso.libre) {
			verde.SetActive (false);
			rojo.SetActive (true);
		}
	}
}

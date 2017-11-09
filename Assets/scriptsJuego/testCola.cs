using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCola : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Cola<int> cola = new Cola<int> ();
		Debug.Log ("Cuenta?" + cola.Count());
		cola.Enqueue (1);
		cola.Enqueue (2);
		cola.Enqueue (3);
		Debug.Log ("Cuenta?" + cola.Count());
		Debug.Log (cola.Peek ());
		Debug.Log ("Cuenta?" + cola.Count());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

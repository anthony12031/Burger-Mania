using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fin : MonoBehaviour {
	GameObject[] reja;
	GameObject[] pjcontrolador;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnMouseDown(){
		this.gameObject.SetActive (false);
		reja = GameObject.FindGameObjectsWithTag("reja");
		foreach (GameObject r in reja) {
			r.transform.position = new Vector3 (r.transform.position.x,4.78f,r.transform.position.z);
		}

		pjcontrolador = GameObject.FindGameObjectsWithTag("pjcontrolador");
		foreach (GameObject pj in pjcontrolador) {
			pj.GetComponent<PersonajeController> ().reiniciarM ();
		}

	}

}

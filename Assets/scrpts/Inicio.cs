﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicio : MonoBehaviour {
	GameObject[] menu;
	GameObject[] pjcontrolador;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnMouseDown(){
		menu = GameObject.FindGameObjectsWithTag("menu");
		foreach (GameObject m in menu) {
			m.SetActive (false);
		}

		pjcontrolador = GameObject.FindGameObjectsWithTag("pjcontrolador");
		foreach (GameObject pj in pjcontrolador) {
			pj.GetComponent<PersonajeController> ().iniciar ();
		}
	}
}

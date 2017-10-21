using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cocinado : MonoBehaviour {

	public float tiempoCocinado;
	public bool estaEnParrilla = true;
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (estaEnParrilla) {
			tiempoCocinado += Time.deltaTime;
			animator.SetInteger ("tiempo", (int)tiempoCocinado);
		}

	}
}

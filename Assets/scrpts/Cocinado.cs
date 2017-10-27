using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cocinado : MonoBehaviour {

	public float tiempoCocinado;	
	public bool estaEnParrilla = true;
	private Animator animator;
	public AudioClip sonidoListo;

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

		//Debug.Log (GetComponent<AudioSource> ().isPlaying);
		if (!GetComponent<AudioSource> ().isPlaying) {
			animator.SetBool ("cocinado", true);
		}

	}

	bool eventoCocidoLlamado = false;

	public void eventoCocido(){
		if(!eventoCocidoLlamado)
			GetComponent<AudioSource> ().PlayOneShot (sonidoListo);
		eventoCocidoLlamado = true;
	}

}

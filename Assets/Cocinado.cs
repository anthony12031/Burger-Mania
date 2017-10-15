using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cocinado : MonoBehaviour {

	public float tiempoCocinado;
	private float tiempoActualCocinado=0;
	private string estado = "1";
	public Sprite salchicha2;
	public Sprite salchicha3;
	public Sprite salchicha4;
	public Sprite salchicha5;
	public bool estaEnParrilla = true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(estaEnParrilla)
			tiempoActualCocinado += Time.deltaTime; 

		if (tiempoActualCocinado >= tiempoCocinado && estado == "1") {
			GetComponent<SpriteRenderer>().sprite = salchicha2 ;
			estado = "2";
		}
		if (tiempoActualCocinado >= tiempoCocinado*2 && estado == "2") {
			GetComponent<SpriteRenderer>().sprite = salchicha3 ;
			estado = "3";
		}
		if (tiempoActualCocinado >= tiempoCocinado*3 && estado == "3") {
			GetComponent<SpriteRenderer>().sprite = salchicha4 ;
			estado = "4";
		}
		if (tiempoActualCocinado >= tiempoCocinado*4 && estado == "4") {
			GetComponent<SpriteRenderer>().sprite = salchicha5 ;
			estado = "5";
		}

		//Debug.Log (tiempoCocinado);
	}
}

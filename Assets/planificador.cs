using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//importar esta libreria para hacer uso de componente UI
using UnityEngine.UI;

public class planificador : MonoBehaviour {

	// colas de los procesos del sistema
	static Queue listos;
	static Queue suspendidos;
	static Queue bloqueados;

	public Text tiempoRestanteTexto;
	float tiempoRestante;
	float Quantum = 20;


	// Use this for initialization
	void Start () {
		tiempoRestante = Quantum;
	}
	
	// Update is called once per frame
	void Update () {
		if (tiempoRestante > 0) {
			tiempoRestante -= Time.deltaTime;
			tiempoRestanteTexto.text = System.Convert.ToString (tiempoRestante);
			Debug.Log (tiempoRestante);
		}
		if (tiempoRestante < 0) {
			tiempoRestanteTexto.text = "0";
		}
				
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour {
	public float montoMoneda = 0;
	GameObject[] propina;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		string actual;
		float nuevo;
		propina = GameObject.FindGameObjectsWithTag("monto");
		foreach (GameObject p in propina) {
			actual = p.GetComponent<UnityEngine.UI.Text>().text;
			nuevo = float.Parse (actual);
			nuevo = nuevo + montoMoneda;
			p.GetComponent<UnityEngine.UI.Text>().text = "$" + nuevo.ToString();		
			Destroy (this.gameObject);
		}
	}
}

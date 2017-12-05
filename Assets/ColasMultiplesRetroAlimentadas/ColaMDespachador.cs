using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaMDespachador : MonoBehaviour {

	public PlanificadorSRTF planificadorSRTF;
	public int CPU;

	// Use this for initialization
	void Start () {
		planificadorSRTF = transform.GetChild (0).gameObject.GetComponent<PlanificadorSRTF>();
		planificadorSRTF.CPU = CPU;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

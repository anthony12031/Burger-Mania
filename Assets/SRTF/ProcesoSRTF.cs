﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class ProcesoSRTF : AProceso {

	public List<Recursos.Recurso> recursos;
	public TextMesh textoTTL;
	public Thread hiloDeEjecucion;
	public GameObject representacion;

	public ProcesoSRTF(IPlanificador plan,int CPU,GameObject rep):base(plan,CPU){
		recursos = new List<Recursos.Recurso> ();
		textoTTL = rep.transform.GetChild (0).GetChild(0).GetComponent<TextMesh> ();
		textoTTL.text = System.Convert.ToString(TTL);
		enEjecucion = true;
		representacion = rep;
	}


	public override void ejecutar ()
	{
		while (enEjecucion) {
		//	Debug.Log ("antes de ejecutar");
			eventoDeEjecucion.WaitOne ();
				Thread.Sleep (1000);
				Debug.Log ("ejecutando");
			}


	}

}

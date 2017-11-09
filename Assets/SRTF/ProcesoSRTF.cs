using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcesoSRTF : AProceso {

	public List<Recursos.Recurso> recursos;


	public ProcesoSRTF(IPlanificador plan,int CPU):base(plan,CPU){
		recursos = new List<Recursos.Recurso> ();
	}


	public override void ejecutar ()
	{
		while (true) {
			Debug.Log ("antes de ejecutar");
			eventoDeEjecucion.WaitOne ();
			enEjecucion = true;
			while (enEjecucion) {
				Debug.Log ("ejecutando");
			}
		}

	}

}

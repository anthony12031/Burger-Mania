using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

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
				Thread.Sleep (1000);
				Debug.Log ("ejecutando");
			}
		}

	}

}

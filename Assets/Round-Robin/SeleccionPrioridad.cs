using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeleccionPrioridad : MonoBehaviour {
	public Dropdown prioridad; 

	public int getPrioridad(){
		return prioridad.value;
	}

	void Start() {
		prioridad.onValueChanged.AddListener(delegate {
			myDropdownValueChangedHandler(prioridad);
		});
	}

	void Destroy() {
		prioridad.onValueChanged.RemoveAllListeners();
	}

	private void myDropdownValueChangedHandler(Dropdown target) {
		//Debug.Log("selected: "+target.value);
	}

	public void SetDropdownIndex(int index) {
		prioridad.value = index;
	}
}

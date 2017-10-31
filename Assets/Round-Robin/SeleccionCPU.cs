using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeleccionCPU : MonoBehaviour {

	public Dropdown dpCPU; 

	public int getCPU(){
		return dpCPU.value;
	}

	void Start() {
		dpCPU.onValueChanged.AddListener(delegate {
			myDropdownValueChangedHandler(dpCPU);
		});
	}

	void Destroy() {
		dpCPU.onValueChanged.RemoveAllListeners();
	}

	private void myDropdownValueChangedHandler(Dropdown target) {
			//Debug.Log("selected: "+target.value);
	}

	public void SetDropdownIndex(int index) {
		dpCPU.value = index;
	}
}

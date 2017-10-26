using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class seleccionTipoPerro : MonoBehaviour {

	public Dropdown dpTipoPerro; 

	public int getTipoPerro(){
		return dpTipoPerro.value;
	}

	void Start() {
		dpTipoPerro.onValueChanged.AddListener(delegate {
			myDropdownValueChangedHandler(dpTipoPerro);
		});
	}

	void Destroy() {
		dpTipoPerro.onValueChanged.RemoveAllListeners();
	}

	private void myDropdownValueChangedHandler(Dropdown target) {
		Debug.Log("selected: "+target.value);
	}

	public void SetDropdownIndex(int index) {
		dpTipoPerro.value = index;
	}
}

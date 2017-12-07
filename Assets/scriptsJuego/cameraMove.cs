using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour {
	GameObject camara;
    GameObject[] botones;
    GameObject[] puntos;
    // Use this for initialization
    float cambio = 1f;
    float pcambio = 1f;
	void Start () {
		camara= GameObject.FindGameObjectWithTag ("MainCamera");
        botones = GameObject.FindGameObjectsWithTag("botonCamara");
        

    }
    public void moverArriba() {
        if((camara.transform.position.y + cambio) < 0.5f) { 
        camara.transform.Translate(0, cambio, 0);
        foreach (GameObject boton in botones) {
            boton.transform.Translate(0, cambio, 0);
        }
        }

    }
    public void moverAbajo(){
        camara.transform.Translate(0, -cambio, 0);
        foreach (GameObject boton in botones)
        {
            boton.transform.Translate(0, -cambio, 0);
        }
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            moverArriba();
		if (Input.GetKeyDown (KeyCode.DownArrow))
            moverAbajo();

    }
}

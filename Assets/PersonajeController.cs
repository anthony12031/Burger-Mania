using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeController : MonoBehaviour {
    public GameObject personaje1;
    public static Queue ColaClientes;
    public float inicial = -2.7f;
    public int estado = 0; // 0. ningun movimiento
    //1. moviendo a posicion x
    
	// Use this for initialization
	void Start () {
        ColaClientes = new Queue();
        for (int i = 0; i < 10; i++)
        {
            Instantiate(personaje1, new Vector3(inicial, 0.9f, 0), Quaternion.identity);
            inicial += 0.6f; 
            
        }
    }

    
	
	// Update is called once per frame
	void Update () {
	    	
	}

}

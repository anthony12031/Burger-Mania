using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeController : MonoBehaviour {
    public GameObject personajeBase;
    public GameObject nuevoPersonaje;
    public GameObject personaje1;
    public static Queue<GameObject> ColaClientes;
    public float inicial = -2.7f;
    public float salto = 0.6f;
    public int estado = 0; // 0. ningun movimiento
    //1. moviendo a posicion x
    
	// Use this for initialization
	void Start () {
        ColaClientes = new Queue<GameObject>();
        //for (int i = 0; i < 10; i++)
        //{
        //    Instantiate(personaje1, new Vector3(inicial, 0.9f, 0), Quaternion.identity);
        //    inicial += 0.6f; 
            
        //}

        agregarPersonaje();
        agregarPersonaje();
        atenderCliente();

    }

    void agregarPersonaje(){
        personajeBase = personaje1;

        nuevoPersonaje = Instantiate(personajeBase, new Vector3(inicial + (11 * salto), 0.9f, 0), Quaternion.identity) as GameObject;
        nuevoPersonaje.GetComponent<Personaje>().posicion = 0;
        ColaClientes.Enqueue(nuevoPersonaje);
        
    }

    void atenderCliente(){
        //Destroy(ColaClientes.Dequeue());
        GameObject sacar = ColaClientes.Dequeue();
        sacar.GetComponent<Personaje>().moverA(3);
        //sacar.transform.position = new Vector3(0,0,0);
    }

    void actualizarVista(){
       //s ColaClientes.
    }
	
	// Update is called once per frame
	void Update () {
	    	
	}

}

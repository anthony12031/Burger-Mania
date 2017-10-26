using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeController : MonoBehaviour {
    public GameObject personajeBase;
    public GameObject nuevoPersonaje;
    public GameObject personaje1;
    public GameObject personaje2;
    public GameObject personaje3;
    public GameObject personajeA;
    public GameObject personajeB;
    public GameObject personajeC;
    public GameObject personajeD;
    public GameObject personajeE;

    public GameObject nuevoPerro;
    public GameObject perroBase;
    public GameObject perroT1;
    public GameObject perroT2;
    public GameObject perroT3;
    public GameObject perroT4;

    public static Queue<GameObject> ColaClientes;
    public static Queue<GameObject> ColaPerros;

    public static Queue<GameObject> ColaBloqueadoPJ;
    public static Queue<GameObject> ColaBloqueadoPR;

    public static Queue<GameObject> ColaSuspendidosPJ;
    public static Queue<GameObject> ColaSuspendidosPR;

    public float inicial = -2.7f;
    public float salto = 5f;
    public int estado = 0; // 0. ningun movimiento
    public int PJlista = 1;
    //1. moviendo a posicion x
    
	// Use this for initialization
	void Start () {
        ColaClientes = new Queue<GameObject>();
        ColaPerros = new Queue<GameObject>();

        ColaBloqueadoPJ = new Queue<GameObject>();
        ColaBloqueadoPR = new Queue<GameObject>();

        ColaSuspendidosPJ = new Queue<GameObject>();
        ColaSuspendidosPR = new Queue<GameObject>();
        //for (int i = 0; i < 10; i++)
        //{
        //    Instantiate(personaje1, new Vector3(inicial, 0.9f, 0), Quaternion.identity);
        //    inicial += 0.6f; 

        //}





    }
		
	public GameObject agregarPersonaje(int orden){

        if (PJlista > 8)
        {
            PJlista = 1;
        }
        
        switch (PJlista)
        {
            case 1:
                personajeBase = personajeA;
            break;
            case 2:
                personajeBase = personaje1;
            break;
            case 3:
                personajeBase = personajeB;
                break;
            case 4:
                personajeBase = personajeC;
                break;
            case 5:
                personajeBase = personaje2;
                break;
            case 6:
                personajeBase = personajeD;
                break;
            case 7:
                personajeBase = personajeE;
                break;
            case 8:
                personajeBase = personaje1;
                break;
        }
        PJlista++;
        switch (orden)
        {
            case 1:
                perroBase = perroT1;
                break;
            case 2:
                perroBase = perroT2;
                break;
            case 3:
                perroBase = perroT3;
                break;
            case 4:
                perroBase = perroT4;
                break;
        }
       


        nuevoPersonaje = Instantiate(personajeBase, new Vector3(inicial + (6 * salto), 0.9f, 0), Quaternion.identity) as GameObject;
        nuevoPersonaje.GetComponent<Personaje>().posicion = ColaClientes.Count;
        ColaClientes.Enqueue(nuevoPersonaje);

        nuevoPerro = Instantiate(perroBase, new Vector3(inicial + (6 * salto), 1.9f, 0), Quaternion.identity) as GameObject;
        nuevoPerro.GetComponent <Perros> ().posicion = ColaPerros.Count;
        ColaPerros.Enqueue(nuevoPerro);



        return nuevoPersonaje;
        

    }

    public void atenderCliente(){
        Destroy(ColaClientes.Dequeue());
        Destroy(ColaPerros.Dequeue());
        actualizarVista();
        //GameObject sacar = ColaClientes.Dequeue();
        //sacar.GetComponent<Personaje>().moverA(3);
        //sacar.transform.position = new Vector3(0,0,0);
    }

    void actualizarVista(){
        //s ColaClientes
        int i = 0;
       for (i = 0; i < ColaClientes.Count; i++)
        {
            GameObject sacar = ColaClientes.Dequeue();
            sacar.GetComponent<Personaje>().posicion -= 1;
            ColaClientes.Enqueue(sacar);
        }
        for (i = 0; i < ColaPerros.Count; i++)
        {
            GameObject sacar = ColaPerros.Dequeue();
            sacar.GetComponent<Perros>().posicion -= 1;
            ColaPerros.Enqueue(sacar);
        }
        Debug.Log(i);
    }
	
    public void listoTObloqueado()
    {
        GameObject sacar = ColaPerros.Dequeue();
        ColaBloqueadoPR.Enqueue(sacar);

        sacar = ColaClientes.Dequeue();
        ColaBloqueadoPJ.Enqueue(sacar);
        actualizarVista();
    }

    public void listoTOsuspendido()
    {
        GameObject sacar = ColaPerros.Dequeue();
        ColaSuspendidosPR.Enqueue(sacar);

        sacar = ColaClientes.Dequeue();
        ColaSuspendidosPJ.Enqueue(sacar);
        actualizarVista();
    }


    public void bloqueadoTOlisto()
    {
        personajeBase = ColaBloqueadoPJ.Dequeue();
        perroBase = ColaBloqueadoPR.Dequeue();

        nuevoPersonaje = Instantiate(personajeBase, new Vector3(inicial + (6 * salto), 0.9f, 0), Quaternion.identity) as GameObject;
        nuevoPersonaje.GetComponent<Personaje>().posicion = ColaClientes.Count;
        ColaClientes.Enqueue(nuevoPersonaje);

        nuevoPerro = Instantiate(perroBase, new Vector3(inicial + (6 * salto), 1.9f, 0), Quaternion.identity) as GameObject;
        nuevoPerro.GetComponent<Perros>().posicion = ColaPerros.Count;
        ColaPerros.Enqueue(nuevoPerro);
    }

    public void suspendidoTOlisto()
    {
        personajeBase = ColaSuspendidosPJ.Dequeue();
        perroBase = ColaSuspendidosPR.Dequeue();

        nuevoPersonaje = Instantiate(personajeBase, new Vector3(inicial + (6 * salto), 0.9f, 0), Quaternion.identity) as GameObject;
        nuevoPersonaje.GetComponent<Personaje>().posicion = ColaClientes.Count;
        ColaClientes.Enqueue(nuevoPersonaje);

        nuevoPerro = Instantiate(perroBase, new Vector3(inicial + (6 * salto), 1.9f, 0), Quaternion.identity) as GameObject;
        nuevoPerro.GetComponent<Perros>().posicion = ColaPerros.Count;
        ColaPerros.Enqueue(nuevoPerro);

        //actualizarVista();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("c"))
            agregarPersonaje(1);
        if (Input.GetKeyDown("r"))
            atenderCliente();
        if (Input.GetKeyDown("l"))
            listoTObloqueado();
        if (Input.GetKeyDown("s"))
            bloqueadoTOlisto();

    }

}

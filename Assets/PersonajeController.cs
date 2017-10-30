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

    public GameObject perroCrudo1;
    public GameObject perroCrudo2;
    public GameObject perroCrudo3;
    public GameObject perroCrudo4;

    public GameObject procesadorPR;
    public GameObject procesadorPJ;

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
		Debug.Log ("tipo orden: " + orden);

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
    }

    public void listoTObloqueado()
    {
        GameObject sacar = ColaPerros.Dequeue();
        if (sacar.name.Contains("pedido_perro"))
        {
            Destroy(sacar);
            sacar = perroCrudo1;
            sacar.name = "salchicha1";
        }

        if (sacar.name.Contains("pedido_perroTomate"))
        {
            Destroy(sacar);
            sacar = perroCrudo2;
            sacar.name = "salchicha2";
        }
        if (sacar.name.Contains("pedido_perroMostaza"))
        {
            Destroy(sacar);
            sacar = perroCrudo3;
            sacar.name = "salchicha3";
        }
        if (sacar.name.Contains("pedido_perroTomateMostaza"))
        {
            Destroy(sacar);
            sacar = perroCrudo4;
            sacar.name = "salchicha4";
        }

        Debug.Log(sacar.name);

        float pjx = 0;
        float pjy = 0;
        float pjz = 0;
        float prx = 0;
        float pry = 0;
        float prz = 0;
     
                
        prx = -1.68f;
        pry = -0.28f;
        prz = 0;

        pjx = prx + 0.14f;
        pjy = pry - 0.05f;
        pjz = prz;

        nuevoPerro = Instantiate(sacar, new Vector3(prx, pry, prz), Quaternion.identity) as GameObject;
        //nuevoPerro.GetComponent<Perros>().posicion = ColaBloqueadoPR;
        ColaBloqueadoPR.Enqueue(nuevoPerro);




        //ColaBloqueadoPR.Enqueue(sacar);

        sacar = ColaClientes.Dequeue();

        sacar.transform.position = new Vector3(pjx, pjy, pjz);
        sacar.GetComponent<Personaje>().posicion = -1;
        sacar.GetComponent<Transform>().localScale = new Vector3(0.3F, 0.3F, 0.3F);
        //sacar.transform.localScale
        ColaBloqueadoPJ.Enqueue(sacar);
        actualizarVista();
    }

    public void listoTOprocesador()
    {
        GameObject sacar = ColaPerros.Dequeue();
        if (sacar.name.Contains("pedido_perro"))
        {
            Destroy(sacar);
            sacar = perroCrudo1;
            sacar.name = "salchicha1";
        }
        if (sacar.name.Contains("pedido_perroTomate"))
        {
            Destroy(sacar);
            sacar = perroCrudo2;
            sacar.name = "salchicha2";
        }
        if (sacar.name.Contains("pedido_perroMostaza"))
        {
            Destroy(sacar);
            sacar = perroCrudo3;
            sacar.name = "salchicha3";
        }
        if (sacar.name.Contains("pedido_perroTomateMostaza"))
        {
            Destroy(sacar);
            sacar = perroCrudo4;
            sacar.name = "salchicha4";
        }

        float pjx = 0;
        float pjy = 0;
        float pjz = 0;
        float prx = 0;
        float pry = 0;
        float prz = 0;
        

                prx = 1.91f;
                pry = -0.35f;
                prz = 0;
       
        pjx = prx + 0.14f;
        pjy = pry - 0.05f;
        pjz = prz;

        nuevoPerro = Instantiate(sacar, new Vector3(prx, pry, prz), Quaternion.identity) as GameObject;
        //nuevoPerro.GetComponent<Perros>().posicion = ColaBloqueadoPR;

        
        procesadorPR = nuevoPerro;
           
        




        //ColaBloqueadoPR.Enqueue(sacar);

        sacar = ColaClientes.Dequeue();

        sacar.transform.position = new Vector3(pjx, pjy, pjz);
        sacar.GetComponent<Personaje>().posicion = -1;
        sacar.GetComponent<Transform>().localScale = new Vector3(0.3F, 0.3F, 0.3F);
        //sacar.transform.localScale

        
         procesadorPJ = sacar;
        

        actualizarVista();
    }




public void listoTOsuspendido()
    {
        
        ////////////////////
        GameObject sacar = ColaPerros.Dequeue();
        if (sacar.name.Contains("pedido_perro"))
        {
            Destroy(sacar);
            sacar = perroCrudo1;
            sacar.name = "salchicha1";
        }
        if (sacar.name.Contains("pedido_perroTomate"))
        {
            Destroy(sacar);
            sacar = perroCrudo2;
            sacar.name = "salchicha2";
        }
        if (sacar.name.Contains("pedido_perroMostaza"))
        {
            Destroy(sacar);
            sacar = perroCrudo3;
            sacar.name = "salchicha3";
        }
        if (sacar.name.Contains("pedido_perroTomateMostaza"))
        {
            Destroy(sacar);
            sacar = perroCrudo4;
            sacar.name = "salchicha4";
        }

        float pjx = 0;
        float pjy = 0;
        float pjz = 0;
        float prx = 0;
        float pry = 0;
        float prz = 0;


        prx = -2.7f;
        pry = -0.4f;
        prz = 0;

        pjx = prx + 0.14f;
        pjy = pry - 0.05f;
        pjz = prz;

        nuevoPerro = Instantiate(sacar, new Vector3(prx, pry, prz), Quaternion.identity) as GameObject;
        //nuevoPerro.GetComponent<Perros>().posicion = ColaBloqueadoPR;
        ColaSuspendidosPR.Enqueue(nuevoPerro);




        //ColaBloqueadoPR.Enqueue(sacar);

        sacar = ColaClientes.Dequeue();

        sacar.transform.position = new Vector3(pjx, pjy, pjz);
        sacar.GetComponent<Personaje>().posicion = -1;
        sacar.GetComponent<Transform>().localScale = new Vector3(0.3F, 0.3F, 0.3F);
        //sacar.transform.localScale
        ColaSuspendidosPJ.Enqueue(sacar);
        actualizarVista();
    }


    public void bloqueadoTOlisto()
    {
        personajeBase = ColaBloqueadoPJ.Dequeue();
        perroBase = ColaBloqueadoPR.Dequeue();

        nuevoPersonaje = Instantiate(personajeBase, new Vector3(inicial + (6 * salto), 0.9f, 0), Quaternion.identity) as GameObject;
        nuevoPersonaje.GetComponent<Personaje>().posicion = ColaClientes.Count;        
        nuevoPersonaje.GetComponent<Transform>().localScale = new Vector3(0.64F, 0.64F, 0.64F);
        ColaClientes.Enqueue(nuevoPersonaje);

        Debug.Log(perroBase.name);
        Destroy(personajeBase);
        Destroy(perroBase);
        if (perroBase.name.Contains("salchicha1"))
        {         
            perroBase = perroT1;
        }
        if (perroBase.name.Contains("salchicha2"))
        {
            perroBase = perroT2;
        }
        if (perroBase.name.Contains("salchicha3"))
        {
            perroBase = perroT3;
        }
        if (perroBase.name.Contains("salchicha4"))
        {
            perroBase = perroT4;
        }

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
        nuevoPersonaje.GetComponent<Transform>().localScale = new Vector3(0.64F, 0.64F, 0.64F);
        ColaClientes.Enqueue(nuevoPersonaje);

        Destroy(personajeBase);
        Destroy(perroBase);
        if (perroBase.name.Contains("salchicha1"))
        {
           perroBase = perroT1;
        }
        if (perroBase.name.Contains("salchicha2"))
        {
            perroBase = perroT2;
        }
        if (perroBase.name.Contains("salchicha3"))
        {
            perroBase = perroT3;
        }
        if (perroBase.name.Contains("salchicha4"))
        {
            perroBase = perroT4;
        }

        nuevoPerro = Instantiate(perroBase, new Vector3(inicial + (6 * salto), 1.9f, 0), Quaternion.identity) as GameObject;
        nuevoPerro.GetComponent<Perros>().posicion = ColaPerros.Count;
        ColaPerros.Enqueue(nuevoPerro);

        //actualizarVista();
    }

    public void procesadorTOlisto()
    {

        
                personajeBase = procesadorPJ;
                perroBase = procesadorPR;
             
        

        nuevoPersonaje = Instantiate(personajeBase, new Vector3(inicial + (6 * salto), 0.9f, 0), Quaternion.identity) as GameObject;
        nuevoPersonaje.GetComponent<Personaje>().posicion = ColaClientes.Count;
        nuevoPersonaje.GetComponent<Transform>().localScale = new Vector3(0.64F, 0.64F, 0.64F);
        ColaClientes.Enqueue(nuevoPersonaje);

        Destroy(personajeBase);
        Destroy(perroBase);

        if (perroBase.name.Contains("salchicha1"))
        {
            
            perroBase = perroT1;
        }
        if (perroBase.name.Contains("salchicha2"))
        {
            perroBase = perroT2;
        }
        if (perroBase.name.Contains("salchicha3"))
        {
            perroBase = perroT3;
        }
        if (perroBase.name.Contains("salchicha4"))
        {
            perroBase = perroT4;
        }

        nuevoPerro = Instantiate(perroBase, new Vector3(inicial + (6 * salto), 1.9f, 0), Quaternion.identity) as GameObject;
        nuevoPerro.GetComponent<Perros>().posicion = ColaPerros.Count;
        ColaPerros.Enqueue(nuevoPerro);

        //actualizarVista();
    }
	public void procesadorTObloqueado()
	{


		personajeBase = procesadorPJ;
		perroBase = procesadorPR;
		//Destroy (procesadorPJ);
		Destroy (procesadorPR);

		float pjx = 0;
		float pjy = 0;
		float pjz = 0;
		float prx = 0;
		float pry = 0;
		float prz = 0;


		prx = -1.68f;
		pry = -0.28f;
		prz = 0;

		pjx = prx + 0.14f;
		pjy = pry - 0.05f;
		pjz = prz;

		nuevoPerro = Instantiate(perroBase, new Vector3(prx, pry, prz), Quaternion.identity) as GameObject;
		//nuevoPerro.GetComponent<Perros>().posicion = ColaBloqueadoPR;
		ColaBloqueadoPR.Enqueue(nuevoPerro);




		procesadorPJ.transform.position = new Vector3(pjx, pjy, pjz);
		procesadorPJ.GetComponent<Personaje>().posicion = -1;
		procesadorPJ.GetComponent<Transform>().localScale = new Vector3(0.3F, 0.3F, 0.3F);
		//sacar.transform.localScale
		ColaBloqueadoPJ.Enqueue(procesadorPJ);
		actualizarVista();
		procesadorPJ = null;
		procesadorPR = null;

	}


	public void procesadorTOsuspendido()
	{


		personajeBase = procesadorPJ;
		perroBase = procesadorPR;
		//Destroy (procesadorPJ);
		Destroy (procesadorPR);

		float pjx = 0;
		float pjy = 0;
		float pjz = 0;
		float prx = 0;
		float pry = 0;
		float prz = 0;


		prx = -2.7f;
		pry = -0.4f;
		prz = 0;

		pjx = prx + 0.14f;
		pjy = pry - 0.05f;
		pjz = prz;

		nuevoPerro = Instantiate(perroBase, new Vector3(prx, pry, prz), Quaternion.identity) as GameObject;
		//nuevoPerro.GetComponent<Perros>().posicion = ColaBloqueadoPR;
		ColaSuspendidosPR.Enqueue(nuevoPerro);




		procesadorPJ.transform.position = new Vector3(pjx, pjy, pjz);
		procesadorPJ.GetComponent<Personaje>().posicion = -1;
		procesadorPJ.GetComponent<Transform>().localScale = new Vector3(0.3F, 0.3F, 0.3F);
		//sacar.transform.localScale
		ColaSuspendidosPJ.Enqueue(procesadorPJ);
		actualizarVista();

		procesadorPJ = null;
		procesadorPR = null;

	}

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("c"))
            agregarPersonaje(1);
        if (Input.GetKeyDown("r"))
            atenderCliente();
        if (Input.GetKeyDown("b"))
            listoTObloqueado();
        if (Input.GetKeyDown("l"))
            bloqueadoTOlisto();
        if (Input.GetKeyDown("s"))
            listoTOsuspendido();
        if (Input.GetKeyDown("k"))
            suspendidoTOlisto();


        if (Input.GetKeyDown("1"))
            listoTOprocesador();
       
        if (Input.GetKeyDown("2"))
            procesadorTOlisto();

		if (Input.GetKeyDown("o"))
			procesadorTOsuspendido();
		
		if (Input.GetKeyDown("p"))
			procesadorTObloqueado();
        


    }

}

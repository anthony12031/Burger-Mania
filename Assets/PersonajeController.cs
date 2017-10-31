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

    public  Queue<GameObject> ColaClientes;
    public  Queue<GameObject> ColaPerros;

    public  Queue<GameObject> ColaBloqueadoPJ;
    public  Queue<GameObject> ColaBloqueadoPR;

    public  Queue<GameObject> ColaSuspendidosPJ;
    public  Queue<GameObject> ColaSuspendidosPR;

	public Vector3 escalaEnFilaPJ;
	public Vector3 posListoCPU1;
	public Vector3 posListoCPU2;
	public Vector3 posListoCPU3;

	public float factorDivision = 3;

    public float inicial = -2.7f;
    public float salto = 5f;
    public int estado = 0; // 0. ningun movimiento
    public int PJlista = 1;
    //1. moviendo a posicion x
    
	// Use this for initialization
	void Start () {

		escalaEnFilaPJ = new Vector3(0.5F, 0.5F, 0.5F);
		posListoCPU1 =  new Vector3(-0.76F, 0.8f, 0.3F);
		posListoCPU2 =  new Vector3(0.3F, 0.8f, 0.3F);
		posListoCPU3 =  new Vector3(1.3F, 0.8f, 0.3F);

        ColaClientes = new Queue<GameObject>();
        ColaPerros = new Queue<GameObject>();

        ColaBloqueadoPJ = new Queue<GameObject>();
        ColaBloqueadoPR = new Queue<GameObject>();

        ColaSuspendidosPJ = new Queue<GameObject>();
        ColaSuspendidosPR = new Queue<GameObject>();


    }
		
	public GameObject agregarPersonaje(int orden,int cpu){
		
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
        nuevoPersonaje.GetComponent<Personaje>().posicion = -1;
        ColaClientes.Enqueue(nuevoPersonaje);

        nuevoPerro = Instantiate(perroBase, new Vector3(inicial + (6 * salto), 1.9f, 0), Quaternion.identity) as GameObject;
        nuevoPerro.GetComponent <Perros> ().posicion = -1;
      //  ColaPerros.Enqueue(nuevoPerro);

		nuevoPerro.transform.parent = nuevoPersonaje.transform;
		nuevoPerro.transform.localPosition = new Vector2(0,-0.8f);

		nuevoPersonaje.transform.localScale = escalaEnFilaPJ;
		nuevoPerro.transform.localScale = escalaEnFilaPJ*2;

        return nuevoPersonaje;
    }
		



    public void atenderCliente(){
        Destroy(ColaClientes.Dequeue());
        Destroy(ColaPerros.Dequeue());
    }


	public void listoToBloqueado(int cpu)
    {
	    Vector2 posBloqueadoCPU1 = new Vector2(-1.6f,0);
		Vector2 pos = Vector2.zero;
		if (cpu == 1) {
			pos = posBloqueadoCPU1;
		}
		GameObject cliente = ColaClientes.Dequeue ();

		if (cliente.transform.parent != null) {
			cliente.transform.parent.position = pos;
		} else {
			cliente.transform.position = pos;
		}
		ColaBloqueadoPJ.Enqueue(cliente);   
    }


	public void listoToProcesador(int cpu)
    {
		Vector2 pos = Vector2.zero;
		if (cpu == 1)
			pos = SalchichaControlador.posParrilla1.v3Pos;
		if (cpu == 2)
			pos = SalchichaControlador.posParrilla2.v3Pos;
		if (cpu == 3)
			pos = SalchichaControlador.posParrilla3.v3Pos;
		GameObject cliente = ColaClientes.Dequeue ();
		if (cliente.transform.parent == null) {
			GameObject salchicha = perroCrudo1;
			GameObject nuevaSalchicha = Instantiate (salchicha, pos, Quaternion.identity) as GameObject;       
			cliente.transform.position = SalchichaControlador.posParrilla1.v3Pos;
			cliente.transform.parent = nuevaSalchicha.transform;
			cliente.transform.localPosition = new Vector2 (0.2f, 0);
			procesadorPJ = cliente;
		} else {
			cliente.transform.parent.position = pos;
			procesadorPJ = cliente;
		}
		foreach (Transform child in cliente.transform.parent) {
			if (child.gameObject.tag == "panPerro") {
				child.gameObject.tag = "panPerroEnProcesador";
				break;
			}
		}
	
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
      //  actualizarVista();
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

	public void suspendidoTOlisto(int cpu)
    {
		ColaClientes.Enqueue(ColaSuspendidosPJ.Dequeue());   
    }


	public void terminarProcesoActual(){
		if (procesadorPJ.transform.parent)
			Destroy (procesadorPJ.transform.parent.gameObject);
		Destroy (procesadorPJ);
		procesadorPJ = null;
	}


    public void procesadorToListo()
    {     
		GameObject cliente = procesadorPJ;
		Debug.Log (procesadorPJ);
        //perroBase = procesadorPR;

        //nuevoPersonaje = Instantiate(personajeBase, new Vector3(inicial + (6 * salto), 0.9f, 0), Quaternion.identity) as GameObject;
        //nuevoPersonaje.GetComponent<Personaje>().posicion = ColaClientes.Count;
        //nuevoPersonaje.GetComponent<Transform>().localScale = new Vector3(0.64F, 0.64F, 0.64F);
		ColaClientes.Enqueue(cliente);

        //Destroy(personajeBase);
        //Destroy(perroBase);

        //actualizarVista();
    }
	public void procesadorToBloqueado()
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

		personajeBase.transform.position = new Vector3(pjx, pjy, pjz);
		//sacar.transform.localScale
		ColaBloqueadoPJ.Enqueue(personajeBase);
		procesadorPJ = null;
		procesadorPR = null;
	}

	public void procesadorToSuspendido(int CPU)
	{
		Vector2 pos = Vector2.zero;
		if(CPU == 1 )
			pos =  new Vector2(-2.8f, -0.3f);
		if(CPU == 2 )
			pos =  new Vector2(-2.8f, -1.0f);
		if(CPU == 3 )
			pos =  new Vector2(-2.8f, -1.6f);

		pos.y += (float)ColaSuspendidosPJ.Count / 100;

		GameObject cliente = procesadorPJ;
		cliente.transform.parent.position = pos;
		ColaSuspendidosPJ.Enqueue(cliente);
		procesadorPJ = null;
		foreach (Transform child in cliente.transform.parent) {
			if (child.gameObject.CompareTag ("panPerroEnProcesador")) {
				child.gameObject.GetComponent<PanPosicion> ().posicionEnParrilla.libre = true;
				child.gameObject.tag = "panPerro";
				break;
			}
		}
		//procesadorPR = null;
	}

	public Vector2 getPosEnCola(Queue<GameObject> cola,int cpu){
		if (cpu == 1) {
			float posY = posListoCPU1.y + ((float)cola.Count)/factorDivision;
			return new Vector2 (posListoCPU1.x, posY);
		}
		return Vector2.zero;
	}

	void updateVistaColas(int cpu){
		/* colas de procesos listos */
		for (int i = 0; i < ColaClientes.Count; i++) {
			GameObject cliente = ColaClientes.Dequeue ();
			float posX=0;
			float posY=0;
			if (cpu == 1) {
				posX = posListoCPU1.x;
				posY = posListoCPU1.y + ((float)(i+1))/factorDivision;
			}
			if (cpu == 2) {
				posX = posListoCPU2.x;
				posY = posListoCPU2.y + ((float)(i+1))/factorDivision;
			}

			if (cpu == 3) {
				posX = posListoCPU3.x;
				posY = posListoCPU3.y + ((float)(i+1))/factorDivision;
			}

			if(cliente.transform.parent == null)
				cliente.transform.position = new Vector2 (posX, posY);
			else
				cliente.transform.parent.position = new Vector2 (posX, posY);
			ColaClientes.Enqueue (cliente);
		}

	}
		
	public int CPU;

    // Update is called once per frame
    void Update () {
	  updateVistaColas (CPU);
    }

}

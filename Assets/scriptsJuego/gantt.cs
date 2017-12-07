using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gantt : MonoBehaviour {
	Vector2 posInicial;
	public Transform posInicialt;
	public Transform cpu1;
	public Transform cpu2;
	public Transform cpu3;
	Vector2 posIni1;
	Vector2 posIni2;
	Vector2 posIni3;

	public GameObject[] clientes;
	public GameObject[] content;
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

	public GameObject listo;
	public GameObject proceso;
	public GameObject bloqueado;
	public GameObject suspendido;


	public GameObject punto;

	public static List<GameObject> objetosCreados1;
	public static List<GameObject> objetosCreados2;
	public  static List<GameObject> objetosCreados3;
	public int contadorObj = 0;
	public float distancia = 0.6f;
	public float inicioSubGantt = 0.5f;
    public float inicioSubGanttinx = 0.5f;
    public float inicioChartx = 0.3f;
    public float inicioCharty = 0.3f;

    public float deltaFrames = 0f;
	public float contadorFrames = 0.01f;
    public bool inicio = false;
	int contador1 = 0;
	int contador2 = 0;
	int contador3 = 0;
    int contadorHistoria1 = 0;
    int contadorHistoria2 = 0;
    int contadorHistoria3 = 0;
    int contadorMuertos1 = 0;
    int contadorMuertos2 = 0;
    int contadorMuertos3 = 0;

    int sumaCPU1 = 0;
    int sumaCPU2 = 0;
    int sumaCPU3 = 0;

    // Use this for initialization
    void Start () {
		content = GameObject.FindGameObjectsWithTag ("contentGantt");
		posInicial = posInicialt.position;
		posIni1 = cpu1.position;
		posIni2 = cpu2.position;
		posIni3 = cpu3.position;

        objetosCreados1 = new List<GameObject>();
        objetosCreados2 = new List<GameObject>();
        objetosCreados3 = new List<GameObject>();



    }
	
	// Update is called once per frame
	public void agregarPersonaje(int cpu, int pj, int id){
		Debug.Log ("CPU: "+cpu);
		Debug.Log ("pj: "+pj);
		Debug.Log ("id: "+id);
		switch (pj)
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
		nuevoPersonaje = Instantiate(personajeBase, posInicial, Quaternion.identity) as GameObject;
		nuevoPersonaje.GetComponent<Personaje> ().posicion = -1;
		nuevoPersonaje.GetComponent<Personaje> ().cpu = cpu;
		nuevoPersonaje.GetComponent<Personaje> ().id = id;
		nuevoPersonaje.tag = "salsaTomate";
		nuevoPersonaje.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
		nuevoPersonaje.transform.parent = content [0].transform;
		switch (cpu) {
		case 1:
			//nuevoPersonaje.transform.position = new Vector3 (nuevoPersonaje.transform.position.x, nuevoPersonaje.transform.position.y - (objetosCreados1.Count * distancia), nuevoPersonaje.transform.position.z);
			objetosCreados1.Add(nuevoPersonaje);
			break;
		case 2:
			objetosCreados2.Add(nuevoPersonaje);
			break;
		case 3:
			objetosCreados3.Add(nuevoPersonaje);
			break;
		}
		actualizarVista ();
	}

	void actualizarVista(){
		
		cpu1.transform.position = posIni1;
		cpu2.transform.position = new Vector2(posIni2[0],posIni2[1] - (distancia*objetosCreados1.Count));
		cpu3.transform.position = new Vector2(posIni3[0],posIni3[1] - (distancia*objetosCreados1.Count) - (distancia*objetosCreados2.Count));

		int contador = 0;
		foreach (GameObject cpuG1 in objetosCreados1) {
			
			cpuG1.transform.position = new Vector2 (inicioSubGanttinx, cpu1.transform.position[1]-(distancia*contador) - inicioSubGantt);
			contador++;
		}

		contador = 0;
		foreach (GameObject cpuG2 in objetosCreados2) {
			
			cpuG2.transform.position = new Vector2 (inicioSubGanttinx, cpu2.transform.position[1]-(distancia*contador) - inicioSubGantt);
			contador++;
		}

		contador = 0;
		foreach (GameObject cpuG3 in objetosCreados3) {
			
			cpuG3.transform.position = new Vector2 (inicioSubGanttinx, cpu3.transform.position[1]-(distancia*contador) - inicioSubGantt);
			contador++;
		}
	}

   

	float tiempotranscurrido=0;
    void Update() {
        if (inicio) { 
            tiempotranscurrido += Time.deltaTime;

        if (tiempotranscurrido >= 0.1) {
            deltaFrames = deltaFrames + contadorFrames;
            tiempotranscurrido = 0;
            cpu1.transform.position = posIni1;
            cpu2.transform.position = new Vector2(posIni2[0], posIni2[1] - (distancia * objetosCreados1.Count));
            cpu3.transform.position = new Vector2(posIni3[0], posIni3[1] - (distancia * objetosCreados1.Count) - (distancia * objetosCreados2.Count));





            clientes = GameObject.FindGameObjectsWithTag("cliente");


            contador1 = 0;
            contador2 = 0;
            contador3 = 0;
            foreach (GameObject cliente in clientes) {
                //Debug.Log("/////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
                foreach (GameObject cpuG1 in objetosCreados1) {
                    //Debug.Log("CPU cliente" + cliente.GetComponent<Personaje>().cpu + " = CPU gantt " + cpuG1.GetComponent<Personaje>().cpu);
                    //Debug.Log("ID cliente" + cliente.GetComponent<Personaje>().id + " = ID gantt " + cpuG1.GetComponent<Personaje>().id);

                    if ( cliente.GetComponent<Personaje>().id == cpuG1.GetComponent<Personaje>().id) {
                        switch (cliente.GetComponent<Personaje>().estado) {
                            case 0:
                                punto = Instantiate(listo, new Vector2(cpuG1.transform.position.x + deltaFrames + inicioChartx, cpu1.transform.position[1] - (distancia * contador1) - inicioCharty), Quaternion.identity, cpuG1.transform) as GameObject;
                                break;
                            case 1:
                                punto = Instantiate(proceso, new Vector2(cpuG1.transform.position.x + deltaFrames + inicioChartx, cpu1.transform.position[1] - (distancia * contador1) - inicioCharty), Quaternion.identity, cpuG1.transform) as GameObject;
                                break;
                            case 2:
                                punto = Instantiate(bloqueado, new Vector2(cpuG1.transform.position.x + deltaFrames + inicioChartx, cpu1.transform.position[1] - (distancia * contador1) - inicioCharty), Quaternion.identity, cpuG1.transform) as GameObject;
                                break;
                            case 3:
                                punto = Instantiate(suspendido, new Vector2(cpuG1.transform.position.x + deltaFrames + inicioChartx, cpu1.transform.position[1] - (distancia * contador1) - inicioCharty), Quaternion.identity, cpuG1.transform) as GameObject;
                                break;

                        }

                        punto.transform.parent = cpuG1.transform;
                        contador1++;
                        //Debug.Log(contador1);
                    }
                }

                foreach (GameObject cpuG2 in objetosCreados2) {
                    if ( cliente.GetComponent<Personaje>().id == cpuG2.GetComponent<Personaje>().id) {
                        switch (cliente.GetComponent<Personaje>().estado) {
                            case 0:
                                punto = Instantiate(listo, new Vector2(cpuG2.transform.position.x + deltaFrames + inicioChartx, cpu2.transform.position[1] - (distancia * contador2) - inicioCharty), Quaternion.identity, cpuG2.transform) as GameObject;
                                break;
                            case 1:
                                punto = Instantiate(proceso, new Vector2(cpuG2.transform.position.x + deltaFrames + inicioChartx, cpu2.transform.position[1] - (distancia * contador2) - inicioCharty), Quaternion.identity, cpuG2.transform) as GameObject;
                                break;
                            case 2:
                                punto = Instantiate(bloqueado, new Vector2(cpuG2.transform.position.x + deltaFrames + inicioChartx, cpu2.transform.position[1] - (distancia * contador2) - inicioCharty), Quaternion.identity, cpuG2.transform) as GameObject;
                                break;
                            case 3:
                                punto = Instantiate(suspendido, new Vector2(cpuG2.transform.position.x + deltaFrames + inicioChartx, cpu2.transform.position[1] - (distancia * contador2) - inicioCharty), Quaternion.identity, cpuG2.transform) as GameObject;
                                break;

                        }


                        punto.transform.parent = cpuG2.transform;
                        contador2++;

                    }
                }


                foreach (GameObject cpuG3 in objetosCreados3) {
                    if ( cliente.GetComponent<Personaje>().id == cpuG3.GetComponent<Personaje>().id) {
                        switch (cliente.GetComponent<Personaje>().estado) {
                            case 0:
                                punto = Instantiate(listo, new Vector2(cpuG3.transform.position.x + deltaFrames + inicioChartx, cpu3.transform.position[1] - (distancia * contador3) - inicioCharty), Quaternion.identity, cpuG3.transform) as GameObject;
                                break;
                            case 1:
                                punto = Instantiate(proceso, new Vector2(cpuG3.transform.position.x + deltaFrames + inicioChartx, cpu3.transform.position[1] - (distancia * contador3) - inicioCharty), Quaternion.identity, cpuG3.transform) as GameObject;
                                break;
                            case 2:
                                punto = Instantiate(bloqueado, new Vector2(cpuG3.transform.position.x + deltaFrames + inicioChartx, cpu3.transform.position[1] - (distancia * contador3) - inicioCharty), Quaternion.identity, cpuG3.transform) as GameObject;
                                break;
                            case 3:
                                punto = Instantiate(suspendido, new Vector2(cpuG3.transform.position.x + deltaFrames + inicioChartx, cpu3.transform.position[1] - (distancia * contador3) - inicioCharty), Quaternion.identity, cpuG3.transform) as GameObject;
                                break;

                        }


                        punto.transform.parent = cpuG3.transform;
                        contador3++;

                    }
                }


            }

        }
    }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour {
	public Animator animP1;
	public Sprite personaje1;
	public float contador = 0;
    public float inicial = -2.7f;
    public float salto = 5f;
	public bool espera = true;
	public bool hayPropina = false;
	public GameObject moneda;
	public GameObject newMoneda;	

    // public int estadoMov = 0;
    //public int movex = 0;
    public int posicion = 11;
	public float rnum ;
	public float propina;
	// Use this for initialization
	void Start () {

		rnum = Random.Range (-3.64f,-2.52f);
//		Debug.Log ("RNUM" + rnum);
    }
	// Update is called once per frame
	void Update () {
		
        contador += Time.deltaTime;

        if (contador>5&&contador<=10) {
            animP1.SetInteger("estado", 1);
        }
        if (contador > 10 && contador < 15){           
            animP1.SetInteger("estado", 2);
        }
        if (contador > 15 && contador < 20){
            animP1.SetInteger("estado", 3);
        }
        if (contador > 20 && contador < 25){
            animP1.SetInteger("estado", 4);
        }


        if (System.Math.Abs((inicial + (posicion * salto)) - transform.position.x)>0.1){
            if((inicial + (posicion * salto)) < transform.position.x){
                
                transform.position = new Vector3(transform.position.x - 0.02f, transform.position.y, transform.position.z);
            }else{
                transform.position = new Vector3(transform.position.x + 0.02f, transform.position.y, transform.position.z);
            }
        }

		if (contador > 30)
			espera = false;

		if (hayPropina && propina!=0) {		
			//Debug.Log ("Aleatorio es" + rnum);
			if (transform.position.x < rnum) {
				hayPropina = false;
				//Debug.Log ("PonePropina");
				newMoneda = Instantiate(moneda, new Vector3(rnum, 0.32f, 0), Quaternion.identity) as GameObject;
				newMoneda.GetComponent<Moneda> ().montoMoneda = propina;
			}

		}
        
        
    }

	 

	    //public void moveto(int posicion)
	    //{
	    //    movex = posicion;
	    //    estadoMov = 1;
	    //}

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour {
	public Animator animP1;
	public Sprite personaje1;
	public float contador = 0;
    public float inicial = -2.7f;
    public float salto = 5f;
    // public int estadoMov = 0;
    //public int movex = 0;
    public int posicion = 11;
	// Use this for initialization
	void Start () {
        
    }
	// Update is called once per frame
	void Update () {
        Debug.Log(salto);
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
        
        
    }

 

    //public void moveto(int posicion)
    //{
    //    movex = posicion;
    //    estadoMov = 1;
    //}

    
}

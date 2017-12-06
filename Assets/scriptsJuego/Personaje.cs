﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour {
	public Animator animP1;
	public Sprite personaje1;
	public float contador = 0;
    public float inicial;
    public float salto = 0.6f;
	public bool esAnimado = true;
	public int id;
	public int cpu;
    // public int estadoMov = 0;
    //public int movex = 0;
    public int posicion = 11;
	public int estado = 0;
	//0 es listo
	//1 es en procesador
	//2 es en bloqueado
	//3 es en suspendido
	// Use this for initialization
	void Start () {
        
    }
	// Update is called once per frame
	void Update () {
        contador += Time.deltaTime;
        

        if (contador>5&&contador<=10) {
            //animP1.SetInteger("estado", 1);
        }
        if (contador > 10 && contador < 15){           
            //animP1.SetInteger("estado", 2);
        }
        if (contador > 15 && contador < 20){
            //animP1.SetInteger("estado", 3);
        }
        if (contador > 20 && contador < 25){
           // animP1.SetInteger("estado", 4);
        }

		 if (posicion != -1) { 
        if (System.Math.Abs((inicial - (posicion * salto)) - transform.position.x)>0.02){
            if((inicial - (posicion * salto)) < transform.position.x){
                transform.position = new Vector3(transform.position.x - 0.02f, transform.position.y, transform.position.z);
            }else{
                transform.position = new Vector3(transform.position.x + 0.02f, transform.position.y, transform.position.z);
            }
        }
        }


    }

 

    //public void moveto(int posicion)
    //{
    //    movex = posicion;
    //    estadoMov = 1;
    //}

    
}

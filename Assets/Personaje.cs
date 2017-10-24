using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour {
	public Animator animP1;
	public Sprite personaje1;
	public float contador = 0;
	// Use this for initialization
	void Start () {
        
    }
	// Update is called once per frame
	void Update () {
        contador += Time.deltaTime;
        Debug.Log(contador);

        //if (contador == 100){
        //    animP1.SetInteger("estado", 1);
        //}
        //if (contador == 200){
        //    animP1.SetInteger("estado", 2);
        //}

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


        //		Debug.Log (animP1.GetInteger("estado"));
        //		contador++;
        //		if(contador>=100 && contador<=200){			
        //			cambiarEstado (1);
        //			animP1.SetInteger ("estado", 1);
        //		}
        //		if(contador>200 && contador<=300){
        //			cambiarEstado (2);
        //			animP1.SetInteger ("estado", 2);
        //		}
        //		if(contador>300 && contador<=400){
        //			cambiarEstado (3);
        //			animP1.SetInteger ("estado", 3);
        //		}
        //		if(contador>500){			
        //			cambiarEstado (4);
        //			animP1.SetInteger ("estado", 4);
        //		}
    }

    void cambiarEstado(int numero){
		animP1.SetInteger ("estado", numero);
	}
}

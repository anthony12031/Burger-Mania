using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour {
	public Animator animP1;
	public Sprite personaje1;
	public float contador = 0;
    public int estadoMov = 0;
    public int movex = 0;
	// Use this for initialization
	void Start () {
        
    }
	// Update is called once per frame
	void Update () {
        contador += Time.deltaTime;
        Debug.Log(contador);

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


        //Moving to 

        if (estadoMov == 1)
        {
            transform.position = new Vector3(transform.position.x + 5 * Time.deltaTime, transform.position.y, transform.position.z);
        }
        
        
    }

    public void moveto(int posicion)
    {
        movex = posicion;
        estadoMov = 1;
    }

    
}

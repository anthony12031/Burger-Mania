﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public unsafe class Cola<T>{
	nodo<T> cab,fin;


	public void Enqueue(T i){
		nodo<T> nuevo = new nodo<T>();
		nuevo.dato = i;
		nuevo.sig = null;
		if(cab == null) 
			cab = nuevo;
		else
			fin.sig=nuevo;
		fin = nuevo;
	}

	public T Dequeue(){
			T x;
			nodo<T> aux = cab;
			cab = aux.sig;
			x = aux.dato;
			return x;
	}

	public int Count(){
		int count = 0;
		nodo<T> aux = cab;
		while(aux!=null){
			aux = aux.sig;
			count++;
		}
		return count;
	}

	public T Peek(){
		return cab.dato;
	}


		

}

public unsafe class nodo <T> {  
	public T  dato;
	public nodo<T> sig;
}  

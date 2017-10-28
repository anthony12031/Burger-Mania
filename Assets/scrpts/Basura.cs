using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basura : MonoBehaviour {

	static AudioSource audioSource;

	public static void reproducirSonido(){
		audioSource.Play();
	}

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

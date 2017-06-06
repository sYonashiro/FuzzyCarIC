using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisaoCarroFuzzyMuro : MonoBehaviour {

	// Use this for initialization
	public GameObject Cronometro;
	public static bool comecou;
	private int cont;

	void Awake()
	{
		comecou = false;
		cont = 3;
	}
	void Update()
	{

		// Faz a contagem regressiva a partir de 3 para comecar a corrida
		int regressiva;
		float tempo =Time.timeSinceLevelLoad;

		regressiva = cont - (int)tempo;
		if (regressiva >= 0) {
			

			Cronometro.GetComponent<TextMesh> ().text = regressiva.ToString ();
		}
		else {
			Cronometro.GetComponent<TextMesh> ().text = "";
			comecou = true;
		}
	}
	// Verifica se o carro fuzzy bateu no muro. se sim o carro explode
	void OnTriggerEnter(Collider other)
	{



		if(other.tag.Equals("Muro")) 

		{

			Raycast.Explodiu = 3;

		}


	}
}

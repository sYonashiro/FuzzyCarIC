using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuemCruzouALinhaDeChegada : MonoBehaviour {

	private bool campeaoDefinido;
	public GameObject VocePerdeu;
	public GameObject VoceVenceu;
	public GameObject SomVitoria;
	public GameObject SomDerrota;
	public GameObject CarroFuzzy;
	public GameObject CarroAD;
	public GameObject NovaCamera;
	void Awake()
	{
		campeaoDefinido = false;
	}

	void Update()
	{
		if (Raycast.Explodiu == 2) 
		{
			//VocePerdeu.SetActive(true);
			//SomDerrota.SetActive (true);
			//campeaoDefinido = true;
			CarroFuzzy.SetActive (false);
			CarroAD.SetActive (false);
			NovaCamera.SetActive (true);

		}


		if (Raycast.Explodiu == 3) 
		{
			VoceVenceu.SetActive(true);
			SomVitoria.SetActive (true);
			//campeaoDefinido = true;
			CarroFuzzy.SetActive (false);
			CarroAD.SetActive (false);
			NovaCamera.SetActive (true);

		}

	}

	// Define quem cruzou primeiro a linha de chegada
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name.Equals("CarAD") && campeaoDefinido == false) 

		{
			
			VoceVenceu.SetActive(true);
			SomVitoria.SetActive (true);
			campeaoDefinido = true;
			CarroFuzzy.SetActive (false);
			CarroAD.SetActive (false);
			NovaCamera.SetActive (true);

		}

		if (other.gameObject.name.Equals("Car") && campeaoDefinido == false) 

		{
			VocePerdeu.SetActive(true);
			SomDerrota.SetActive (true);
			campeaoDefinido = true;
			CarroFuzzy.SetActive (false);
			CarroAD.SetActive (false);
			NovaCamera.SetActive (true);
		}

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarLinhadeChegada : MonoBehaviour {

	public GameObject carro;
	public GameObject LinhaLargada;
	public GameObject LinhaChegada;
	// Depois que o carro player1 passar pela linha ela apaga a placa de largada e ativa a placa de chegada
	void OnTriggerEnter(Collider other)
	{
		print ("Colidiu");
		if(other.gameObject.Equals(carro))
			{
				LinhaChegada.SetActive (true);
				LinhaLargada.SetActive (false);
			}

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisaoCD : MonoBehaviour {

	// Verifica em qual segmento da pista esta.
	public GameObject objeto;


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.Equals(objeto)) 

		{
			Raycast.Aceleracao = 10;
			Raycast.Movimento = 1.0f;
			//Debug.LogError (Raycast.Movimento);
			//Destroy (other.gameObject);

		}

	}
}

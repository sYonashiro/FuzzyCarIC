using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisaoCE : MonoBehaviour {

	// Verifica em qual segmento da pista esta.
	public GameObject objeto;


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.Equals(objeto)) 

		{
			Raycast.Aceleracao = 11;
			Raycast.Movimento = 1.0f;

		}

	}
}

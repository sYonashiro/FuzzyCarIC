using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisaoReta : MonoBehaviour {

	// Use this for initialization
	public GameObject objeto;


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.Equals(objeto)) 

		{
			Raycast.Aceleracao = 30;
			Raycast.Movimento = 0.0f;
			//Debug.LogError (Raycast.Movimento);
			//Destroy (other.gameObject);

		}

	}
}

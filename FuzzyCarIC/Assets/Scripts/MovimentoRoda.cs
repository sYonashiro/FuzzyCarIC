using UnityEngine;
using System.Collections;

public class MovimentoRoda : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.UpArrow))
            transform.Rotate(0, 20, 0);

        if (Input.GetKey(KeyCode.DownArrow))
            transform.Rotate(0, -20, 0);
	}
}

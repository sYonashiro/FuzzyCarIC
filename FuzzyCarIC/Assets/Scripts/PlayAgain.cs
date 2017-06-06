using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgain : MonoBehaviour {

	// Reinicia a fase
	public void playAgain()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}

using UnityEngine;
using System.Collections;

public class MovimentoRoda : MonoBehaviour {

	public GameObject explosao;
	private int explodiu;
	private float contador;
	private float velocidade;
	public AudioSource BombSound;
	public GameObject Parado;
	public GameObject Acelerando;
	public GameObject Freando;
	public GameObject voceperdeu;
	public GameObject vaias;
	public GameObject MusicaPrincipal;
	// Use this for initialization
	void Start () {
		explodiu = 0;
		contador = 0.5f;
		velocidade = 0;
		MusicaPrincipal.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 atual = new Vector3(transform.position.x,0.000000f,transform.position.z);
		if(explodiu==0)
		transform.position = atual;

		// Logo após a explosão faz o carrinho subir e girar no ar
		if (explodiu == 1 && contador<6) {
			contador += 0.1f;
			atual = new Vector3(transform.position.x,contador,transform.position.z);
			transform.RotateAround (atual, transform.position, 4);
			transform.position = atual;
			
		}
		//espera o contador chegar a 6 para encerrar a animação de explosao
		if (contador >= 6) {
			explosao.SetActive (false);
			voceperdeu.SetActive (true);
			MusicaPrincipal.SetActive (false);
			Parado.SetActive (false);
			Acelerando.SetActive (false);
			Freando.SetActive (false);
			vaias.SetActive (true);
			// numero 2 significa que a animação da explosao ja terminou e que os dois carros podem ser eliminados da pista
			Raycast.Explodiu = 2;

		}



		// movimentação do player 1 para a direita 
		if (Input.GetKey(KeyCode.RightArrow)&& ColisaoCarroFuzzyMuro.comecou==true)
            transform.Rotate(0, 0.5f, 0);

		// movimentação do player 1 para a esquerda 
		if (Input.GetKey(KeyCode.LeftArrow)&& ColisaoCarroFuzzyMuro.comecou==true)
            transform.Rotate(0, -0.5f, 0);

		// quando o usuario aperta na tecla seta para cima, entao a velocidade incrementa 0.15
		if (Input.GetKey (KeyCode.UpArrow) && ColisaoCarroFuzzyMuro.comecou==true) {
			velocidade += 0.15f;
			Parado.SetActive (false);
			Acelerando.SetActive (true);
			Freando.SetActive (false);
		}
		// quando o usuario aperta na tecla seta para baixo, entao a velocidade decrementa 0.15
		if (Input.GetKey (KeyCode.DownArrow)&&ColisaoCarroFuzzyMuro.comecou==true) {
			velocidade -= 0.15f;
			Parado.SetActive (false);
			Acelerando.SetActive (false);
			Freando.SetActive (true);
		}
		if (velocidade == 0) {

			Parado.SetActive (true);
			Acelerando.SetActive (false);
			Freando.SetActive (false);
		}
			
		if (velocidade > 30)
			velocidade = 30;
		if (velocidade < -10)
			velocidade = -10;



		transform.Translate(0, 0, velocidade * Time.fixedDeltaTime );
	}

	// Verifica se o carrinho player1 colidiu com alguma coisa. se colidiu no muro ou no carro fuzzy, entao ele explode
	void OnTriggerEnter(Collider other)
	{
		
		if (other.name.Equals("Car")) 

		{
			explosao.SetActive (true);
			explodiu = 1;
			Raycast.Explodiu = 1;
			BombSound.Play ();

			//Debug.LogError (Raycast.Movimento);
			//Destroy (other.gameObject);

		}

		else if(other.tag.Equals("Muro")) 

		{
			explosao.SetActive (true);
			explodiu = 1;
			Raycast.Explodiu = 1;
			BombSound.Play ();
			//Debug.LogError (Raycast.Movimento);
			//Destroy (other.gameObject);

		}

	}
}

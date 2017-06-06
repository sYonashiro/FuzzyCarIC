using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Raycast : MonoBehaviour {

    #region Variáveis privadas
    private Fuzzy fuzzy;
    private RaycastHit frontHit;
	private RaycastHit frontalrightHit;
	private RaycastHit frontalleftHit;
	private RaycastHit curvedfrontalrightHit;
	private RaycastHit curvedfrontalleftHit;
    private RaycastHit rightHit;
    private RaycastHit leftHit;
	private RaycastHit frontrightHit;
	private RaycastHit rearrightHit;
	private RaycastHit frontleftHit;
	private RaycastHit rearleftHit;

    private float angle;
	private int contFrames;
	public static float Movimento;
	public static float Aceleracao;
	public static float carroAD;
	public GameObject carro;
	public GameObject farolFD;
	public GameObject farolRD;
	public GameObject farolFE;
	public GameObject farolRE;


    #endregion

	public static int Explodiu;

    #region Variáveis públicas (objetos selecionados como entradas do script no Unity)
    public GameObject car;
    public Text distanceText;
    #endregion

    // Use this for initialization
    void Start()
    {
        //angle = 0;
		//fuzzy = new Fuzzy();
		Movimento = 0;
    }

    void Awake()
    {
		fuzzy = new Fuzzy();
		angle = 0;
		contFrames = 0;
		Aceleracao = 20;
		carroAD = 0;
		Explodiu = 0;
    }

    // Update is called once per frame
    void Update()
    {

		if(Explodiu==0 && ColisaoCarroFuzzyMuro.comecou==true)
		{
        UpdateTexts();


			
		//print (carroAD);

        // Movimenta o carro para frente

		car.transform.Translate(0, 0, fuzzy.GetAcceleration() * Time.fixedDeltaTime );
		carro.transform.position = new Vector3(carro.transform.position.x, 0 , carro.transform.position.z);
		angle = fuzzy.GetAngle (frontHit.distance, rightHit.distance, leftHit.distance, Movimento, frontrightHit.distance, rearrightHit.distance, frontleftHit.distance, rearleftHit.distance, frontalrightHit.distance, frontalleftHit.distance, curvedfrontalrightHit.distance,curvedfrontalleftHit.distance, carroAD);
		car.transform.Rotate(0, angle/60.0f, 0);
		//print(fuzzy.GetAcceleration());
		}
    }

    /// <summary>
    /// Atualiza distâncias frontal e lateral, e mostra na tela
    /// </summary>
    private void UpdateTexts()
    {
		//Vector3 frontPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z+3);
		//Vector3 rearPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z-3);
		// Se o Raycast colidiu de frente, atualiza distância frontal e mostra na tela

		Vector3 atual = new Vector3(transform.position.x,0.0000000f,transform.position.z);

		transform.position = atual;

		//Se o Raycast colidiu na frente central, atualiza distância frente e mostra na tela
        if (Physics.Raycast(transform.position, transform.right, out frontHit, 1000))
        {
            distanceText.text = " Frente: " + frontHit.distance.ToString();
			Debug.DrawLine (transform.position, frontHit.point, Color.cyan);

			if (frontHit.collider.name.Equals ("CarAD")) {

				carroAD = 1;
				//print (carroAD);
			} else {
				carroAD = 0;
			}

        }
        // Se o Raycast colidiu do lado direito, atualiza distância lateral direita e mostra na tela
        if (Physics.Raycast(transform.position, transform.up, out rightHit, 300))
        {
            distanceText.text += "\n Direita: " + rightHit.distance.ToString();
			Debug.DrawLine (transform.position, rightHit.point, Color.cyan);

			if (rightHit.collider.name.Equals ("CarAD")) {

				carroAD = 2;
				//print (carroAD);
			} else {
				carroAD = 0;
			}
        }
        // Se o Raycast colidiu do lado esquerdo, atualiza distância lateral esquerda e mostra na tela
        if (Physics.Raycast(transform.position, -transform.up, out leftHit, 300))
        {
            distanceText.text += "\n Esquerda: " + leftHit.distance.ToString();
			Debug.DrawLine (transform.position, leftHit.point, Color.cyan);

			if (leftHit.collider.name.Equals ("CarAD")) {

				carroAD = 3;
				//print (carroAD);
			} else {
				carroAD = 0;
			}
        }




		//Se o Raycast colidiu do lado Direita Superior, atualiza distância direita superior e mostra na tela
		if (Physics.Raycast(farolFD.transform.position, transform.up, out frontrightHit, 300))
		{
			distanceText.text += "\n Direita Superior: " + frontrightHit.distance.ToString();
			Debug.DrawLine (farolFD.transform.position, frontrightHit.point, Color.cyan);

			if (frontrightHit.collider.name.Equals ("CarAD")) {

				carroAD = 4;
				//print (carroAD);
			} else {
				carroAD = 0;
			}
		}
		// Se o Raycast colidiu do lado direito inferior, atualiza distância lateral direita inferior e mostra na tela
		if (Physics.Raycast(farolRD.transform.position, transform.up, out rearrightHit, 300))
		{
			distanceText.text += "\n Direita Inferior: " + rearrightHit.distance.ToString();
			if (rearrightHit.collider.name.Equals ("CarAD")) {

				carroAD = 5;
				//print (carroAD);
			} else {
				carroAD = 0;
			}
			Debug.DrawLine (farolRD.transform.position, rearrightHit.point, Color.cyan);
		}
		// Se o Raycast colidiu do lado esquerdo superior, atualiza distância lateral esquerda superior e mostra na tela
		if (Physics.Raycast(farolFE.transform.position, -transform.up, out frontleftHit, 300))
		{
			distanceText.text += "\n Esquerda Superior: " + frontleftHit.distance.ToString();
			Debug.DrawLine (farolFE.transform.position, frontleftHit.point, Color.cyan);


			if (frontleftHit.collider.name.Equals ("CarAD")) {

				carroAD = 6;
				//print (carroAD);
			} else {
				carroAD = 0;
			}
		}

		//Se o Raycast colidiu do lado esquerdo inferior, atualiza distância esquerdo inferior e mostra na tela
		if (Physics.Raycast(farolRE.transform.position, -transform.up, out rearleftHit, 300))
		{
			
			distanceText.text += "\n Esquerda Inferior: " + rearleftHit.distance.ToString();
			Debug.DrawLine (farolRE.transform.position, rearleftHit.point, Color.cyan);

			if (rearleftHit.collider.name.Equals ("CarAD")) {

				carroAD = 7;
				//print (carroAD);
			} else {
				carroAD = 0;
			}
		}


		//Se o Raycast colidiu na parte frontal direita, atualiza distância frontal direita e mostra na tela
		if (Physics.Raycast(farolFD.transform.position, transform.right, out frontalrightHit, 1000))
		{
			distanceText.text += "\n Frente Direita: " + frontalrightHit.distance.ToString();
			Debug.DrawLine (farolFD.transform.position, frontalrightHit.point, Color.cyan);

			if (frontalrightHit.collider.name.Equals ("CarAD")) {

				carroAD = 8;
				//print (carroAD);
			} else {
				carroAD = 0;
			}
		}

		//Se o Raycast colidiu na parte frontal esquerda, atualiza distância frontal esquerda e mostra na tela
		if (Physics.Raycast(farolFE.transform.position, transform.right, out frontalleftHit, 1000))
		{
			distanceText.text += "\n Frente Esquerda: " + frontalleftHit.distance.ToString();
			Debug.DrawLine (farolFE.transform.position, frontalleftHit.point, Color.cyan);

			if (frontalleftHit.collider.name.Equals ("CarAD")) {

				carroAD = 9;
				//print (carroAD);
			} else {
				carroAD = 0;
			}
		}

		//Se o Raycast colidiu na parte frontal curvada a direita, atualiza distância frontal curvada direita e mostra na tela
		if (Physics.Raycast(farolFD.transform.position, (transform.right+transform.up)/2.0f, out curvedfrontalrightHit, 1000))
		{
			distanceText.text += "\n Frente Direita Curvada: " + curvedfrontalrightHit.distance.ToString();
			Debug.DrawLine (farolFD.transform.position, curvedfrontalrightHit.point, Color.cyan);

			if (curvedfrontalrightHit.collider.name.Equals ("CarAD")) {

				carroAD = 10;
				//print (carroAD);
			} else {
				carroAD = 0;
			}
		}


		//Se o Raycast colidiu na parte frontal curvada esquerda, atualiza distância frontal curvada esquerda e mostra na tela
		if (Physics.Raycast(farolFE.transform.position, (transform.right-transform.up)/2.0f, out curvedfrontalleftHit, 1000))
		{
			distanceText.text += "\n Frente Esquerda Curvada: " + curvedfrontalleftHit.distance.ToString();
			Debug.DrawLine (farolFE.transform.position, curvedfrontalleftHit.point, Color.cyan);

			if (curvedfrontalleftHit.collider.name.Equals ("CarAD")) {

				carroAD = 11;
				//print (carroAD);
			} else {
				carroAD = 0;
			}
		}

    }




}

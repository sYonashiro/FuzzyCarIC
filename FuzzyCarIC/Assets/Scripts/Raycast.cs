using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Raycast : MonoBehaviour {

    #region Variáveis privadas
    //private Fuzzy fuzzy;
    private RaycastHit frontHit;
    private RaycastHit rightHit;
    private RaycastHit leftHit;
    //private float angle;
    #endregion

    #region Variáveis públicas (objetos selecionados como entradas do script no Unity)
    public GameObject car;
    public Text distanceText;
    #endregion

    // Use this for initialization
    void Start()
    {
        //angle = 0;
    }

    void Awake()
    {
        //fuzzy = new Fuzzy();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTexts();

        // Movimenta o carro para frente
        car.transform.Translate(0, 0, 10 * Time.fixedDeltaTime);
        
        //angle = fuzzy.GetAngulo(frontHit.distance, rightHit.distance, leftHit.distance);
        //carro.transform.Rotate(0, angle * 0.1f, 0);
    }

    /// <summary>
    /// Atualiza distâncias frontal e lateral, e mostra na tela
    /// </summary>
    private void UpdateTexts()
    {
        // Se o Raycast colidiu de frente, atualiza distância frontal e mostra na tela
        if (Physics.Raycast(transform.position, transform.right, out frontHit, 1000))
        {
            distanceText.text = "Frente: " + frontHit.distance.ToString();
        }
        // Se o Raycast colidiu do lado direito, atualiza distância lateral direita e mostra na tela
        if (Physics.Raycast(transform.position, transform.up, out rightHit, 300))
        {
            distanceText.text += "\nDireita: " + rightHit.distance.ToString();
        }
        // Se o Raycast colidiu do lado esquerdo, atualiza distância lateral esquerda e mostra na tela
        if (Physics.Raycast(transform.position, -transform.up, out leftHit, 300))
        {
            distanceText.text += "\nEsquerda: " + leftHit.distance.ToString();
        }
    }
}

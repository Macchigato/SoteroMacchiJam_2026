using UnityEngine;
using UnityEngine.UI;

public class ExibirPista : MonoBehaviour
{
    public static ExibirPista Instance;

    public GameObject painelPista;
    public Image imagemPista;
    public RectTransform iconeGiratario;

    public float velocidadeRotacao = 180f;
    public float tempoEmTela = 3f;

    void Awake()
    {
        Instance = this;
        painelPista.SetActive(false);
    }

    void Update()
    {
        if(painelPista.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            painelPista.SetActive(false);
            GameManager.Instance.ColetarPista();
        }
    }
    public void MostrarPista(Sprite spritePista)
    {

        imagemPista.sprite = spritePista;
        iconeGiratario.rotation = Quaternion.identity;
        painelPista.SetActive(true);
    }

}

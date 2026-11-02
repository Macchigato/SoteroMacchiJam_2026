using UnityEngine;

public class UI_NPCManager : MonoBehaviour
{
    public GameObject telaUI;
    public GameObject pedidoUI;
    public GameObject esperaUI;
    public GameObject reclamacaoUI;

    void Start()
    {
        telaUI.SetActive(false);
        pedidoUI.SetActive(false);
        esperaUI.SetActive(false);
        reclamacaoUI.SetActive(false);
    }
}
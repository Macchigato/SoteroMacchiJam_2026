using UnityEngine;

public class UI_NPCManager : MonoBehaviour
{
    public GameObject telaUI;
    public GameObject pedidoUI;
    public GameObject esperaUI;

    void Start()
    {
        telaUI.SetActive(false);
        pedidoUI.SetActive(false);
        esperaUI.SetActive(false);
    }
}
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DialogoManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject canvasDialogo;
    public TMP_Text textoDialogo;
    public string[] falas;

    private int falaAtual = 0;
    public bool dialogoAtivo = false;
    public int contador = 0;


    void Start()
    {
        canvasDialogo.SetActive(false);
    }

    void Update()
    {
        if (!dialogoAtivo)
        {
            return;
        }

        if ( Keyboard.current.anyKey.wasPressedThisFrame)
        {
            AvancarDialogo();
        }
    }

    public void IniciarDialogo()
    {
        if (falas == null || falas.Length == 0)
        {
            return;
        }

        falaAtual = 0;
        dialogoAtivo = true;

        canvasDialogo.SetActive(true);
        gameManager.SetConversando(true);

        AtualizarTexto();
    }

    private void AvancarDialogo()
    {
        falaAtual++;

        if (falaAtual >= falas.Length)
        {
            EncerrarDialogo();
            return;
        }

        AtualizarTexto();
    }

    private void AtualizarTexto()
    {
        textoDialogo.text = falas[falaAtual];
    }

    public void EncerrarDialogo()
    {
        dialogoAtivo = false;
        canvasDialogo.SetActive(false);

        gameManager.SetConversando(false);
    }
}
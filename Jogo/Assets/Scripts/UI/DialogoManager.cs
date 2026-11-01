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
    private bool dialogoAtivo = false;

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

        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
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
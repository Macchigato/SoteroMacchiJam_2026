using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class DialogoManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject canvasDialogo;
    public TMP_Text textoDialogo;
    public Image image;
    public DialogoOS[] falas;

    private int falaAtual = 0;
    public bool dialogoAtivo = false;
    public int contador = 0;


    void Start()
    {
        canvasDialogo.SetActive(false);
        falaAtual = 0;
    }

    void Update()
    {
        if (!dialogoAtivo)
        {
            return;
        }

        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            EncerrarDialogo();
        }
    }

    public void IniciarDialogo()
    {
        if (falas == null || falas.Length == 0)
        {
            return;
        }

        dialogoAtivo = true;

        canvasDialogo.SetActive(true);
        gameManager.SetConversando(true);

        textoDialogo.text = falas[falaAtual].texto;
        image.sprite = falas[falaAtual].imagem;
        falaAtual++;

    }

    public bool TemDialogoDisponivel()
    {
        if (falaAtual == falas.Length)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void EncerrarDialogo()
    {
        dialogoAtivo = false;
        canvasDialogo.SetActive(false);
        gameManager.SetConversando(false);
    }
}
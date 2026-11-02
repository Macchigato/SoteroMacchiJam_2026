using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class pista : MonoBehaviour
{

    public GameManager gameManager;

    public Texture2D textura;

    private bool podeColetarPista;

    private InputAction jumpAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        if (podeColetarPista)
        {
            if (jumpAction != null && jumpAction.WasPressedThisFrame())
            {
            gameManager.pistaAtual = textura;
            gameManager.AtualizarPista();
            gameManager.ColetarPista();
            gameManager.gameState = 1;
            gameManager.tempo = 1f;
            Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        podeColetarPista = true;
    }

    void OTriggerExit2D(Collider2D collision)
    {
        podeColetarPista = false;
    }
}

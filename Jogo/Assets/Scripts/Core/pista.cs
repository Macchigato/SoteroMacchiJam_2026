using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class pista : MonoBehaviour
{
    public Sprite spritePista;
    private bool podeColetarPista;
    private InputAction jumpAction;
    void Start()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        if (podeColetarPista)
        {
            if (jumpAction != null && jumpAction.WasPressedThisFrame())
            {
                ExibirPista.Instance.MostrarPista(spritePista);
                GameManager.Instance.gameState = 1;
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        podeColetarPista = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        podeColetarPista = false;
    }
}

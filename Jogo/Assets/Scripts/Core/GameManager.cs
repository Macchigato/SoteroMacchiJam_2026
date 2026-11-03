using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // 0 = movimentando, 1 = animação da pista
    public int gameState = 0;
    private InputAction jumpAction;
    public int pistasColetadas = 0;
    public GameObject corraUI;

    public GameObject painelAlertaDia;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
        if(corraUI != null) corraUI.SetActive(false);
        StartCoroutine(Introducao());
    }

    IEnumerator Introducao()
    {
        if (painelAlertaDia != null) painelAlertaDia.SetActive(true);
        yield return new WaitForSeconds(1f);
        painelAlertaDia.SetActive(false);
    }


    void Update()
    {
        if (gameState == 1)
        {
            if (jumpAction != null && jumpAction.WasPressedThisFrame())
            {
                gameState = 0;
            }
        }
    }

    public void ColetarPista()
    {
        pistasColetadas++;
        
        if (pistasColetadas == 3)
        {
            if (SceneManager.GetActiveScene().name == "Noite 1")
            {
                SceneManager.LoadScene("Dia 2");
            } else if (SceneManager.GetActiveScene().name == "Noite 2")
            {
                if(corraUI != null) corraUI.SetActive(true);
            }
        }
    }

    public bool conversando = false;

    public Behaviour movimentoPlayer;

    public void SetConversando(bool estado)
    {
        conversando = estado;

        if (movimentoPlayer != null)
        {
            movimentoPlayer.enabled = !conversando;
        }
    }


}
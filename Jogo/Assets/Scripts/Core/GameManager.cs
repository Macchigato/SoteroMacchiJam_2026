using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // 0 = movimentando, 1 = animação da pista
    public int gameState = 0;

    public pista pistaObj;
    public GameObject pistaDesenho;
    public Texture2D pistaAtual;

    private InputAction jumpAction;
    private SpriteRenderer pistaRenderer;

    public int pistasColetadas = 0;

    //public TextMeshProUGUI tmp;

    public GameObject glow;

    void Start()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");

        if (pistaDesenho != null)
        {
            pistaRenderer = pistaDesenho.GetComponent<SpriteRenderer>();
            AtualizarPista();
            pistaDesenho.SetActive(false);
            glow.SetActive(false);
        }
    }

    void Update()
    {
        if (gameState == 1)
        {
            if (pistaDesenho != null)
            {
                glow.SetActive(true);
                pistaDesenho.SetActive(true);
                pistaDesenho.transform.localScale = new Vector3(Mathf.Sin(Time.time) * 2.0f, 2.0f, 1.0f);
            }

            if (jumpAction != null && jumpAction.WasPressedThisFrame())
            {
                if (pistaDesenho != null)
                {
                    pistaDesenho.SetActive(false);
                    glow.SetActive(false);
                }

                gameState = 0;
            }
        }
    }

    public void AtualizarPista()
    {
        if (pistaRenderer == null || pistaAtual == null)
        {
            return;
        }

        pistaRenderer.sprite = Sprite.Create(
            pistaAtual,
            new Rect(0, 0, pistaAtual.width, pistaAtual.height),
            new Vector2(0.5f, 0.5f),
            100f
        );
    }

        public void ColetarPista()
    {
        pistasColetadas++;
       // tmp.text = "Pistas Coletadas: " + pistasColetadas.ToString();
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
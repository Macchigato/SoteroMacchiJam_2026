using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public static Movement Instance { get; private set; }

    public float velocidade = 4.0f;

    public Rigidbody2D rb;

    private InputAction moveAction;

    public GameManager gameManager;
    public OSComida comidaMao = null;

    private Animator animator;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (gameManager != null && gameManager.gameState == 0 && !gameManager.conversando)
        {
            if (moveAction != null)
            {
                rb.linearVelocityX = moveAction.ReadValue<Vector2>().x * velocidade;
            }
        }
        else
        {
            rb.linearVelocityX = 0.0f;
        }


        if (moveAction.ReadValue<Vector2>().x != 0)
        {
            if (moveAction.ReadValue<Vector2>().x > 0) GetComponent<SpriteRenderer>().flipX = false;
            else GetComponent<SpriteRenderer>().flipX = true;
            
            animator.SetBool("Parado", false);
        }
        else
        {
            animator.SetBool("Parado", true);
        }
    }
}
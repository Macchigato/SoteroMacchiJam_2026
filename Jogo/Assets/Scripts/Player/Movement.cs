using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{


    public static Movement Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public float velocidade = 4.0f;

    public Rigidbody2D rb;

    InputAction moveAction;

    public GameManager gameManager;
    public OSComida comidaMao = null;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameState == 0)
        {
            rb.linearVelocityX = moveAction.ReadValue<Vector2>().x * velocidade;
        }
        else
        {
            rb.linearVelocityX = 0.0f;
        }
    }
}

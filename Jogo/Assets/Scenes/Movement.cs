using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    public float velocidade = 4.0f;

    public Rigidbody2D rb;

    InputAction moveAction;

    public GameManager gameManager;


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

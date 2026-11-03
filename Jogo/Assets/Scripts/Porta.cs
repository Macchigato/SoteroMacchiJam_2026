using UnityEngine;
using UnityEngine.SceneManagement;

public class Porta : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            if(GameManager.Instance.pistasColetadas == 3)
            {
                SceneManager.LoadScene("Final");
            } else
            {
                // tocar som de trancado
            }
            
        }
    }



}
